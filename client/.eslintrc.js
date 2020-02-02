module.exports = {
  root: true,
  env: {
    node: true,
  },
  extends: [
    'plugin:vue/recommended',
    '@vue/airbnb',
    // '@vue/prettier',
  ],
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'import/extensions': 'off',
    'vue/html-closing-bracket-newline': 'off',
    'import/prefer-default-export': 'off',
    'no-mixed-operators': 'off',
  },
  parserOptions: {
    parser: 'babel-eslint',
  },
};
