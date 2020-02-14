export const digitize = (val) => {
  try {
    const numbersOnly = val.match(/\d+/g).map(Number).join('');
    return parseInt(numbersOnly, 10);
  } catch (e) {
    console.error('digitize', e);
    return null;
  }
};

export const digitLength = (val) => {
  try {
    const numbersOnly = val.match(/\d+/g).map(Number).join('');
    const int = parseInt(numbersOnly, 10);
    return int.toString().length;
  } catch (e) {
    console.error('digitLength', e);
    return null;
  }
};
