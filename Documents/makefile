.PHONY: thesis.pdf all clean

# All .tex files
SRC=$(wildcard *.tex)

all: $(SRC)
	latexmk -pdf -pdflatex="pdflatex -interactive=nonstopmode" -use-make $^

# Custom rule. Allows you to call `make thesis` and will run this for thesis.tex
%:
	latexmk -pdf -pdflatex="pdflatex -interactive=nonstopmode" -use-make $*.tex

clean:
	latexmk -CA


