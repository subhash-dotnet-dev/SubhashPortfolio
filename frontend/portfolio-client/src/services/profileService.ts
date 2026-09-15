import api from './api';
import type { Profile } from '../types';

export const profileService = {
  get: async (): Promise<Profile | null> => {
    try {
      const { data } = await api.get<Profile>('/Profile');
      return data;
    } catch (err) {
      console.error('Failed to fetch profile:', err);
      return null;
    }
  },
};
