# Fire-Emblem-Engage-Coloring
A C# program that takes Fire Emblem Engage red scale sprites and palettes to color sprites. Made by [Eclogia](https://www.reddit.com/user/eclogia).

# Usage
Provide the following folders' path as command-line arguments in order: raw (red scale) sprites, palettes, output

# Limitations
Either the palette I received are extracted wrong, or there is an extra step I don't know the nature of, which makes in-game sprites lighter-colored than those output by this program. It's still better than the red scale version of the sprites in terms of community usability, but know that this is not totally accurate to the game.

# How it works
A Fire Emblem Engage palette file consists of a 512x1 px-sized image with each color on the image (taking 2 pixels) corresponding to a value of red channel in the "raw" sprite image in order. The leftmost color corresponds to a value of 0, while the rightmost one corresponds to 255.

# Dependencies
Uses [SkiaSharp](https://github.com/mono/SkiaSharp), which is has an MIT licence.