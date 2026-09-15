import { useEffect, useState } from 'react';
import { useProfile } from '../../hooks/useProfile';
import { useSocialLinks } from '../../hooks/useSocialLinks';
import {
  ArrowUp,
  Mail,
  Phone,
  MapPin,
  Heart,
  Sparkles,
  ArrowRight,
  Download,
  Briefcase,
  GraduationCap,
  Code2,
  Home as HomeIcon,
  Send,
  User,
  FolderOpen,
  MessageSquare,
} from 'lucide-react';
import { FaGithub, FaLinkedin, FaInstagram, FaFacebook, FaWhatsapp } from 'react-icons/fa';
import { SiLeetcode, SiHackerrank } from 'react-icons/si';
import '../../styles/footer.css';

const iconMap = {
  1: FaGithub,
  2: FaLinkedin,
  3: SiLeetcode,
  4: SiHackerrank,
  5: FaInstagram,
  6: FaFacebook,
};

const QUICK_LINKS = [
  { label: 'Home', href: '#home', icon: HomeIcon },
  { label: 'About', href: '#about', icon: User },
  { label: 'Skills', href: '#skills', icon: Code2 },
  { label: 'Experience', href: '#experience', icon: Briefcase },
  { label: 'Projects', href: '#projects', icon: FolderOpen },
  { label: 'Education', href: '#education', icon: GraduationCap },
  { label: 'Contact', href: '#contact', icon: MessageSquare },
];

export default function Footer() {
  const { profile } = useProfile();
  const { links } = useSocialLinks();
  const [showTop, setShowTop] = useState(false);

  const year = new Date().getFullYear();
  const name = profile?.fullName || 'Subhash Yadav';
  const role = profile?.title || '.NET Full Stack Developer';
  const email = profile?.email || 'subhash.dev79@gmail.com';
  const phone = profile?.phone || '+91 9572003492';
  const location = profile?.location || 'Ameerpet, Hyderabad, India';

  useEffect(() => {
    const onScroll = () => setShowTop(window.scrollY > 600);
    onScroll();
    window.addEventListener('scroll', onScroll, { passive: true });
    return () => window.removeEventListener('scroll', onScroll);
  }, []);

  return (
    <footer className="ft">

      <div className="ft-inner">

        {/* ============ TOP GRID ============ */}
        <div className="ft-top-grid">

          {/* ─── COLUMN 1: Brand ─── */}
          <div className="ft-brand">
            <a href="#home" className="ft-brand-logo" aria-label="Subhash Yadav">
              <span className="ft-brand-mark">
                <span className="ft-brand-initials">SY</span>
                <span className="ft-brand-dot" />
              </span>
              <span className="ft-brand-text">
                <span className="ft-brand-name">
                  Subhash <span className="ft-brand-accent">Yadav</span>
                </span>
                <span className="ft-brand-role">{role}</span>
              </span>
            </a>

            <p className="ft-brand-bio">
              Building modern web applications across frontend, backend, APIs, and databases.
            </p>

            <div className="ft-brand-actions">
              <a
                href="https://wa.me/919572003492?text=Hi%20Subhash%2C%20I%20saw%20your%20portfolio!"
                target="_blank"
                rel="noopener noreferrer"
                className="ft-brand-btn ft-brand-btn--primary"
              >
                <FaWhatsapp size={14} />
                <span>Let's Talk</span>
                <ArrowRight size={13} />
              </a>
              <a href="/resume/Subhash_Yadav_Resume.pdf" download className="ft-brand-btn ft-brand-btn--ghost">
                <Download size={14} />
                <span>Resume</span>
              </a>
            </div>
          </div>

          {/* ─── COLUMN 2: Quick Links ─── */}
          <div className="ft-links">
            <h4 className="ft-col-title">
              <span className="ft-col-title-mark" />
              Quick Links
            </h4>
            <ul className="ft-links-list">
              {QUICK_LINKS.map((link) => {
                const Icon = link.icon;
                return (
                  <li key={link.href}>
                    <a href={link.href} className="ft-link">
                      <Icon size={12} className="ft-link-icon" />
                      <span className="ft-link-text">{link.label}</span>
                      <ArrowRight size={11} className="ft-link-arrow" />
                    </a>
                  </li>
                );
              })}
            </ul>
          </div>

          {/* ─── COLUMN 3: Contact Info ─── */}
          <div className="ft-contact">
            <h4 className="ft-col-title">
              <span className="ft-col-title-mark" />
              Get In Touch
            </h4>

            <ul className="ft-contact-list">
              <li>
                <a href={`mailto:${email}`} className="ft-contact-item">
                  <span className="ft-contact-icon ft-contact-icon--email">
                    <Mail size={14} />
                  </span>
                  <span className="ft-contact-body">
                    <span className="ft-contact-lbl">Email</span>
                    <span className="ft-contact-val">{email}</span>
                  </span>
                </a>
              </li>

              <li>
                <a href={`tel:${phone.replace(/\s+/g, '')}`} className="ft-contact-item">
                  <span className="ft-contact-icon ft-contact-icon--phone">
                    <Phone size={14} />
                  </span>
                  <span className="ft-contact-body">
                    <span className="ft-contact-lbl">Phone</span>
                    <span className="ft-contact-val">{phone}</span>
                  </span>
                </a>
              </li>

              <li>
                <span className="ft-contact-item">
                  <span className="ft-contact-icon ft-contact-icon--loc">
                    <MapPin size={14} />
                  </span>
                  <span className="ft-contact-body">
                    <span className="ft-contact-lbl">Location</span>
                    <span className="ft-contact-val">{location}</span>
                  </span>
                </span>
              </li>
            </ul>

            {/* Status pill */}
            <div className="ft-status-pill">
              <span className="ft-status-dot" />
              <span>Available for work</span>
            </div>
          </div>

        </div>


        {/* ============ BOTTOM BAR ============ */}
        <div className="ft-bottom">
          <div className="ft-crafted">
            <span className="ft-crafted-icon">
              <Sparkles size={14} />
            </span>
            <span>Crafted with</span>
            <span className="ft-crafted-heart">
              <Heart size={12} fill="currentColor" />
            </span>
            <span>by</span>
            <span className="ft-crafted-name">{name}</span>
          </div>

          <div className="ft-socials">
            {links.map((l) => {
              const Icon = iconMap[l.platform as keyof typeof iconMap];
              if (!Icon) return null;
              const slug = l.displayName.toLowerCase().replace(/\s+/g, '');
              return (
                <a
                  key={l.id}
                  href={l.url}
                  target="_blank"
                  rel="noopener noreferrer"
                  className={`ft-social ft-social--${slug}`}
                  aria-label={l.displayName}
                  title={l.displayName}
                >
                  <Icon size={14} />
                </a>
              );
            })}
            <a
              href="https://wa.me/919572003492?text=Hi%20Subhash%2C%20I%20saw%20your%20portfolio!"
              target="_blank"
              rel="noopener noreferrer"
              className="ft-social ft-social--whatsapp"
              aria-label="WhatsApp"
              title="Chat on WhatsApp"
            >
              <FaWhatsapp size={14} />
            </a>
          </div>

          <div className="ft-meta">
            <span className="ft-copy-text">
              <span className="ft-copy-year">© {year}</span>
              <span className="ft-sep" />
              <span>All rights reserved</span>
            </span>
            <span className="ft-made">
              <span className="ft-made-flag" />
              <span className="ft-made-text">Made in India</span>
            </span>
          </div>
        </div>

      </div>

      {/* ============ SCROLL TO TOP ============ */}
      <button
        className={`ft-top ${showTop ? 'is-visible' : ''}`}
        onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })}
        aria-label="Back to top"
      >
        <ArrowUp size={16} />
      </button>

    </footer>
  );
}