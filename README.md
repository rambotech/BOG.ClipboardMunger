# BOG.ClipboardMunger
A Windows tray utility with user-configurable scripts to take clipboard text content, and replace it with some change.  Very useful for software deveopers.  Contains a set of scripts I have developed over the years, and has a highlighting text editor for C# or Visual Basic to edit the scripts or build your own.

To use, copy text to the clipboard, then right-click on the tray icon to select a script to run.  The altered content replaces the clipboard text content, making it ready to paste.

Scripts included with the installation include:

- Base64/URL/Html Encode/Decode
- Double line spacing to single line spacing
- CSharp class fill out (hydration) from a skeletion.
- SQL Script construction from lines of Tab Separated Values.
- JSON construction from lines of Tab Separated Values.
- SHA1 / MD5 hash value
- JSON and XML reformat for readability.
- String splits and joins.
- Phoneticizing a password: ABc123 == ALPHA BRAVO charlie One Two Three 
- Prefix / Suffix to lines
- string.Format() for tab-separated values on lines.
- ToUpper() / ToLower() / ToHex()
- Hex view representation of the text content.
- Generate a GUID

... and you can add your own.

This is a work in progress: the code is being transferred from a VS 2010 project to VS 2015 Community Edition.
