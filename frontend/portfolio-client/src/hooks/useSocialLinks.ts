import { useEffect, useState } from 'react';
import { socialLinkService } from '../services/socialLinkService';
import type { SocialLink } from '../types';

export function useSocialLinks() {
  const [links, setLinks] = useState<SocialLink[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let mounted = true;
    socialLinkService.getAll().then((data) => {
      if (mounted) {
        setLinks(data);
        setLoading(false);
      }
    });
    return () => {
      mounted = false;
    };
  }, []);

  return { links, loading };
}
