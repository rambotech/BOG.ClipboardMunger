*** THIS IS STILL A WORK IN PROGRESS ***

# BOG.ClipboardMunger
A Windows tray utility with a collection of methods to take clipboard text content, and replace it with some change. Very useful for software deveopers, and for people
who need to do a lot of standard quick actions to transform text.

The project contains a basic set of scripts I have developed and used extensively over time. If you would like to add scripts of your own, add them to the project BOG.ClipboardMunger.Common project 
in the MethodLibrary folder, and implement the IClipboardMungerProvider interface.
- Set the GroupName and MethodName properties to unique values (appears in the list of available scripts).
- Use neither any dispose method, nor connectivity.  The script must simply be data-in/data-out (atomic).  Anything more complex should not go in here.
- Set any optional parameters the user must provide (and optional default values for them).
- Create any Examples you want to demonstrate the method's use.

See the Base64Encode method for a parameterless example, and MakeGuid for one with parameters.

That's it.

Standard scripts included are:

- Base64/URL/Html Encode/Decode
- Double line spacing to single line spacing
- SQL Script construction from lines of Tab Separated Values.  (i.e. copy directly from sheet to SSMS, and bypass a longer SQL import process)
- JSON construction from lines of Tab Separated Values.
- JSON Beautify or minimize.
- SHA1 / MD5 hash value
- JSON and XML reformat for readability.
- String splits and joins.
- Phoneticizing a password: ABc123 == ALPHA BRAVO charlie One Two Three 
- Prefix / Suffix to each line
- string.Format() for tab-separated values on lines.
- ToUpper() / ToLower() / ToHex()
- Generate a GUID (with options)
- Classic Hex view representation of the clipboard content, e.g. 

```
    0000: 59 6f 75 72 20 6d 75 6c 74 69 2d 6c 69 6e 65 20  | Your multi-line 
    0010: 03 20 74 65 78 74 20 67 6f 65 73 20 68 65 72 65  | . text goes here
    0020: 0d 0a 4c 69 6b 65 20 74 68 69 73 0d 0a           | ..Like this..   
```

- A not-so-classic dehexify to take a hex sequence like

```
    00:ab:42:76:31:86:ad:67:5c:ce:9d:4e:a1:3e:97:
    18:47:06:eb:04:8b:8c:f5:17:6a:99:ba:c9:3e:c0:
    b5:9f:c2:e5:d1:e4:55:11:1e:2d:bb:78:f2:38:63:

```

and return its value as Base64 (in case the resulting string contains binary).

... and a lot more.

Version History:
2023-07-23: 1.1.2.5
- SqlInsertFromGrid. output cleanup

2023-07-23: 1.1.2.4
- Add GridPadFixed
- SqlInsertFromGrid. refactor for multi-VALUE lines (wip)

2023-06-02: 1.1.2.3
- migrated more string methods:
  - UniqueLines, SqlUpdateFromGrid
- now supports examples

2023-06-02: 1.1.2.2
- migrated more string methods

2023-06-02: 1.1.2.1
- Add a migrated function
- Show version in the window title

2023-06-02: 1.1.2.0
- Add a number of migrated functions
- Clear text from error tab when munge action starts.

2023-05-17: 1.1.1.0
- Add a number of functions

2022-11-02: 1.1.0.0
- Add Dehexify function

2016: 1.0.0.0
- New project for static classes instead of dynamic scripts in XML files.

