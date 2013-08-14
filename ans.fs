\ Char+ chars postpone compile, char [char] key        20apr92py

: ANS ;

' key Alias ekey                ' key? Alias ekey?

| User pending  pending off
| : char? ( key -- flag )
    $FF and 0<>  kbshift @ 0>= and ;
: key? ( -- flag )  pending c@ IF  true exit  THEN
  ekey? dup  IF  drop ekey dup pending c! char?
                 dup 0= IF  0 pending c!  THEN  THEN ;
: key  key? IF  pending c@  0 pending c! exit  THEN
  BEGIN  ekey dup $100 u>=  WHILE  drop  REPEAT  ;
: ekey>char ( ekey -- char t / f )  dup $100 u>=
  dup 0= IF nip THEN ;
\ Simpler: : key ekey $FF and ;

\ obsolescent
User span
: expect  accept span ! ;
: convert  -1 >number drop ;
\ end obsolescent

\needs environment? include environ.fs
\needs locals| include locals.fs

: synonym ( newname oldname -- )  lastcfa push
  Header -2 allot last @ dup  c@ $20 and
  0= IF  0 A, $20 flag!  THEN
  bl word find  dup 0> IF  immediate  THEN
  1 and 0= IF  restrict  THEN
  swap (name> ! reveal ;
