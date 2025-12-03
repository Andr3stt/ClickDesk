/**
 * Componente de Logo do ClickDesk
 * 
 * Exibe o logo oficial do ClickDesk com ícone
 * e texto estilizado.
 */

import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { Cores } from '../estilos/cores';

export default function LogoClickDesk({ size = 'medium', showText = true }) {
  const sizes = {
    small: { icon: 24, text: 16 },
    medium: { icon: 32, text: 22 },
    large: { icon: 48, text: 28 },
  };

  const currentSize = sizes[size] || sizes.medium;

  return (
    <View style={styles.container}>
      <View style={[styles.iconContainer, { 
        width: currentSize.icon, 
        height: currentSize.icon,
        borderRadius: currentSize.icon / 2,
      }]}>
        <Text style={[styles.iconText, { fontSize: currentSize.icon * 0.5 }]}>
          CD
        </Text>
      </View>
      {showText && (
        <Text style={[styles.text, { fontSize: currentSize.text }]}>
          ClickDesk
        </Text>
      )}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
  },
  iconContainer: {
    backgroundColor: Cores.brand,
    justifyContent: 'center',
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
    elevation: 4,
  },
  iconText: {
    color: Cores.branco,
    fontWeight: 'bold',
  },
  text: {
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
  },
});
