/**
 * Configuração do Metro Bundler
 * 
 * Metro é o bundler JavaScript usado pelo React Native.
 * Esta configuração ajusta o comportamento do bundler.
 */

// Aprende sobre a API Metro: https://facebook.github.io/metro/docs/configuration
const { getDefaultConfig } = require('expo/metro-config');

const config = getDefaultConfig(__dirname);

module.exports = config;
