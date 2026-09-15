import api from './api';
import type { Experience } from '../types';

export const experienceService = {
  getAll: async (): Promise<Experience[]> => {
    try {
      const { data } = await api.get<Experience[]>('/Experience');
      return data;
    } catch (err) {
      console.error('Failed to fetch experiences:', err);
      return [];
    }
  },
};
