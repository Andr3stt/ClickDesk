/**
 * Configuração do Babel para React Native com Expo
 * 
 * Este arquivo configura o transpilador Babel para processar
 * o código JavaScript/JSX da aplicação.
 */

module.exports = function(api) {
  api.cache(true);
  
  return {
    presets: ['babel-preset-expo'],
    plugins: []
  };
};
