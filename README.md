*** THIS IS STILL A WORK IN PROGRESS ***

# BOG.ClipboardMunger
A Windows tray utility with a collection of methods to take clipboard text content, and replace it with some change.  Very useful for software deveopers, and for people
who need to do a lot of standard data "munging", or quick actions to transform text.

Contains a basic set of scripts I have developed and used extensively over time.

If you would like to add scripts of your own, add them to the project BOG.ClipboardMunger.Common project 
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

... and a lot more.



