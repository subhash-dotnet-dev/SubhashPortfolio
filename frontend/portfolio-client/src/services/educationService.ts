import api from './api';
import type { Education } from '../types';

export const educationService = {
  getAll: async (): Promise<Education[]> => {
    try {
      const { data } = await api.get<Education[]>('/Education');
      return data;
    } catch (err) {
      console.error('Failed to fetch education:', err);
      return [];
    }
  },
};
