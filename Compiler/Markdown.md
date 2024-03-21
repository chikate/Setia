## MARKDOWN syntax / Computer text to display

# MainPage
```
title("dragos")

Main-Page-Layout
    NAV_BAR
        "hello" # label/text
    LEFT_PANNEL
        "left-pannel"
    RIGHT_PANNEL
        "right-pannel"
    VIEW
        LoginComponent
    VIEW_TOP_BAR
        "view-top-bar"
    VIEW_BOTTOM_BAR
        "view-bottom-bar"
```

# Main-Page-Layout
```
bg-color(20,20,20,1) vertical # background
    bg-color(0.5) bg-blur(0.2) horizontal anchor(top) padding(2)            
        slot(NAV_BAR)
    bg-color(0.6) horizontal # content
        bg-color(0.1) vertical anchor(left)                                     
            slot(LEFT_PANNEL)
        bg-color(0.3) vertical anchor(right)                                    
            slot(RIGHT_PANNEL)
        bg-color(0.2) vertical  # content center pannel
            bg-color(0.5) anchor(bottom)                                          
                slot(VIEW_BOTTOM_BAR)
            slot(VIEW)                                                            
            bg-color(0.5) anchor(top)                                            
                slot(VIEW_TOP_BAR)
```

# Login-Component
```
bg-color(20,20,20,0.9) padding(2) vertical content-fill-x
    input-text hint("username") key-enter(submitAccount) bg-color(blue,0.5) hover-bg-color(blue,0.6) icon(input)
    input-text hint("password") key-enter(submitAccount) bg-color(blue,0.5) hover-bg-color(blue,0.6)
```

# Extra
```
bg-color(20,20,20,0.9) padding(2rel,10px,3cm) absolute content-fill / button click(do_this) bg-color(blue,0.5) hover-bg-color(blue,0.6) fill-x / label(test de cerere)

bg-color(20,20,20,0.9) padding(2rel,10px,3cm) absolute content-fill # items-top, items-top-left, items-left-top / items-lt, items-rt, items-ct, items-lc, items-center, items-rc, items-br, items-bc, items-bl
    click(do_this) bg-color(blue,0.5) hover-bg-color(blue,0.6) 
        label(test de cerere)
```