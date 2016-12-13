set version=1.6.2


pushd Release

rem create archives
rem "c:\Program Files\7-Zip\7z" a -tzip -mx=9 DvrServerSetup-%version%.zip *
"c:\Program Files\7-Zip\7z" a -t7z -mx=9 Log2Console-%version%.7z *
copy /b /y "C:\Program Files\7-Zip\7zs.sfx" + ..\config.txt + Log2Console-%version%.7z Log2Console-%version%.exe

rem Move to destination
rem move DvrServerSetup-%version%.zip ..\
move Log2Console-%version%.7z ..\
move Log2Console-%version%.exe ..\
popd


pause
