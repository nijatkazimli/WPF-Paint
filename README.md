Mini Paint
----------

#### Lab Part

*   Task description:
    *   Main window **(2.0p)**
        *   On the window title bar a title **Mini Paint**
        *   When the application starts the window is in the maximized state, filling an entire screen
        *   Min size of the window is set to **950x250** px
        *   Font size is set to 16
    *   Layout **(2.0p)**
        *   The window is split into two parts
        *   Top part has 4 buttons, 2 on the left (**Rectangle** and **Ellipse**) and 2 on the right (**Delete** and **Random colors**)
        *   Top part background is **LightGray**
        *   The text on the buttons is wrapped to a new line if it does not fit in the button
        *   Lower part is filled with the **Canvas** control
        *   Canvas is filled with linear gradient from lower left corner of the application, to the upper right corner
        *   The gradient goes from the **Black** color, through the **DarkSlateGray** in the middle, to the **Black** color again
        *   The layout is preserved when the window is resized
    *   Displaying shapes **(2.0p)**
        *   When the application starts there are 4 random shapes displayed on the canvas (Rectangles and Ellipses)
        *   Positions, colors and sizes are randomized each time the application starts
        *   Shapes are not drawn outside of the canvas (they are clipped to a canvas area)
        *   Shapes are uniformly filled with color and have no outlines
    *   Selecting shapes **(2.0p)**
        *   When the mouse hover over a shape the cursor changes to a **Hand**
        *   Shapes can be selected and deselected by clicking on them with the right mouse button
        *   When a shape is selected it is displayed in front of all the unselected shapes
        *   Selected shape has a **White** glow effect with the radius of **50 pixels** and direction of **270 degrees**
    *   Changing Shapes **(1.0p)**
        *   When the **Delete** button is pressed, all the selected shapes are removed from the canvas
        *   When the **Random colors** button is pressed, colors of all the selected shapes are changed to new randomly generated colors
    *   Adding shapes **(3.0p)**
        *   New shapes can be added by clicking either **Rectangle** button or **Ellipse** button
        *   When one of those buttons is pressed, the cursor over the canvas changes to a **Cross**
        *   New shapes can be drawn by pressing and holding the left mouse button on the canvas and moving the mouse
        *   During drawing the mouse can be moved anywhere, also outside of the application window, and the shape continues to follow the mouse cursor
        *   When the left mouse button is released, the shape is finished and the cursor changes back to the default one
*   Remarks:
    *   It is not possible to obtain points for incomplete functionality
    *   In all doubtful and untold aspects application should behave like an example app
*   Tips:
    *   Canvas
    *   Rectangle, Ellipse
    *   Canvas.Children, Canvas.ClipToBounds
    *   Canvas.SetLeft, Canvas.SetTop, Canvas.SetZIndex
    *   Mouse.Capture
    *   LinearGradientBrush, GradientStop
    *   DropShadowEffect
