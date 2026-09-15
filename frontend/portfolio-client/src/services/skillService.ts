import api from './api';
import type { Skill } from '../types';

export const skillService = {
  getAll: async (): Promise<Skill[]> => {
    try {
      const { data } = await api.get<Skill[]>('/Skills');
      return data;
    } catch (err) {
      console.error('Failed to fetch skills:', err);
      return [];
    }
  },
};
