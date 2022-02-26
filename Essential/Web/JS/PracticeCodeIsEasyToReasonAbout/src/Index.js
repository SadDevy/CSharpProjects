var multiplyByThree = function (array) {
    var multiplier = 3;
    
    // Handle undefined or null inputs
    if (!array) {
      throw new Error("Argument is undefined or null");
    }
    
    // Handle non-array inputs
    if (!Array.isArray(array)) {
      throw new Error("Argument is not an array");
    }
    
    var result = [];
    for (var i = 0; i < array.length; i++) {
      if (typeof array[i] !== "number") {
        throw new Error("Array must contain valid numbers only");
      } else {
        result[i] = array[i] * multiplier;
      }
    }
    
    return result;
  }