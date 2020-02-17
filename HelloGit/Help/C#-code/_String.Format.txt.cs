// ������� string.Format():
// C/c ������ ������ �������� �������, ��������� ���������� ���������� �������� ����� �������
// D/d ������������� ������, ��������� ����������� ���������� ����
// E/e ���������������� ������������� �����, ��������� ���������� ���������� �������� ����� �������
// F/f ������ ������� ����� � ������������� ������, ��������� ���������� ���������� �������� ����� �������
// G/g ������ ����� �������� �� ���� ��������: F ��� E
// N/n ����� ������ ������ ������� ����� � ������������� ������, ���������� ���������� �������� ����� �������
// P/p ������ ����������� ����� ��������� ����� � ������, ��������� ���������� ���������� �������� ����� �������
// X/x ����������������� ������ ����� 

// ��� �������������� ������ ������������ ��������� "C":
double number = 23.7;
string result = string.Format("{0:C}", number);
Console.WriteLine(result); // $ 23.7
string result2 = string.Format("{0:C2}", number);
Console.WriteLine(result2); // $ 23.70

��� �������������� ������ ��� � ������� ����������� ��� ��������� ��������:
���������  ��������  
  D        ����� ������ ����. ��������, 17 ���� 2015 �.
  d        ������� ������ ����. ��������, 17.07.2015
  F        ������ ������ ���� � �������. ��������, 17 ���� 2015 �. 17:04:43
  f        ������ ������ ���� � ������� ������ �������. ��������, 17 ���� 2015 �. 17:04
  G        ������� ������ ���� � ������ ������ �������. ��������, 17.07.2015 17:04:43
  g        ������� ������ ���� � �������. ��������, 17.07.2015 17:04
M, m       ������ ���� ������. ��������, 17 ����
O, o       ������ ��������� �������������� ���� � �������. ����� ���� � ������� � ������������ �� 
           ���������� ISO8601 � ������� "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffzzz". 
           ��������, 2015-07-17T17:04:43.4092892+03:00
R, r       ����� �� ��������. ��������, Fri, 17 Jul 2015 17:04:43 GMT
  s        ����������� ������ ���� � �������. ��������, 2015-07-17T17:04:43
  T        ������ ������ �������. ��������, 17:04:43
  t        ������� ������ �������. ��������, 17:04
  U        ������ ������������� ������ ������ ���� � �������. ��������, 17 ���� 2015 �. 17:04:43
  u        ������� ������������� ������ ������ ���� � �������. ��������, 2015-07-17 17:04:43Z
Y, y       ������ ����. ��������, ���� 2015

������� ������� ���� � ����� �� ���� ��������:
DateTime now = DateTime.Now;
Console.WriteLine("D: " + now.ToString("D"));
Console.WriteLine("d: " +  now.ToString("d"));
Console.WriteLine("F: " + now.ToString("F"));
Console.WriteLine("f: {0:f}", now);
Console.WriteLine("G: {0:G}", now);
Console.WriteLine("g: {0:g}", now);
Console.WriteLine("M: {0:M}", now);
Console.WriteLine("O: {0:O}", now);
Console.WriteLine("o: {0:o}", now);
Console.WriteLine("R: {0:R}", now);
Console.WriteLine("s: {0:s}", now);
Console.WriteLine("T: {0:T}", now);
Console.WriteLine("t: {0:t}", now);
Console.WriteLine("U: {0:U}", now);
Console.WriteLine("u: {0:u}", now);
Console.WriteLine("Y: {0:Y}", now);

// SQLIte
date(timestring, modifier, modifier, ...)
time(timestring, modifier, modifier, ...)
datetime(timestring, modifier, modifier, ...)
julianday(timestring, modifier, modifier, ...)
strftime(format, timestring, modifier, modifier, ...)

%d		day of month: 00
%f		fractional seconds: SS.SSS
%H		hour: 00-24
%j		day of year: 001-366
%J		Julian day number
%m		month: 01-12
%M		minute: 00-59
%s		seconds since 1970-01-01
%S		seconds: 00-59
%w		day of week 0-6 with Sunday==0
%W		week of year: 00-53
%Y		year: 0000-9999
%%		%

Function		Equivalent strftime()
date(...)		strftime('%Y-%m-%d', ...)
time(...)		strftime('%H:%M:%S', ...)
datetime(...)		strftime('%Y-%m-%d %H:%M:%S', ...)
julianday(...)		strftime('%J', ...)

YYYY-MM-DD
YYYY-MM-DD HH:MM
YYYY-MM-DD HH:MM:SS
YYYY-MM-DD HH:MM:SS.SSS
YYYY-MM-DDTHH:MM
YYYY-MM-DDTHH:MM:SS
YYYY-MM-DDTHH:MM:SS.SSS
HH:MM
HH:MM:SS
HH:MM:SS.SSS
now
DDDDDDDDDD

Modifiers

The time string can be followed by zero or more modifiers that alter date and/or time. Each modifier is a transformation that is applied to the time value to its left. Modifiers are applied from left to right; order is important. The available modifiers are as follows.

NNN days
NNN hours
NNN minutes
NNN.NNNN seconds
NNN months
NNN years
start of month
start of year
start of day
weekday N
unixepoch
localtime
utc

Examples

Compute the current date.

SELECT date('now');
Compute the last day of the current month.

SELECT date('now','start of month','+1 month','-1 day');
Compute the date and time given a unix timestamp 1092941466.

SELECT datetime(1092941466, 'unixepoch');
Compute the date and time given a unix timestamp 1092941466, and compensate for your local timezone.

SELECT datetime(1092941466, 'unixepoch', 'localtime');
Compute the current unix timestamp.

SELECT strftime('%s','now');
Compute the number of days since the signing of the US Declaration of Independence.

SELECT julianday('now') - julianday('1776-07-04');
Compute the number of seconds since a particular moment in 2004:

SELECT strftime('%s','now') - strftime('%s','2004-01-01 02:34:56');
Compute the date of the first Tuesday in October for the current year.

SELECT date('now','start of year','+9 months','weekday 2');
Compute the time since the unix epoch in seconds (like strftime('%s','now') except includes fractional part):

SELECT (julianday('now') - 2440587.5)*86400.0;

