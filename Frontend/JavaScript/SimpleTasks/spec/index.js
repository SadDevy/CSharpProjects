//1 Реализовать add(x)(y)
var add = function (x) {
    if (typeof x !== 'number')
        throw new TypeError('Первый параметр должен быть числом.');

    return (y) => {
        if (typeof y !== 'number')
            throw new TypeError('Второй параметр должен быть числом.');

        return x + y;
    }
}

exports.add = add;

//2  Функция принимает массив чисел, она должна вернуть массив функций, 
//которая возвращает числа из массива и индекс этого числа в исходном массиве.
function numbersToFunctions(numbers) {
    if (!(numbers instanceof Array))
        throw new TypeError('Параметр должен быть массивом.');

    numbers.map(number => {
        if (typeof number !== 'number')
            throw new TypeError('Элементы массива должны быть числами.');
    });

    return numbers.map(number =>
        () => {
            var obj = {
                id: numbers.indexOf(number),
                value: number
            };

            return obj;
        });
}

exports.numbersToFunctions = numbersToFunctions;

//3 Реализовать реверс строки
function lineReverse(line) {
    if (typeof line !== 'string')
        throw new TypeError('Параметр должен быть строкой.');

    return line.split('')
        .reverse()
        .join('');
}

exports.lineReverse = lineReverse;

//4 Сконвертировать массив чисел в массив функций, возвращающие числа
function numbersToFunctionsReturnedNumbers(numbers) {
    if (!(numbers instanceof Array))
        throw new TypeError('Параметр должен быть массивом.');

    numbers.map(function (number) {
        if (typeof number !== 'number')
            throw new TypeError('Элементы массива должны быть числами.');
    });

    var result =
        numbers.map(number =>
            () => number);

    return result;
}

exports.numbersToFunctionsReturnedNumbers = numbersToFunctionsReturnedNumbers;

//5 Результат выражений и решение по шагам
//1. +!{}[0] = 1
//+!{}[0] => 
//  {}[0] = undefined
//  !{}[0] = true
// +!{}[0] = 1
//
//2. !+[0] = true
//!+[0] =>
//  +[0] = 0
//  !+[0] = true
//
//3. !-[1] = false
//!-[1] =>
//  -[1] = -1
//  !-1 = false

//6. Реализовать calc

function add_6(x, y) {
    return x + y;
}

function mul(x, y) {
    return x * y;
}

function calc(x) {
    return function curried(...args) {
        var lastElement = args[args.length - 1];
        if (typeof lastElement === 'function'
            && lastElement.length <= args.length) {
            var fn = lastElement;

            var result = x;
            for (var i = 0; i < args.length - 1; i++) {
                result = fn(result, args[i]);
            }

            return result;
        }
        else {
            return (...args2) => {
                return curried.apply(this, args.concat(args2));
            }
        }
    }
}

exports.calc = calc;
exports.add_6 = add_6;
exports.mul = mul;

//7. Написать функцию, которая вернёт клон переданного объекта
function clone(obj) {
    if (!(obj instanceof Object))
        throw new TypeError('Параметр должен быть объектом.');

    var result = {};
    Object.assign(result, obj);

    return result;
}

exports.clone = clone;

//8. Что напечатает и как поправить.
function Point() {
    this.x = 20;
    this.getX = function () { return this.x; }
}

var a = new Point();
var f = a.getX.bind(a); //!!!

//Реализовать глубокое копирование
function deepCopy(obj) {
    if (typeof obj !== 'object')
        return obj;

    var result = {};
    for (var key in obj) {
        var property = obj[key];
        if (typeof property !== 'object'
            && typeof property !== 'function')
            result[key] = property;
        else {
            if (property instanceof Array) {
                result[key] = property.map(value => {
                    if (typeof value !== 'object'
                        && typeof value !== 'function') {
                        return value;
                    }
                    else {
                        return deepCopy(value);
                    }
                });
            }
            else {
                result[key] = deepCopy(property);
            }
        }
    }

    return result;
}

exports.deepCopy = deepCopy;

//forEach
Array.prototype.newForEach = function (callback) {
    if (typeof callback !== 'function')
        throw new TypeError('Параметр должен быть функцией.');

    for (var element of this) {
        callback(element, this.indexOf(element), this);
    }
};

exports.newForEach = this.newForEach;

//filter
Array.prototype.newFilter = function (callback) {
    if (typeof callback !== 'function')
        throw new TypeError('Параметр должен быть функцией.');

    var result = [];
    for (var element of this) {
        if (callback(element, this.indexOf(element), this) === true)
            result.push(element);
    }

    return result;
};

exports.newFilter = this.newFilter;

//each
Array.prototype.newEach = function (callback) {
    if (typeof callback !== 'function')
        throw new TypeError('Параметр должен быть функцией.');

    for (let element of this) { //!!!
        if (callback(element, this.indexOf(element), this) === false)
            return false;
    }

    return true;
};

exports.newEach = this.newEach;

//map
Array.prototype.newMap = function (callback) {
    if (typeof callback !== 'function')
        throw new TypeError('Параметр должен быть функцией.');

    var result = [];
    for (var element of this) {
        var resultCallback = callback(element, this.indexOf(element), this);
        result.push(resultCallback);
    }

    return result;
};

exports.newMap = this.newMap;

//reduce
Array.prototype.newReduce = function (callback) {
    if (typeof callback !== 'function')
        throw new TypeError('Параметр должен быть функцией.');

    var result = this[0];
    for (var i = 1; i < this.length; i++) {
        result = callback(result, this[i], i, this);
    }

    return result;
};

exports.newReduce = this.newReduce;