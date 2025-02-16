# Calculator
A console-based calculator project written in C#.<br />This application is developed using .NET 8. Right now only for windows.


## Instalation
You do not need .NET runtime.<br />Simply unzip newest version from **Releases**, locate **Calculator.exe** file and you are good to go.

## Quick disclaimer how % works
**Scenario 1: % used as a operation**<br />
Lets say, you type **55 % 10** How it will be handled ?
- Get value1 = 55
- Get operation = "%"
- Get value2 = 10 ( isPercentage = false )
- Calculated value2 = 55 * (10 / 100) = 5.5

**Scenario 2: % used in calcultations**<br />
Lets say, you type **55 * 10%** How it will be handled ?
- Get value1 = 55
- Get operation = "*"
- Get value2 = 10% ( isPercentage = true )
- Calculated value2 = 55 * (10 / 100) = 5.5
- Call GetResult(55; 5.5, "*") which will return 55 * 5.5 = 302.5

_Why doesn't this app show 5.5 when I type 55 * 10%?_ Most calculators show 5.5 (which is 10% of 55). But my app shows 302.5 because it calculates 55 * 5.5 instead of calculating 10% of 55. App skips the step of calculating what percentage of the number you want, and goes straight to multiplying by a factor derived from the percentage. It multiplies by that factor immediately. 

Therefore, if you're looking to find a percentage of a number – for example, calculating what _10% of 55 is_ – you should use a % operator.


## TODO LIST

- [x] Basic arithmetic operations | + | - | * | ÷ |&nbsp;&nbsp; ***v.1.0***
- [x] More advenced operations | √ | % | ^ |&nbsp;&nbsp; ***v.2.1***
- [ ] Memory acces 
    - [ ] Implement | M+&nbsp; | (Add to memory)
    - [ ] Implement | M-&nbsp; | (Subtract from memory)
    - [ ] Implement | MRC | (Memory Recall)
- [ ] Sign change 
- [ ] Multi-language support 
    - [x] Polish&nbsp;&nbsp; ***v.1.0***
    - [ ] English
- [ ] Mouse support