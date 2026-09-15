import { useEffect, useState } from 'react';
import { skillService } from '../services/skillService';
import type { Skill } from '../types';

export function useSkills() {
  const [skills, setSkills] = useState<Skill[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let mounted = true;
    skillService.getAll().then((data) => {
      if (mounted) {
        setSkills(data);
        setLoading(false);
      }
    });
    return () => {
      mounted = false;
    };
  }, []);

  return { skills, loading };
}
