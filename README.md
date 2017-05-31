# Nimator CouchBase plugin

This project is a plugin for [Nimator](https://github.com/omniaretail/nimator). Nimator is a ligthweight Monitor framework and this puglin gives Nimator the ability to make REST calls to get information a CouchBase Cluster.

## The Plugin

This plugin is composed by:

1. CouchBase Classes for General  Statistics (with the ability to make HTTP GET calls)
2. L (A Language to check properties from C# Classes with Logical Functions)

### Components
#### Checkers
I created one checker for [Couch Base General Statistics](https://developer.couchbase.com/documentation/server/current/rest-api/rest-endpoints-all.html) (/pools). With this checker you can ask any query for any boolean expression using L - A Language for boolean expression validation.

This Check settings consist in:
```
{
          "$type": "NimatorCouchBase.CouchBase.Checkers.CheckCouchBaseGeneralAttributesSettings, NimatorCouchBase",
          "Validations": {
            "$type": "NimatorCouchBase.NimatorBooster.LRuntimeObjectValidations, NimatorCouchBase",
            "Validations": {
              "$type": "System.Collections.Generic.List`1[[NimatorCouchBase.NimatorBooster.ILRuntimeObjectValidation, NimatorCouchBase]], mscorlib",
              "$values": [
                {
                  "$type": "NimatorCouchBase.NimatorBooster.LRuntimeObjectValidation, NimatorCouchBase",
                  "NotificationLevel": 20,
                  "LValidation": "StorageTotals.Ram.Total>10"
                },
                {
                  "$type": "NimatorCouchBase.NimatorBooster.LRuntimeObjectValidation, NimatorCouchBase",
                  "NotificationLevel": 40,
                  "LValidation": "StorageTotals.Ram.Total>30"
                },
                {
                  "$type": "NimatorCouchBase.NimatorBooster.LRuntimeObjectValidation, NimatorCouchBase",
                  "NotificationLevel": 30,
                  "LValidation": "StorageTotals.Ram.Total>20"
                }
              ]
            }            
          },
          "Parameters": {
            "$type": "NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.HttpCallerParameters, NimatorCouchBase",
            "HttpUrl": "http://localhost:8091/pools/default",
            "Authenticator": {
              "$type": "NimatorCouchBase.NimatorBooster.HttpCheckers.Callers.HttpAuthenticationSettings, NimatorCouchBase",
              "Username": "supertoino",
              "Password": "OcohoW*99"
            },
            "Method": 0
          }
```
Those settings can have several rules. They are interperted by order of Notification (from Higher to Lower) when one is true the process stops and a ICheckResult is returned.
#### L
L is a langauge to validate boolean expressions. It has access to variables of objects. The access is made using the variable name as is defined in the class (it's name sensitive). You can have expressions like ```StorageTotals.Ram.Total>20``` or ```StorageTotals.Ram.Used>StorageTotals.Ram.Total*0.5``` or even weird ones ```1+5*10!=StorageTotals.Ram.Total*0.5```. It doesn't support (). It has operator precedence: first multiplactions and divisions then additions and subtrations.
##### BNF for Language L
```
<L> ::= <ArithmeticFunction> <LogicalFunction> <ArithmeticFunction>
<ArithmeticFunction> ::= <PrefixOperators> | <PrefixOperators> <InfixOperations> <ArithmeticFunction>
<PrefixOperators> ::= <Var> | <Long> | <Double>						
<Var> ::= 1*<letter>
<Long> ::= 1*<digit>
<Double> ::= <digit> (".") <digit>
<LogicalFunction> ::= = | != | < | > | <= | >=
<InfixOperations> ::= + | - | * | \
```
#### Improvements and Limitations
1. I create specific C# Classes for the Couchbase statistics, however a more general aproach can be created using the ```dynamic``` object. In this way no code needs to be made in order to add more checkers when json objects are returned.
2. Improve L to accept () and conjuntions and dijunctions. Also more rules to sets would be interesting.

### Usage
Just fork the repo and run _ConsoleNimatorCouchBase_. Configure you Coachbase authentication settings. They are default settings for notifications using L. Feel free to trysome for you. There is a working example with the code submitted. You just have to change the settings regarding the CouchBase Server endpoint.
