LOGIC syntax / Computer text to logic

- a file is a class, the file name is the class name, its variables are the parameters and its functions are the methods, any line that starts with a function call, that function will be executed when opening the file(like python)
- if the first line contains the parameter imports and has something attached to it, it transforms in an import function (if not it is as a variable?)

```
IMPORTS
    github.com/chikate/graphics,styleing,icons, # if there are multiple functions/parameters after the last / import them all
    ./src/constants.ts/CONSTANTS, # the . represents the local path
    ./envs/.env/NEGATIVE_NUMBER_LIST, # ignore the last comma if there is nothinf after it in that line
    ./envs/.env/USER
```

- variabels, they are all iniated as constants and when a user wants to change its data a new variable will be automaticali declared with the prefix var and that will be the dynamic variable, pointers by default, and it references the value in memory that is written in the file
- Memory block, has a value attached
- first word from a line will always be the name/syntax, anything after the first word will be its value without the whitespace
- every variable will be a list, if there is only one value it will be the only one in the list
- comma is the separator of the list elements

```
COMMENT_PREFIX                              #
LIST_SPLITTER                               ,

ROWS                                                5, 10, 30, 50 # integers number list declaration
DEFAULT_ROWS_INDEX	                                1 # integer number declaration
NEGATIVE_NUMBER 	                                -1 # negative integer number declaration
NEGATIVE_REAL_NR_LIST                               -1.2, -20, -15.5 # negative decimal number list declaration
API                                                 198.168.0.1:25565 # IP declaration, if there is more then one dot on the list slot it will stay a string if not it will be a decimal
EXAMPLE_OF_COMMA_AND_DOT_CONTAINING_STRING_LIST     "Text nou, pentru testare", "Text nou2, pentru testare" # text declaration everything that would be in a "" quotes will be treated as a human input string also accepted '' and `` when needed

# interface/type and list
USER_PARAMS                                 id,     name,       email,             Role             level # a simple list that represents the parameters of the USER_PARAMS variable
USERS:USER_PARAMS                           index,  dragos d,   dragos@draogs.ro,  ROLES.Admin      2
                                            index,  darius d,   darius@darius.ro   Mod
                                            index,  gabi g,     gabi@gabi.ro,      ,                3
                                            index,  none,       none@none.com
                        
ROLES:                                      Admin, Mod, Mod2 # maybe we can drop this overall

RIGHTS                                      Users.Add, Users.GetAll, Users.Update # a list of strings and at the same time parameters of the RIGHTS object 

ROLES.Admin                                 RIGHTS # setting in the ROLES object Admin parameter with the list RIGHTS 
ROLES.Mod                                   RIGHTS[1..2,5..8] # setting in the ROLES object Admin parameter with the list RIGHTS without the 3 and 4 index slot
ROLES.Mod2                                  RIGHTS[1..2,5..8]

USERS[1].id                                 1

# interface and list of interface
FILTER_PARAMS                               id,     name,          Role,            filters
FILTERS:FILTER_PARAMS                       index,  edit name,     ROLES.Admin,     1,
                                            index,  darius d,
                                            index,  gabi g,  

MATH_OPERATOR                               name,           function_name,  symbol
MATH_OPERATORS:MATH_OPERATOR                Addition,       add,            +
                                            Subtraction,    subtract,       -
                                            Multiplication, multiply,       *
                                            Division,       divide,         /
                                            Modulo,         modulo,         %
                                            Power,          power,          ^
                                            Radical,        radical,        _/
                                            Square Root,    sqr
                                            Log,            log

# single variable with interface
USER_1                        1, dragos1, dragos1@dragos.com
USER_2                        2, dragos2, dragos2@dragos.com
USER_3                        3
USER_4                        4, ,        mail@mail.com

# list of parameters from imported interface
USERS
    index, darius1, darius1@darius.com
    index, darius2, darius2@darius.com

# functions
block(default)/race/async, suspends, overrite, pure/callable 
exampleFunction action, method, body # Logic block has logic below attachet to it with ident
    for 1..20
        print ascii_elem                   # 1 ... number/ascii(string) ... 20

    for "a".."z"
        print ascii_elem                    # a ... ascii(string)/culture?(english) ... z

    for "."..","
        print ascii_elem                    # elem: ., - ,, # index: 46, 45, 44

    for english_alphabet
        print english_alphabet_elem         # a ... english ... z

    if ROWS > 20
        for ROWS, USERS
            if ROWS_elem = USERS_elem
                return log "Same value and div: {ROWS.length / USERS.length}"

        for ROWS, USERS
            if ROWS_elem params = USERS_elem params
                log "Same params"
                return log "Same params"

makeRequest action, method, body
    return fetch(API, action, method, body)
    # return fetch(API, Users/GetAll)
    # return fetch(API, Users/Add, post, CURRENT_USER)
    # return fetch(API, Users/Update, put, CURRENT_USER)
    # return fetch(API, Users/Delete?id={CURRENT_USER.id}, delete)
    
makeRequest("update","get",USERS) # Params call
makeRequest("update",,USERS) # Params call
makeRequest("update",body: USERS) # Params call
makeRequest(,,,USERS) # Params call
makeRequest(body: USERS) # Params call
```