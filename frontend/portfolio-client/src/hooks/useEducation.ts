import { useEffect, useState } from 'react';
import { educationService } from '../services/educationService';
import type { Education } from '../types';

export function useEducation() {
  const [education, setEducation] = useState<Education[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let mounted = true;
    educationService.getAll().then((data) => {
      if (mounted) {
        setEducation(data);
        setLoading(false);
      }
    });
    return () => {
      mounted = false;
    };
  }, []);

  return { education, loading };
}
