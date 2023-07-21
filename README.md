# Pi Calculator

## Release Status

Beta 1: (~~Not released yet~~) In development, uses a Machin-like formula, with the Taylor series.

Alpha 4 (fail): Epic fail. It got discontinued after a switch back to Alpha 3.

Alpha 3 (disc): Discontinued progress available in SVN.

Alpha 2: Progress bar doesn't work.

## Summary

This project calculates π (PI) and allows you to save it to a file. It is back in development into **the current algorithm**.

## Algorithms

### Current Algorithm: Machin-like/Taylor formula

Same as below, but 4·arctan(1⁄5) - arctan(1⁄239)

### Development Algorithm: Gregory and Leibniz formula

$$ \pi = 4 \cdot \arctan1 = 4 - \frac{4 ^ 3}3 + \frac{4 ^ 5}3 - \frac{4 ^ 7}3 + \ldots = \sum^\infty_{k=1} \frac{2 \cdot (-1) ^ {k + 1}}{k-0.5} $$

### Candidate Algorithm: Chudnovsky Algorithm

$$ \frac{1}{\pi} = 12 \sum^\infty_{k=0} \frac{(-1)^k (6k)! (13591409 + 545140134k)}{(3k)!(k!)^3 640320^{3k + 3/2}} $$

### Candidate Algorithm: Ramanujan's Formula

$$ \frac{1}{\pi} = \frac{2\sqrt{2}}{9801} \sum^\infty_{k=0} \frac{(4k)! (1103 + 26390k)}{(k!\ \cdot\ 396^k)^4} $$

## System Requirements

1. It **requires** _Windows_ for _.NET Framework 2.0_. Too bad if you use Linux, UNIX, or any other operating system. Make your own C++ port if you want.
1. Memory:
    - 256 MB to 512 MB for limit at 16M digits
    - 1024 MB+ recommended

## Install

There is no need to install portable programs like this software. Just extract and run the standalone executable.

## Licence

See COPYRIGHT(.txt) for more information.

The code is licenced under the GNU GPL3, allowing you to reuse and adapt the code.

Content, such as the logo, is licensed under the Creative Commons Attribution-ShareAlike 3.0. You must attribute the work without suggesting that this project endorses your work. In addition to that, if you alter, transform, or build upon this work, you may distribute the resulting work only under the same or similar license to this one.
