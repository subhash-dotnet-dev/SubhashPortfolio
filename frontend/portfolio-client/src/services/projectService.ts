import api from './api';
import type { Project } from '../types';

export const projectService = {
  getAll: async (): Promise<Project[]> => {
    try {
      const { data } = await api.get<Project[]>('/Projects');
      return data;
    } catch (err) {
      console.error('Failed to fetch projects:', err);
      return [];
    }
  },
};
