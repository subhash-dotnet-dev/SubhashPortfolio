import { useEffect, useState } from 'react';
import { projectService } from '../services/projectService';
import type { Project } from '../types';

export function useProjects() {
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let mounted = true;
    projectService.getAll().then((data) => {
      if (mounted) {
        setProjects(data);
        setLoading(false);
      }
    });
    return () => {
      mounted = false;
    };
  }, []);

  return { projects, loading };
}
