"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const tl = require("azure-pipelines-task-lib/task");
const fs = require("fs");
const iconv = require("iconv-lite");
const jschardet = require("jschardet");
const path = require("path");
const ENCODING_AUTO = 'auto';
const ENCODING_ASCII = 'ascii';
const ENCODING_UTF_7 = 'utf-7';
const ENCODING_UTF_8 = 'utf-8';
const ENCODING_UTF_16LE = 'utf-16le';
const ENCODING_UTF_16BE = 'utf-16be';
const ENCODING_WIN1252 = 'windows1252';
const ENCODING_ISO_8859_1 = 'iso88591';
const ACTION_WARN = 'warn';
const ACTION_FAIL = 'fail';
const XML_ESCAPE = /[<>&'"]/g;
const JSON_ESCAPE = /["\\/\b\f\n\r\t]/g;
class NullLogger {
    debug(message) { }
    info(message) { }
    warn(message) { }
    error(message) { }
}
var LogLevel;
(function (LogLevel) {
    LogLevel[LogLevel["Debug"] = 0] = "Debug";
    LogLevel[LogLevel["Info"] = 1] = "Info";
    LogLevel[LogLevel["Warn"] = 2] = "Warn";
    LogLevel[LogLevel["Error"] = 3] = "Error";
    LogLevel[LogLevel["Off"] = 255] = "Off";
})(LogLevel || (LogLevel = {}));
class Logger {
    constructor(level) {
        this._level = level;
    }
    debug(message) {
        this.log(LogLevel.Debug, message);
    }
    info(message) {
        this.log(LogLevel.Info, message);
    }
    warn(message) {
        this.log(LogLevel.Warn, message);
    }
    error(message) {
        this.log(LogLevel.Error, message);
    }
    log(level, message) {
        // always log debug to system debug
        if (level === LogLevel.Debug)
            tl.debug(message);
        // always set task result on error
        if (level === LogLevel.Error)
            tl.setResult(tl.TaskResult.Failed, message);
        if (level < this._level)
            return;
        switch (level) {
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
var logger = new NullLogger();
var forJson = function (match) {
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
var forXML = function (match) {
    switch (match) {
        case '<': return '&lt;';
        case '>': return '&gt;';
        case '&': return '&amp;';
        case '\'': return '&apos;';
        case '"': return '&quot;';
    }
    return match;
};
var mapEncoding = function (encoding) {
    switch (encoding) {
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
};
var getEncoding = function (filePath) {
    let buffer = fs.readFileSync(filePath, { flag: 'r' });
    let charset = jschardet.detect(buffer);
    switch (charset.encoding) {
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
};
var replaceTokensInFile = function (filePath, regex, options) {
    logger.info('replacing tokens in: ' + filePath);
    // ensure encoding
    let encoding = options.encoding;
    if (options.encoding === ENCODING_AUTO)
        encoding = getEncoding(filePath);
    logger.debug('using encoding: ' + encoding);
    // read file and replace tokens
    let content = iconv.decode(fs.readFileSync(filePath), encoding);
    content = content.replace(regex, (match, name) => {
        let value = tl.getVariable(name);
        if (!value) {
            if (options.keepToken)
                value = match;
            else
                value = '';
            let message = 'variable not found: ' + name;
            switch (options.actionOnMissing) {
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
        let escapeType = options.escapeType;
        if (escapeType === 'auto') {
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
};
var mapLogLevel = function (level) {
    switch (level) {
        case "normal":
            return LogLevel.Info;
        case "detailed":
            return LogLevel.Debug;
        case "off":
            return LogLevel.Off;
    }
    return LogLevel.Info;
};
function run() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            // load inputs
            let root = tl.getPathInput('rootDirectory', false, true);
            let tokenPrefix = tl.getInput('tokenPrefix', true);
            tokenPrefix = tokenPrefix.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&');
            let tokenSuffix = tl.getInput('tokenSuffix', true);
            tokenSuffix = tokenSuffix.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&');
            let options = {
                encoding: mapEncoding(tl.getInput('encoding', true)),
                keepToken: tl.getBoolInput('keepToken', true),
                actionOnMissing: tl.getInput('actionOnMissing', true),
                writeBOM: tl.getBoolInput('writeBOM', true),
                emptyValue: tl.getInput('emptyValue', false),
                escapeType: tl.getInput('escapeType', false),
                escapeChar: tl.getInput('escapeChar', false),
                charsToEscape: tl.getInput('charsToEscape', false),
                verbosity: tl.getInput('verbosity', true)
            };
            logger = new Logger(mapLogLevel(options.verbosity));
            let targetFiles = [];
            tl.getDelimitedInput('targetFiles', '\n', true).forEach((x) => {
                if (x)
                    x.split(',').forEach((y) => {
                        if (y)
                            targetFiles.push(y.trim());
                    });
            });
            // initialize task
            let regex = new RegExp(tokenPrefix + '((?:(?!' + tokenSuffix + ').)*)' + tokenSuffix, 'gm');
            logger.debug('pattern: ' + regex.source);
            // process files
            tl.findMatch(root, targetFiles).forEach(filePath => {
                if (!tl.exist(filePath)) {
                    logger.error('file not found: ' + filePath);
                    return;
                }
                replaceTokensInFile(filePath, regex, options);
            });
        }
        catch (err) {
            logger.error(err.message);
        }
    });
}
run();
