lexer grammar SPARQLLexer;

SPACE : WS -> channel(HIDDEN);
COMMENT : '#' ~[\r\n]* -> channel(HIDDEN);

TRUE : T R U E;
FALSE : F A L S E;
DISTINCT : D I S T I N C T;
NOT : N O T;
IN : I N;
STR : S T R;
LANG : L A N G;
LANGMATCHES : L A N G M A T C H E S;
DATATYPE : D A T A T Y P E;
BOUND : B O U N D;
IRI : I R I;
URI : U R I;
BNODE : B N O D E;
RAND : R A N D;
ABS : A B S;
CEIL : C E I L;
FLOOR : F L O O R;
ROUND : R O U N D;
CONCAT : C O N C A T;
STRLEN : S T R L E N;
UCASE : U C A S E;
LCASE : L C A S E;
ENCODE_FOR_URI : E N C O D E '_' F O R '_' U R I;
CONTAINS : C O N T A I N S;
STRSTARTS : S T R S T A R T S;
STRENDS : S T R E N D S;
STRBEFORE : S T R B E F O R E;
STRAFTER : S T R A F T E R;
YEAR : Y E A R;
MONTH : M O N T H;
DAY : D A Y;
HOURS : H O U R S;
MINUTES : M I N U T E S;
SECONDS : S E C O N D S;
TIMEZONE : T I M E Z O N E;
TZ : T Z;
NOW : N O W;
UUID : U U I D;
STRUUID : S T R U U I D;
MD5 : M D '5';
SHA1 : S H A '1';
SHA256 : S H A '256';
SHA384 : S H A '384';
SHA512 : S H A '512';
COALESCE : C O A L E S C E;
IF : I F;
STRLANG : S T R L A N G;
STRDT : S T R D T;
SameTerm : S A M E T E R M;
IsIRI : I S I R I;
IsURI : I S U R I;
IsBLANK : I S B L A N K;
IsLITERAL : I S L I T E R A L;
IsNUMERIC : I S N U M E R I C;
COUNT : C O U N T;
SUM : S U M;
MIN : M I N;
MAX : M A X;
AVG : A V G;
SAMPLE : S A M P L E;
GROUP_CONCAT : G R O U P '_' C O N C A T;
SEPARATOR : S E P A R A T O R;
REGEX : R E G E X;
SUBSTR : S U B S T R;
REPLACE : R E P L A C E;
EXISTS : E X I S T S;
SELECT : S E L E C T;
REDUCED : R E D U C E D;
AS : A S;
WHERE : W H E R E;
OPTIONAL : O P T I O N A L;
GRAPH : G R A P H;
SERVICE : S E R V I C E;
SILENT : S I L E N T;
BIND : B I N D;
VALUES : V A L U E S;
UNDEF : U N D E F;
A_ : A;
UNION : U N I O N;
MINUS : M I N U S;
FILTER : F I L T E R;
GROUP : G R O U P;
BY : B Y;
HAVING : H A V I N G;
ASC : A S C;
DESC : D E S C;
LIMIT : L I M I T;
OFFSET : O F F S E T;
ORDER : O R D E R;
DEFAULT : D E F A U L T;
NAMED : N A M E D;
ALL : A L L;
USING : U S I N G;
INSERT : I N S E R T;
DELETE : D E L E T E;
WITH : W I T H;
DATA : D A T A;
COPY : C O P Y;
TO : T O;
MOVE : M O V E;
CREATE : C R E A T E;
LOAD : L O A D;
INTO : I N T O;
CLEAR : C L E A R;
DROP : D R O P;
BASE : B A S E;
PREFIX : P R E F I X;
CONSTRUCT : C O N S T R U C T;
FROM : F R O M;
DESCRIBE : D E S C R I B E;
ASK : A S K;

CARET2 : '^^';
CARET : '^';
OPAR : '(';
CPAR : ')';
COMMA : ',';
OR : '||';
AND : '&&';
EQ : '=';
NEQ : '!=';
GT : '>';
LT : '<';
GTE : '>=';
LTE : '<=';
ADD : '+';
SUB : '-';
MUL : '*';
DIV : '/';
EXCL : '!';
SCOL : ';';
OBRACE : '{';
CBRACE : '}';
DOT : '.';
OBRACK : '[';
CBRACK : ']';
PIPE : '|';
QMARK : '?';

/// [139]    IRIREF : '<' ([^<>"{}|^`\]-[#x00-#x20])* '>'
IRIREF : '<' ~[<>"{}|^`\\\u0000-\u0020]* '>';

/// [140]    PNAME_NS : PN_PREFIX? ':'
PNAME_NS : PN_PREFIX? ':';

/// [141]    PNAME_LN : PNAME_NS PN_LOCAL
PNAME_LN : PNAME_NS PN_LOCAL;

/// [142]    BLANK_NODE_LABEL : '_:' ( PN_CHARS_U | [0-9] ) ((PN_CHARS|'.')* PN_CHARS)?
BLANK_NODE_LABEL : '_:' ( PN_CHARS_U | [0-9] ) ( ( PN_CHARS|'.' )* PN_CHARS )?;

/// [143]    VAR1 : '?' VARNAME
VAR1 : '?' VARNAME;

/// [144]    VAR2 : '$' VARNAME
VAR2 : '$' VARNAME;

/// [145]    LANGTAG : '@' [a-zA-Z]+ ('-' [a-zA-Z0-9]+)*
LANGTAG : '@' [a-zA-Z]+ ( '-' [a-zA-Z0-9]+ )*;

/// [146]    INTEGER : [0-9]+
INTEGER : [0-9]+;

/// [147]    DECIMAL : [0-9]* '.' [0-9]+
DECIMAL : [0-9]* '.' [0-9]+;

/// [148]    DOUBLE : [0-9]+ '.' [0-9]* EXPONENT | '.' ([0-9])+ EXPONENT | ([0-9])+ EXPONENT
DOUBLE
 : [0-9]+ '.' [0-9]* EXPONENT
 | '.' [0-9]+ EXPONENT
 | [0-9]+ EXPONENT
 ;

/// [149]    INTEGER_POSITIVE : '+' INTEGER
INTEGER_POSITIVE : '+' INTEGER;

/// [150]    DECIMAL_POSITIVE : '+' DECIMAL
DECIMAL_POSITIVE : '+' DECIMAL;

/// [151]    DOUBLE_POSITIVE : '+' DOUBLE
DOUBLE_POSITIVE : '+' DOUBLE;

/// [152]    INTEGER_NEGATIVE : '-' INTEGER
INTEGER_NEGATIVE : '-' INTEGER;

/// [153]    DECIMAL_NEGATIVE : '-' DECIMAL
DECIMAL_NEGATIVE : '-' DECIMAL;

/// [154]    DOUBLE_NEGATIVE : '-' DOUBLE
DOUBLE_NEGATIVE : '-' DOUBLE;

/// [156]    STRING_LITERAL1 : "'" ( ([^#x27#x5C#xA#xD]) | ECHAR )* "'"
STRING_LITERAL1 : '\'' ( ~[\u0027\u005C\u000A\u000D] | ECHAR )* '\'';

/// [157]    STRING_LITERAL2 : '"' ( ([^#x22#x5C#xA#xD]) | ECHAR )* '"'
STRING_LITERAL2 : '"' ( ~[\u0022\u005C\u000A\u000D] | ECHAR )* '"';

/// [158]    STRING_LITERAL_LONG1 : "'''" ( ( "'" | "''" )? ( [^'\] | ECHAR ) )* "'''"
STRING_LITERAL_LONG1 : '\'\'\'' ( ( '\'' | '\'\'' )? ( ~['\\] | ECHAR ) )* '\'\'\'';

/// [159]    STRING_LITERAL_LONG2 : '"""' ( ( '"' | '""' )? ( [^"\] | ECHAR ) )* '"""'
STRING_LITERAL_LONG2 : '"""' ( ( '"' | '""' )? ( ~["\\] | ECHAR ) )* '"""';

/// [161]    NIL : '(' WS* ')'
NIL : '(' WS* ')';

/// [163]    ANON : '[' WS* ']'
ANON : '[' WS* ']';

// Fall through rule
UNEXPECTED_CHAR : . ;

/// [155]    EXPONENT : [eE] [+-]? [0-9]+
fragment EXPONENT : [eE] [+-]? [0-9]+;

/// [160]    ECHAR : '\' [tbnrf\"']
fragment ECHAR : '\\' [tbnrf"'];

/// [162]    WS : #x20 | #x9 | #xD | #xA
fragment WS : [\u0020\u0009\u000D\u000A];

/// [164]    PN_CHARS_BASE : [A-Z] | [a-z] | [#x00C0-#x00D6] | [#x00D8-#x00F6] | [#x00F8-#x02FF] | [#x0370-#x037D] | [#x037F-#x1FFF] | [#x200C-#x200D] | [#x2070-#x218F] | [#x2C00-#x2FEF] | [#x3001-#xD7FF] | [#xF900-#xFDCF] | [#xFDF0-#xFFFD] | [#x10000-#xEFFFF]
fragment PN_CHARS_BASE
 : [a-zA-Z]
 | [\u00C0-\u00D6]
 | [\u00D8-\u00F6]
 | [\u00F8-\u02FF]
 | [\u0370-\u037D]
 | [\u037F-\u1FFF]
 | [\u200C-\u200D]
 | [\u2070-\u218F]
 | [\u2C00-\u2FEF]
 | [\u3001-\uD7FF]
 | [\uF900-\uFDCF]
 | [\uFDF0-\uFFFD]
 | [\u{10000}-\u{EFFFF}]
 ;

/// [165]    PN_CHARS_U : PN_CHARS_BASE | '_'
fragment PN_CHARS_U : PN_CHARS_BASE | '_';

/// [166]    VARNAME : ( PN_CHARS_U | [0-9] ) ( PN_CHARS_U | [0-9] | #x00B7 | [#x0300-#x036F] | [#x203F-#x2040] )*
fragment VARNAME : ( PN_CHARS_U | [0-9] ) ( PN_CHARS_U | [0-9\u00B7\u0300-\u036F\u203F-\u2040] )*;

/// [167]    PN_CHARS : PN_CHARS_U | '-' | [0-9] | #x00B7 | [#x0300-#x036F] | [#x203F-#x2040]
fragment PN_CHARS
 : PN_CHARS_U
 | [\-0-9\u00B7\u0300-\u036F\u203F-\u2040]
 ;

/// [168]    PN_PREFIX : PN_CHARS_BASE ((PN_CHARS|'.')* PN_CHARS)?
fragment PN_PREFIX : PN_CHARS_BASE ((PN_CHARS|'.')* PN_CHARS)?;

/// [169]    PN_LOCAL : (PN_CHARS_U | ':' | [0-9] | PLX ) ((PN_CHARS | '.' | ':' | PLX)* (PN_CHARS | ':' | PLX) )?
fragment PN_LOCAL : (PN_CHARS_U | ':' | [0-9] | PLX ) ((PN_CHARS | '.' | ':' | PLX)* (PN_CHARS | ':' | PLX) )?;

/// [170]    PLX : PERCENT | PN_LOCAL_ESC
fragment PLX : PERCENT | PN_LOCAL_ESC;

/// [171]    PERCENT : '%' HEX HEX
fragment PERCENT : '%' HEX HEX;

/// [172]    HEX : [0-9] | [A-F] | [a-f]
fragment HEX : [0-9A-Fa-f];

/// [173]    PN_LOCAL_ESC : '\' ( '_' | '~' | '.' | '-' | '!' | '$' | '&' | "'" | '(' | ')' | '*' | '+' | ',' | ';' | '=' | '/' | '?' | '#' | '@' | '%' )
fragment PN_LOCAL_ESC : '\\' [_~.\-!$&'()*+,;=/?#@%];

fragment A : [aA];
fragment B : [bB];
fragment C : [cC];
fragment D : [dD];
fragment E : [eE];
fragment F : [fF];
fragment G : [gG];
fragment H : [hH];
fragment I : [iI];
fragment J : [jJ];
fragment K : [kK];
fragment L : [lL];
fragment M : [mM];
fragment N : [nN];
fragment O : [oO];
fragment P : [pP];
fragment Q : [qQ];
fragment R : [rR];
fragment S : [sS];
fragment T : [tT];
fragment U : [uU];
fragment V : [vV];
fragment W : [wW];
fragment X : [xX];
fragment Y : [yY];
fragment Z : [zZ];
