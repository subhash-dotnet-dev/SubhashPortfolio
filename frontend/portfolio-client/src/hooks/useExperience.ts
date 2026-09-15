import { useEffect, useState } from 'react';
import { experienceService } from '../services/experienceService';
import type { Experience } from '../types';

export function useExperience() {
  const [experiences, setExperiences] = useState<Experience[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let mounted = true;
    experienceService.getAll().then((data) => {
      if (mounted) {
        setExperiences(data);
        setLoading(false);
      }
    });
    return () => {
      mounted = false;
    };
  }, []);

  return { experiences, loading };
}
