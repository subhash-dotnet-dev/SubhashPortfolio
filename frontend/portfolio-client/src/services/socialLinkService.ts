import api from './api';
import type { SocialLink } from '../types';

export const socialLinkService = {
  getAll: async (): Promise<SocialLink[]> => {
    try {
      const { data } = await api.get<SocialLink[]>('/social-links');
      return data;
    } catch (err) {
      console.error('Failed to fetch social links:', err);
      return [];
    }
  },
};
