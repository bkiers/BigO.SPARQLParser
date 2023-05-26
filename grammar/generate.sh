#!/usr/bin/env sh

java -jar antlr-4.13.0-complete.jar \
  -visitor \
  -Dlanguage=CSharp ./*.g4 \
  -package BigO.SPARQLParser.Parser

mv ./*.cs ../BigO.SPARQLParser/Parser

rm *.tokens
rm *.interp