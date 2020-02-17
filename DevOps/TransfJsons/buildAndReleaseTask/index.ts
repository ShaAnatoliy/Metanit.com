import tl = require('azure-pipelines-task-lib/task');
import fs = require('fs');
import iconv = require('iconv-lite');
import jschardet = require('jschardet');
import path = require('path');


const ENCODING_AUTO: string = 'auto';
const ENCODING_ASCII: string = 'ascii';
const ENCODING_UTF_7: string = 'utf-7';
const ENCODING_UTF_8: string = 'utf-8';
const ENCODING_UTF_16LE: string = 'utf-16le';
const ENCODING_UTF_16BE: string = 'utf-16be';
const ENCODING_WIN1252: string = 'windows1252';
const ENCODING_ISO_8859_1: string = 'iso88591';

const ACTION_WARN: string = 'warn';
const ACTION_FAIL: string = 'fail';

const XML_ESCAPE: RegExp = /[<>&'"]/g;
const JSON_ESCAPE: RegExp = /["\\/\b\f\n\r\t]/g;

interface Options {
    readonly encoding: string, 
    readonly keepToken: boolean,
    readonly actionOnMissing: string, 
    readonly writeBOM: boolean, 
    readonly emptyValue: string, 
    readonly escapeType: string,
    readonly escapeChar: string, 
    readonly charsToEscape: string,
    readonly verbosity: string
}

interface ILogger {
    debug(message: string): void,
    info(message: string): void,
    warn(message: string): void,
    error(message: string): void
}

class NullLogger implements ILogger {
    public debug(message: string): void {}
    public info(message: string): void {}
    public warn(message: string): void {}
    public error(message: string): void {}
}

enum LogLevel {
    Debug,
    Info,
    Warn,
    Error,
    Off = 255
}

class Logger implements ILogger {
    private _level: LogLevel;
    
    constructor(level: LogLevel) {
        this._level = level;
    }

    public debug(message: string): void {
        this.log(LogLevel.Debug, message);
    }

    public info(message: string): void {
        this.log(LogLevel.Info, message);
    }

    public warn(message: string): void {
        this.log(LogLevel.Warn, message);
    }

    public error(message: string): void {
        this.log(LogLevel.Error, message);
    }

    private log(level: LogLevel, message: string): void {
        // always log debug to system debug
        if (level === LogLevel.Debug)
            tl.debug(message);
        
        // always set task result on error
        if (level === LogLevel.Error)
            tl.setResult(tl.TaskResult.Failed, message);

        if (level < this._level)
            return;

        switch (level)
        {
            case LogLevel.Debug:
            case LogLevel.Info:
                console.log(message);
                break;

            case LogLevel.Warn:
                tl.warning(message);
                break;
        }
           
    }
}

var logger: ILogger = new NullLogger();

var forJson = function (match: string) : string
{
    switch (match) {
        case '"':
        case '\\':
        case '/':
            return '\\' + match;
        
        case '\b': return "\\b";
        case '\f': return "\\f";
        case '\n': return "\\n";
        case '\r': return "\\r";
        case '\t': return "\\t";
    }
    return match;
};

var forXML = function (match: string) : string
{
    switch (match) {
        case '<': return '&lt;';
        case '>': return '&gt;';
        case '&': return '&amp;';
        case '\'': return '&apos;';
        case '"': return '&quot;';
    }
    return match;
};

var mapEncoding = function (encoding: string): string {
    switch (encoding)
    {
        case 'auto':
            return ENCODING_AUTO;

        case 'Ascii':
        case 'ascii': 
            return ENCODING_ASCII;

        case 'UTF7':
        case 'utf-7': 
            return ENCODING_UTF_7;

        case 'UTF8':
        case 'utf-8': 
            return ENCODING_UTF_8;

        case 'Unicode':
        case 'utf-16le': 
            return ENCODING_UTF_16LE;

        case 'BigEndianUnicode':
        case 'utf-16be': 
            return ENCODING_UTF_16BE;

        case 'win1252':
            return ENCODING_WIN1252;
        
        case 'iso88591':
            return ENCODING_ISO_8859_1;

        case 'UTF32':
            throw new Error('utf-32 encoding is no more supported.');

        case 'BigEndianUTF32':
            throw new Error('utf-32be encoding is no more supported.');

        default:
            throw new Error('invalid encoding: ' + encoding);
    }
}

var getEncoding = function (filePath: string): string {
    let buffer: Buffer = fs.readFileSync(filePath, { flag: 'r' });
    let charset: any = jschardet.detect(buffer);

    switch (charset.encoding)
    {
        case 'ascii':
            return ENCODING_ASCII;

        case 'UTF-8':
            return ENCODING_UTF_8;

        case 'UTF-16LE':
            return ENCODING_UTF_16LE;

        case 'UTF-16BE':
            return ENCODING_UTF_16BE;

        case 'windows-1252':
            return ENCODING_WIN1252;

        default:
            return ENCODING_ASCII;
    }
}

var replaceTokensInFile = function ( filePath: string, regex: RegExp, options: Options): void 
{
    logger.info('replacing tokens in: ' + filePath);

    // ensure encoding
    let encoding: string = options.encoding;
    if (options.encoding === ENCODING_AUTO)
        encoding = getEncoding(filePath);

    logger.debug('using encoding: ' + encoding);

    // read file and replace tokens
    let content: string = iconv.decode(fs.readFileSync(filePath), encoding);
    content = content.replace(regex, (match, name) => {
        let value: string = tl.getVariable(name) as string;

        if (!value)
        {
            if (options.keepToken)
                value = match;
            else
                value = '';

            let message: string = 'variable not found: ' + name;
            switch (options.actionOnMissing)
            {
                case ACTION_WARN:
                    logger.warn(message);
                    break;

                case ACTION_FAIL:
                    logger.error(message);
                    break;

                default:
                    logger.debug(message);
            }
        }
        else if (options.emptyValue && value === options.emptyValue)
            value = '';

        let escapeType: string = options.escapeType;
        if (escapeType === 'auto')
        {
            switch (path.extname(filePath)) {
                case '.json':
                    escapeType = 'json';
                    break;

                case '.xml':
                    escapeType = 'xml';
                    break;

                default:
                    escapeType = 'none';
                    break;
            }
        }

        // log value before escaping to show raw value and avoid secret leaks (escaped secrets are not replaced by ***)
        logger.debug(name + ': ' + value);

        switch (escapeType) {
            case 'json':
                value = value.replace(JSON_ESCAPE, forJson(match));
                break;

            case 'xml':
                value = value.replace(XML_ESCAPE, forXML(match));
                break;

            case 'custom':
                if (options.escapeChar && options.charsToEscape)
                    for (var c of options.charsToEscape)
                        // split and join to avoid regex and escaping escape char
                        value = value.split(c).join(options.escapeChar + c);
                break;
        }

        return value;
    });

    // write file
    fs.writeFileSync(filePath, iconv.encode(content, encoding, { addBOM: options.writeBOM, stripBOM: undefined, defaultEncoding: undefined }));
}

var mapLogLevel = function (level: string): LogLevel {
    switch (level)
    {
        case "normal":
            return LogLevel.Info;
        
        case "detailed":
            return LogLevel.Debug;
        
        case "off":
            return LogLevel.Off;
    }

    return LogLevel.Info;
}

async function run() {
    try {
        // load inputs
        let root: string = tl.getPathInput('rootDirectory', false, true) as string;
        let tokenPrefix: string = tl.getInput('tokenPrefix', true) as string;
        tokenPrefix = tokenPrefix.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&');
        let tokenSuffix: string = tl.getInput('tokenSuffix', true) as string;
        tokenSuffix = tokenSuffix.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&');
        let options: Options = {
            encoding: mapEncoding(tl.getInput('encoding', true)  as string),
            keepToken: tl.getBoolInput('keepToken', true),
            actionOnMissing: tl.getInput('actionOnMissing', true)  as string,
            writeBOM: tl.getBoolInput('writeBOM', true),
            emptyValue: tl.getInput('emptyValue', false)  as string,
            escapeType: tl.getInput('escapeType', false)  as string,
            escapeChar: tl.getInput('escapeChar', false) as string,
            charsToEscape: tl.getInput('charsToEscape', false)  as string,
            verbosity: tl.getInput('verbosity', true)  as string
        };

        logger = new Logger(mapLogLevel(options.verbosity));

        let targetFiles: string[] = [];
        tl.getDelimitedInput('targetFiles', '\n', true).forEach((x: string) => {
            if (x)
                x.split(',').forEach((y: string) => {
                    if (y)
                        targetFiles.push(y.trim());
                })
        });

        // initialize task
        let regex: RegExp = new RegExp(tokenPrefix + '((?:(?!' + tokenSuffix + ').)*)' + tokenSuffix, 'gm');
        logger.debug('pattern: ' + regex.source);

        // process files
        tl.findMatch(root, targetFiles).forEach(filePath => {
            if (!tl.exist(filePath))
            {
                logger.error('file not found: ' + filePath);

                return;
            }

            replaceTokensInFile(filePath, regex, options);
        });
    }
    catch (err)
    {
        logger.error(err.message);
    }
}

run();

/* underscore
 > npm install --save underscore

var _ = require("underscore");
var json = '[{"user": "a", "age": 20}, {"user": "b", "age": 30}, {"user": "c", "age": 40}]';
var users = JSON.parse(json);
var filtered = _.where(users, {user: "a"});
// => [{user: "a", age: 20}]

*/
