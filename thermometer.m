#! xbigforth
\ automatic generated code
\ do not edit

also editor also minos also forth

component class thermometer
public:
  canvas ptr temp
  vscaler ptr pos
 ( [varstart] )  ( [varend] ) 
how:
  : params   DF[ 0 ]DF X" No Title" ;
class;

thermometer implements
 ( [methodstart] )  ( [methodend] ) 
  : widget  ( [dumpstart] )
        CV[ outer self thermometer with pos get endwith
>r - >r 4 r@ 2+ steps
1 r> 1+ home! path 0 r@ to 2 0 to 0 r> negate to fill ]CV ( MINOS ) ^^ CK[ 2drop 2drop  ]CK ( MINOS ) $20 $1 *hfil $C8 $1 *vfil canvas new  ^^bind temp
        ^^ #0 #99 SC[ drop temp draw ]SC ( MINOS ) vscaler new  ^^bind pos
          $10 $1 *hfil $10 $1 *vfill glue new 
          ^^ S[ close ]S ( MINOS )  TT" Terminate application" X"  OK " button new 
          $10 $1 *hfil $10 $1 *vfill glue new 
        #3 vabox new hfixbox 
      #3 habox new #1 hskips
    ( [dumpend] ) ;
class;

: main
  thermometer open-app
  event-loop bye ;
script? [IF]  main  [THEN]
previous previous previous
