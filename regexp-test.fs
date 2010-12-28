\ regexp test

charclass [bl-]   blanks +class '- +char
charclass [0-9(]  '( +char '0 '9 ..char

: telnum ( addr u -- flag )
    (( {{ ` (  \( \d \d \d \) ` ) || \( \d \d \d \) }}  blanks c?
    \( \d \d \d \) [bl-] c?
    \( \d \d \d \d \) {{ \$ || -\d }} )) ;

: ?tel ( addr u -- ) telnum
    IF  '( emit \1 type ." ) " \2 type '- emit \3 type ."  succeeded"
    ELSE \0 type ."  failed " THEN ;

: ?tel-s ( addr u -- ) ?tel ."  should succeed" space depth . cr ;
: ?tel-f ( addr u -- ) ?tel ."  should fail" space depth . cr ;

." --- Telephone number match ---" cr
s" (123) 456-7890" ?tel-s
s" (123) 456-7890 " ?tel-s
s" (123)-456 7890" ?tel-f
s" (123) 456 789" ?tel-f
s" 123 456-7890" ?tel-s
s" 123 456-78909" ?tel-f

: telnum2 ( addr u -- flag )
    (( // {{ [0-9(] -c? || \^ }}
    {{ ` (  \( \d \d \d \) ` ) || \( \d \d \d \) }}  blanks c?
    \( \d \d \d \) [bl-] c?
    \( \d \d \d \d \) {{ \$ || -\d }} )) ;

: ?tel2 ( addr u -- ) telnum2
    IF   '( emit \1 type ." ) " \2 type '- emit \3 type ."  succeeded"
    ELSE \0 type ."  failed " THEN  cr ;
." --- Telephone number search ---" cr
s" blabla (123) 456-7890" ?tel2
s" blabla (123) 456-7890 " ?tel2
s" blabla (123)-456 7890" ?tel2
s" blabla (123) 456 789" ?tel2
s" blabla 123 456-7890" ?tel2
s" blabla 123 456-78909" ?tel2
s" (123) 456-7890" ?tel2
s"  (123) 456-7890 " ?tel2
s" a (123)-456 7890" ?tel2
s" la (123) 456 789" ?tel2
s" bla 123 456-7890" ?tel2
s" abla 123 456-78909" ?tel2

." --- Number extraction test ---" cr

charclass [0-9,./:]  '0 '9 ..char ', +char '. +char '/ +char ': +char

: ?num
    (( // \( {++ [0-9,./:] c? ++} \) ))
    IF  \1 type  ELSE  \0 type ."  failed"  THEN   cr ;

s" 1234" ?num
s" 12,345abc" ?num
s" foobar12/345:678.9abc" ?num
s" blafasel" ?num

." --- String test --- " cr

: ?string
    (( // \( {{ =" foo" || =" bar" || =" test" }} \) ))
    IF  \1 type  cr THEN ;
s" dies ist ein test" ?string
s" foobar" ?string
s" baz bar foo" ?string
s" Hier kommt nichts vor" ?string

." --- longer matches test --- " cr

: ?foos
    (( \( {** =" foo" **} \) ))
    IF  \1 type  ELSE  \0 type ."  failed"  THEN  cr ;

: ?foobars
    (( // \( {** =" foo" **} \) \( {++ =" bar" ++} \) ))
    IF  \1 type ', emit \2 type  ELSE  \0 type ."  failed"  THEN  cr ;

: ?foos1
    (( // \( {+ =" foo" +} \) \( {++ =" bar" ++} \) ))
    IF  \1 type ', emit \2 type  ELSE  \0 type ."  failed"  THEN  cr ;

s" foobar" ?foos
s" foofoofoobar" ?foos
s" fofoofoofofooofoobarbar" ?foos
s" bla baz bar" ?foos
s" foofoofoo" ?foos

s" foobar" ?foobars
s" foofoofoobar" ?foobars
s" fofoofoofofooofoobarbar" ?foobars
s" bla baz bar" ?foobars
s" foofoofoo" ?foobars

s" foobar" ?foos1
s" foofoofoobar" ?foos1
s" fofoofoofofooofoobarbar" ?foos1
s" bla baz bar" ?foos1
s" foofoofoo" ?foos1

\ backtracking on decissions

: ?aab ( addr u -- flag )
   (( {{ =" aa" || =" a" }} {{ =" ab" || =" a" }} )) ;
s" aab" ?aab 0= [IF] .( aab failed!) cr [THEN]

\ simple replacement test
 
." --- simple replacement test ---" cr

: delnum  ( addr u -- addr' u' )   s// \d >> s" " //g ;
: test-delnum  ( addr u addr' u' -- )
   2swap delnum 2over 2over str= 0= IF
      ." test-delnum: got '" type ." ', expected '" type ." '"
   ELSE  2drop 2drop ." passed" cr  THEN ;
s" 0"  s" " test-delnum
s" 00"  s" " test-delnum
s" 0a"  s" a" test-delnum
s" a0"  s" a" test-delnum
s" aa"  s" aa" test-delnum

: delcomment  ( addr u -- addr' u' )  s// ` # {** .? **} >> s" " //g ;
s" hello # test " delcomment type cr
: delparents  ( addr u -- addr' u' )  s// ` ( {* .? *} ` ) >> s" ()" //g ;
s" delete (test) and (another test) " delparents type cr

\ replacement tests

." --- replacement tests ---" cr

: hms>s ( addr u -- addr' u' )
  s// \( \d \d \) ` : \( \d \d \) ` : \( \d \d \) >>
  \1 s>number drop 60 *
  \2 s>number drop + 60 *
  \3 s>number drop + 0 <<# 's' hold #s #> #>> //g ;

s" bla 12:34:56 fasel 00:01:57 blubber" 2dup type hms>s
."  -> " 2dup type
s" bla 45296s fasel 117s blubber" str= [IF] .(  ok) [ELSE] .(  failed) [THEN] cr

: hms>s,del() ( addr u -- addr' u' )
  s// {{ \( \d \d \) ` : \( \d \d \) ` : \( \d \d \)
         >> \1 s>number drop 60 *
            \2 s>number drop + 60 *
            \3 s>number drop + 0 <# 's' hold #s #> <<
         || ` ( // ` ) >> <<" ()"
      }} LEAVE //s ;

s" (bla) 12:34:56 (fasel) 00:01:57 (blubber)" 2dup type hms>s,del() ."  -> " type cr

\ more tests from David Kühling

: underflow1  ( c-addr u -- flag )
   (( {{
         {{ ` - || }} \d
         || \d
      }} )) ;
s" -1dummy" underflow1 0= [IF] ." failed" cr [ELSE] ." underflow1 passed" cr [THEN]

: underflow2  ( -- )
   (( \( {{ \s {** \s **} 
	 || =" /*" // =" */"
	 || =" //" {** \d **} }} \) )) ;
s" /*10203030203030404*/   " underflow2 0= [IF] ." failed" cr [ELSE] ." underflow2 passed: " \1 type cr [THEN]
pad 0 underflow2 [IF] ." failed" cr [THEN]

charclass [*] '* +char
charclass [*/] '* +char '/ +char

: underflow3  ( -- )
   ((
      =" /*"
      \( {** {{ [*] -c? || ` * [*/] -c? }} **} \)
      {++ ` * ++} ` /
   )) ;

s" /*10203030203030404*/   " underflow3 0= [IF] ." failed" cr [ELSE] ." underflow3 passed: " \1 type cr [THEN]

: underflow4  ( -- )
   (( \( {{ {** \d **} || {** \d **} }} \d \) )) ;

s" 0  " underflow4 0= [IF] ." failed" cr [ELSE] ." underflow4 passed: " \1 type cr [THEN]

script? [IF] bye [THEN]
