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

![](http://chart.googleapis.com/chart?cht=tx&chs=80&chl=%5Cpi%20%3D%204%20%5Ccdot%20%5Carctan1%20%3D%204%20-%20%5Cfrac%7B4%20%5E%203%7D3%20%2B%20%5Cfrac%7B4%20%5E%205%7D3%20-%20%5Cfrac%7B4%20%5E%207%7D3%20%2B%20%5Cldots%20%3D%20%5Csum%5E%5Cinfty_%7Bk%3D1%7D%20%5Cfrac%7B2%20%5Ccdot%20(-1)%20%5E%20%7Bk%20%2B%201%7D%7D%7Bk-0.5%7D&.png)

### Candidate Algorithm: Chudnovsky Algorithm

![](https://chart.apis.google.com/chart?cht=tx&chs=80&chl=%5Cfrac%7B1%7D%7B%5Cpi%7D%20%3D%2012%20%5Csum%5E%5Cinfty_%7Bk%3D0%7D%20%5Cfrac%7B(-1)%5Ek%20(6k)!%20(13591409%20%2B%20545140134k)%7D%7B(3k)!(k!)%5E3%20640320%5E%7B3k%20%2B%203%2F2%7D%7D&.png)

### Candidate Algorithm: Ramanujan's Formula

![](https://chart.apis.google.com/chart?cht=tx&chs=105&chl=%5Cfrac%7B1%7D%7B%5Cpi%7D%20%3D%20%5Cfrac%7B2%5Csqrt%7B2%7D%7D%7B9801%7D%20%5Csum%5E%5Cinfty_%7Bk%3D0%7D%20%5Cfrac%7B(4k)!%20(1103%20%2B%2026390k)%7D%7B(k!%5C%20%5Ccdot%5C%20396%5Ek)%5E4%7D&.png)

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
