import api from './api';
import type { ContactFormData, ContactResponse } from '../types';

export const contactService = {
  submit: async (data: ContactFormData): Promise<ContactResponse> => {
    try {
      await api.post('/Contact', data);
      return {
        success: true,
        message: 'Message sent successfully. I will get back to you soon!',
      };
    } catch (err) {
      console.error('Failed to send message:', err);
      return {
        success: false,
        message: 'Failed to send message. Please try again.',
      };
    }
  },
};
