# Nimator CouchBase plugin

This project is a plugin for [Nimator](https://github.com/omniaretail/nimator). Nimator is a ligthweight Monitor framework and this puglin gives Nimator the ability to make REST calls to get information a CouchBase Cluster.

## The Plugin

This plugin is composed by:

- Checkers to make REST calls
- L (A Language to check properties from C# Classes with Logical Functions)
- CouchBase Classes for General and Bucket Statitics

### Components
#### Checkers
#### L
##### BNF for Language L
```
**<L>** ::= <ArithmeticFunction> <LogicalFunction> <ArithmeticFunction>
**<ArithmeticFunction>** ::= <PrefixOperators> | <PrefixOperators> <InfixOperations> <ArithmeticFunction>
**<PrefixOperators>** ::= <Var> | <Long> | <Double>						
**<Var>** ::= 1*<letter>
**<Long>** ::= 1*<digit>
**<Double>** ::= <digit> (".") <digit>
**<LogicalFunction>** ::= = | != | < | > | <= | >=
**<InfixOperations>** ::= + | - | * | \
```
### Usage
Just fork the repo and run _ConsoleNimatorCouchBase_. Configure you Coachbase authentication settings. They are default settings for notifications using L. Feel free to trysome for you.
