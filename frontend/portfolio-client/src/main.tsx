/**
 * ============================================================================
 *  APPLICATION ENTRY POINT
 * ============================================================================
 *  Mounts React with ThemeProvider and renders App.
 *  @author Subhash Yadav
 * ============================================================================
 */

import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './styles/global.css';
import App from './App';
import { ThemeProvider } from './context/ThemeContext';

/* ============================================================================
 *  ROOT ELEMENT
 * ========================================================================== */

const rootElement = document.getElementById('root');

if (!rootElement) {
  throw new Error('Root element with id "root" not found. Check index.html.');
}

/* ============================================================================
 *  MOUNT APPLICATION
 * ========================================================================== */

createRoot(rootElement).render(
  <StrictMode>
    <ThemeProvider>
      <App />
    </ThemeProvider>
  </StrictMode>
);
