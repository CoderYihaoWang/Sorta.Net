# Sorta.Net

Sorta is a visualization tool that helps developers visualize their (comparison-based) sorting algorithms. The project is splitted into frontend(WIP) and backend, connected via a shared API schema. The backend API can be implemented in various languages, so that developers can practice and visualize sorting algorithms using their favourate language. This repository is the C# implementation of Sorta.

## What it does

This project is an API that exposes 2 endpoints:

- `GET /` returns a list of all implemented sorting algorithms, as well as their urls. For example:
```json
{
    "Bubble Sort": "https://localhost:12345/api/bubble-sort",
    "Selection Sort": "https://localhost:12345/api/selection-sort",
    "Quick Sort": "https://localhost:12345/api/quick-sort"
    // ......
}
```

- `GET /{algorithm}?data={data}` Will send the `data` (comma separated int list) to the server to be sorted using the named algorithm (one of the urls got above, for example). The server will sort the data using the algorithm, and will respond with statistics for that sort, as well as a list of steps which provides a mechanism for the frontend to visualize the sorting process. For example, the following response is returned when querying `GET /quick-sort?data=5,4,3,2,1`:
```json
{
    "unsorted":[5,4,3,2,1], // original data
    "sorted":[1,2,3,4,5],   // sorted data
    "hasCompleted":true,    // whether the sorting has completed
                            // this can be false if the algorithm generates too many steps and exceeds the limit
    "copies":33,            // number of copies happened
    "variables":1,          // number of extra variables used
    "comparisons":10,       // number of comparisons made
    "swaps":11,             // number of swaps made
                            // one swap will also contribute to 3 copies (t = a, a = b, b = t)
                            // the first time a swap happens it will increment the count of extra variables by 1, to account for the temp variable used
    "length":5,             // length of data
    "steps":[               // a list of operations made, used for visualizing
        {"type":"Compare","from":1,"to":0}, // compare data[0] with data[1]
        {"type":"Swap","from":1,"to":1},    // wap data[1] with data[1]
        {"type":"Compare","from":2,"to":0}, 
        {"type":"Swap","from":2,"to":2},
        {"type":"Compare","from":3,"to":0},
        {"type":"Swap","from":3,"to":3},
        // ...... more steps ignored
        {"type":"Swap","from":2,"to":2}
    ]
}
```

## How it works

This project aims to provide an unobstrusive way to visualize sorting algorithms.

## Add your own algorithms

