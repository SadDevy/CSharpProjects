const {
    add,
    numbersToFunctions,
    lineReverse,
    numbersToFunctionsReturnedNumbers,
    calc,
    add_6,
    mul,
    clone,
    deepCopy,
    newForEach,
    newFilter,
    newEach,
    newMap,
    newReduce
} = require('./index');

describe('Test add', function () {
    it('should return 3', function () {
        const expected = 3;
        const actual = add(1)(2);
        expect(actual).toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            add('')(1);
        }).toThrowError(TypeError);
    });

    it('should throw TypeError', function () {
        expect(function () {
            add(1)('');
        }).toThrowError(TypeError);
    });
});

describe('Test numbers array to functions with id', function () {
    it('should return functions array', function () {
        const numbers = [1, 2, 3, 4, 5];
        const actual = numbersToFunctions(numbers);

        actual.map(value =>
            expect(typeof value).toEqual('function'));
    });

    it('functions should return numbers with codes', function () {
        const numbers = [1, 2, 3];
        const expected = [
            {
                id: 0, value: 1
            },
            {
                id: 1, value: 2
            },
            {
                id: 2, value: 3
            }
        ];

        const functions = numbersToFunctions(numbers);
        const actual = functions.map(func =>
            func());

        expect(actual).toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            numbersToFunctions(1, 2, 3);
        }).toThrowError(TypeError);
    });

    it('should throw TypeError', function () {
        expect(function () {
            numbersToFunctions([1, 2, 'line']);
        }).toThrowError(TypeError);
    });
});

describe('Test line reverse', function () {
    it('should return reversed line', function () {
        const line = 'reverse';

        const expected = 'esrever';
        const actual = lineReverse(line);

        expect(actual).toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            lineReverse(1);
        }).toThrowError(TypeError);
    });
});

describe('Test function which return functions array with numbers instead of numbers', function () {
    it('should return functions array', function () {
        const numbers = [1, 2, 3, 4, 5];
        const actual = numbersToFunctionsReturnedNumbers(numbers);

        actual.map(value =>
            expect(typeof value).toEqual('function'));
    });

    it('functions should return numbers', function () {
        const numbers = [1, 2, 3];

        const functions = numbersToFunctionsReturnedNumbers(numbers);
        const actual = functions.map(func =>
            func());

        expect(actual).toEqual(numbers);
    });

    it('should throw TypeError', function () {
        expect(function () {
            numbersToFunctionsReturnedNumbers(1, 2, 3);
        }).toThrowError(TypeError);
    });

    it('should throw TypeError', function () {
        expect(function () {
            numbersToFunctionsReturnedNumbers([1, 2, 'line']);
        }).toThrowError(TypeError);
    });
});

describe('Test calc', function () {
    it('should return calculated value by add and mul', function () {
        const result = calc(1)(2)(3)(4)(5);

        const firstExpected = 15;
        const secondExpected = 120;
        const firstActual = result(add_6);
        const secondActual = result(mul);

        expect(firstActual).toEqual(firstExpected);
        expect(secondActual).toEqual(secondExpected);
    });
});

describe('Test clone object', function () {
    it('should be cloned', function () {
        const expected = {
            Name: 'Name',
            Info: {
                FirstPoint: 1,
                AdditionalInfo: {
                    FirstPoint: 1
                }
            }
        };

        const actual = clone(expected);

        expect(actual).toEqual(expected);
    });

    it('cloned shouldn\' be changed', function () {
        const expected = {
            Name: 'Name',
            Info: {
                FirstPoint: 1,
                AdditionalInfo: {
                    FirstPoint: 1
                }
            }
        };

        const actual = clone(expected);
        actual.Name = 'Without name'

        expect(actual).not.toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            clone(1);
        }).toThrowError(TypeError);
    });
});

describe('Test deep copy', function () {
    it('should be copy', function () {
        const expected = {
            name: 'name',
            values:
            {
                name: 'name1',
                fn: function () { return 1; },
                values: {
                    name: 'name2',
                    value: 3,
                    array: [1, { id: 1 }, 3, 4, function () { return 1; }]
                }
            }
        };

        const actual = deepCopy(expected);

        expect(actual).toEqual(expected);
    });

    it('copy shouldn\'t be changed', function () {
        const expected = {
            name: 'name',
            values:
            {
                name: 'name1',
                fn: function () { return 1; },
                values: {
                    name: 'name2',
                    value: 3,
                    array: [1, { id: 1 }, 3, 4, function () { return 1; }]
                }
            }
        };

        const actual = deepCopy(expected);
        actual.name = '1';
        actual.values.name = '2';
        actual.values.values.array[1].id = 3;

        expect(actual).not.toEqual(expected);
    });

    it('should return the current', function () {
        const expected = 1;
        const actual = deepCopy(1);

        expect(actual).toEqual(expected);
    });
});

describe('Test forEach', function () {
    it('should calculate', function () {
        const array = [1, 2, 3];
        const expected = 6;

        var actual = 0;
        array.newForEach(function (item) {
            actual += item;
        });

        expect(actual).toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            [1, 2, 3].newForEach(1);
        }).toThrowError(TypeError);
    });
});

describe('Test filter', function () {
    it('should return filtered', function () {
        const array = [1, 2, 3];
        const expected = [2, 3];

        var actual = array.newFilter(function (item) {
            return item > 1;
        });

        expect(actual).toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            [1, 2, 3].newFilter(1);
        }).toThrowError(TypeError);
    });
});

describe('Test each', function () {
    it('should return true', function () {
        const array = [1, 2, 3];
        const expected = true;

        var actual = array.newEach(function (item) {
            return item > 0;
        });

        expect(actual).toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            [1, 2, 3].newEach(1);
        }).toThrowError(TypeError);
    });
});


describe('Test map', function () {
    it('should return numbers incremented by 1', function () {
        const array = [1, 2, 3];
        const expected = [2, 3, 4];

        var actual = array.newMap(number => ++number);

        expect(actual).toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            [1, 2, 3].map(1);
        }).toThrowError(TypeError);
    });
});

describe('Test reduce', function () {
    it('should return numbers sum', function () {
        const array = [1, 2, 3];
        const expected = 6;

        var actual = array.newReduce((sum, current) => sum + current);

        expect(actual).toEqual(expected);
    });

    it('should throw TypeError', function () {
        expect(function () {
            [1, 2, 3].newReduce(1);
        }).toThrowError(TypeError);
    });
});