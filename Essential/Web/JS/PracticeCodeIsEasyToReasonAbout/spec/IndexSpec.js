describe('#multiplyByThree', () => {
    it('should return the correct result', () => {
      var array = [1, 2, 3]
      expect(multiplyByThree(array)).toEqual([3, 6, 9]);
    });
    
    it('should throw an error for invalid inputs', () => {
      var array = ['blah blah']
      expect(() => multiplyByThree(array)).toThrowError();
    });
  });