#! xbigforth
\ automatic generated code
\ do not edit

also editor also minos also forth

component class minos-about
public:
  button ptr theseus-ok
 ( [varstart] )  ( [varend] ) 
how:
  : params   DF[ 0 ]DF s" Minos Help" ;
class;

minos-about implements
 ( [methodstart] )  ( [methodend] ) 
  : widget  ( [dumpstart] )
              $0 $1 *hfill $0 $1 *vfill glue new 
               icon" icons/minos" icon new 
              $0 $1 *hfill $0 $1 *vfill glue new 
            #3 habox new
            X" Theseus 10sep2000" text-label new 
            X" based on MINOS" text-label new 
            X" (c) 1997-2000 by Bernd Paysan" text-label new 
              X" Theseus is available under " text-label new 
              ^^ S[ gpl-about open ]S ( MINOS ) X" GPL" button new 
              $10 $1 *hfilll $10 $1 *vfil glue new 
            #3 habox new
          #5 vabox new #2 borderbox
        #1 habox new
            $10 $1 *hfill $10 $1 *vfill glue new 
            ^^ S[ close ]S ( MINOS ) X"  OK " button new  ^^bind theseus-ok
            $10 $1 *hfill $10 $1 *vfill glue new 
          #3 habox new #1 vskips
        #1 vabox new
      #2 vabox new
    ( [dumpend] ) ;

class;

previous previous previous
