#! xbigforth
\ automatic generated code
\ do not edit

also editor also minos also forth

component class gears
public:
  early widget
  early open
  early open-app
  glcanvas ptr GLgear
 ( [varstart] ) cell var alphax
cell var alphay
cell var alphaz
cell var alphapitch
cell var alphabend
cell var alpharoll
cell var zoom
cell var speed
cell var last-time
cell var gear-time
cell var gear-task
cell var shade
cell var texting
cell var scale-x
cell var scale-y
cell var scale-z
3 cells var textures ( [varend] ) 
how:
  : open     new DF[ 0 ]DF s" Gears" open-component ;
  : open-app new DF[ 0 ]DF s" Gears" open-application ;
class;

include gears.fs
gears implements
 ( [methodstart] ) : make-gear-task recursive
  1 gear-task !  &1000 speed @ 1 max / &100 min after >r
  glgear render &100 0 DO  pause  LOOP
  glgear draw   &100 0 DO  pause  LOOP
  ['] make-gear-task self r> screen schedule ;
: draw-gears
  gear-task @ 0= IF  glgear with init-texture endwith
                     textures !+ !+ !  make-gear-task  THEN
  timer@ dup last-time @ -
  speed @ &20 */ gear-time +! last-time !
  glgear self
  alphax @ alphay @ alphaz @
  alphapitch @ alphabend @ alpharoll @
  zoom @ gear-time @ shade @ 0 max texting @ 0 max
  scale-x @ scale-y @ scale-z @
  textures @+ @+ @ draw-gear ;
: dispose  gear-task @  IF  self dpy cleanup pause gear-task off  THEN
  super dispose ; ( [methodend] ) 
  : widget  ( [dumpstart] )
            GL[ outer with draw-gears endwith ]GL ( MINOS ) ^^ S[  ]S ( MINOS ) $100 $1 *hfil $100 $1 *vfil glcanvas new  ^^bind GLgear
            ^^ &0 &360 SC[ &360 mod alphax ! ]SC ( MINOS )  TT" Rotate around X axis" hscaler new 
            ^^ &0 &360 SC[ &360 mod alphay ! ]SC ( MINOS )  TT" Rotate around Y axis" hscaler new 
            ^^ &0 &360 SC[ &360 mod alphaz ! ]SC ( MINOS )  TT" Rotate around Z axis" hscaler new 
            ^^ &35 &360 SC[ &360 mod alphapitch ! ]SC ( MINOS )  TT" Pitch" hscaler new 
            ^^ &331 &360 SC[ &360 mod alphabend ! ]SC ( MINOS )  TT" Bend" hscaler new 
            ^^ &350 &360 SC[ &360 mod alpharoll ! ]SC ( MINOS )  TT" Roll" hscaler new 
            ^^ &0 &100 SC[ zoom ! ]SC ( MINOS )  TT" Zoom" hscaler new 
            ^^ &20 &40 SC[ speed ! ]SC ( MINOS )  TT" Speed" hscaler new 
            ^^ &100 &100 SC[ scale-x ! ]SC ( MINOS )  TT" Scale X" hscaler new 
            ^^ &100 &100 SC[ scale-y ! ]SC ( MINOS )  TT" Scale Y" hscaler new 
            ^^ &100 &100 SC[ scale-z ! ]SC ( MINOS )  TT" Scale Z" hscaler new 
              ^^ TN[ 1 shade ]T[ ( MINOS )  ]TN ( MINOS ) S" Texture" rbutton new 
              ^^ TN[ 0 shade ]T[ ( MINOS )  ]TN ( MINOS ) S" Solid" rbutton new 
              ^^ TN[ 2 shade ]T[ ( MINOS )  ]TN ( MINOS ) S" Lines" rbutton new 
            &3 hartbox new vfixbox 
              ^^ TN[ 0 texting ]T[ ( MINOS )  ]TN ( MINOS ) S" xy" rbutton new 
              ^^ TN[ 1 texting ]T[ ( MINOS )  ]TN ( MINOS ) S" rphi" rbutton new 
              ^^ TN[ 2 texting ]T[ ( MINOS )  ]TN ( MINOS ) S" zphi" rbutton new 
            &3 hartbox new vfixbox 
              ^^ S[ gears open ]S ( MINOS ) S" One more" button new 
              ^^ S[ close ]S ( MINOS ) S" Close" button new 
            &2 hatbox new vfixbox  panel
          &15 vabox new
        &1 vabox new
      &1 vabox new
    ( [dumpend] ) ;
  : init  ^>^^  assign  widget 1 super init ;
class;

: main
  gears open-app
  $1 0 ?DO  stop  LOOP bye ;
script? [IF]  main  [THEN]
previous previous previous
