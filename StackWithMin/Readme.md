# Stack With Min

## Problem

Have a stack that has the following methods or properties that all operate at O(1)

* Push 
* Pop
* Min 

## Solution

There are a few ways to solve this. Likely a Linked-List will be used. We could encode the min data in each node in the linked list, possibly increasing storage size but guaranteeing our performance. 

Possibly more efficient, but not always so, is a separate list with minimums. 