import { useEffect, useState } from 'react';
import { createPortal } from 'react-dom';
import { useTheme } from '../../context/ThemeContext';
import {
  Sun, Moon, Send, Menu, X,
  Home as HomeIcon, User, Code2, Briefcase,
  Rocket, GraduationCap, Mail,
} from 'lucide-react';
import { FaGithub, FaLinkedin, FaInstagram, FaFacebook } from 'react-icons/fa';
import { SiLeetcode, SiHackerrank } from 'react-icons/si';
import '../../styles/navbar.css';

const NAV_LINKS = [
  { label: 'Home', href: '#home', icon: HomeIcon },
  { label: 'About', href: '#about', icon: User },
  { label: 'Skills', href: '#skills', icon: Code2 },
  { label: 'Experience', href: '#experience', icon: Briefcase },
  { label: 'Projects', href: '#projects', icon: Rocket },
  { label: 'Education', href: '#education', icon: GraduationCap },
  { label: 'Contact', href: '#contact', icon: Mail },
];

const SOCIALS = {
  github: 'https://github.com/subhash-dotnet-dev',
  linkedin: 'https://www.linkedin.com/in/subhash-dotnet-dev/',
  leetcode: 'https://leetcode.com/subhashyadav',
  hackerrank: 'https://www.hackerrank.com/profile/subhash_dev',
  instagram: 'https://www.instagram.com/visionsubhash/',
  facebook: 'https://www.facebook.com/VisionSubhash',
  location: 'Hyderabad',
};

export default function Navbar() {
  const { theme, toggleTheme } = useTheme();
  const [menuOpen, setMenuOpen] = useState(false);
  const [active, setActive] = useState('home');
  const [scrolled, setScrolled] = useState(false);

  useEffect(() => {
    const onScroll = () => setScrolled(window.scrollY > 20);
    onScroll();
    window.addEventListener('scroll', onScroll, { passive: true });
    return () => window.removeEventListener('scroll', onScroll);
  }, []);

  useEffect(() => {
    const ids = NAV_LINKS.map((l) => l.href.replace('#', ''));
    const observers: IntersectionObserver[] = [];
    ids.forEach((id) => {
      const el = document.getElementById(id);
      if (!el) return;
      const obs = new IntersectionObserver(
        (entries) => entries.forEach((e) => {
          if (e.isIntersecting) setActive(id);
        }),
        { rootMargin: '-40% 0px -55% 0px' }
      );
      obs.observe(el);
      observers.push(obs);
    });
    return () => observers.forEach((o) => o.disconnect());
  }, []);

  useEffect(() => {
    if (!menuOpen) return;
    const onEsc = (e: KeyboardEvent) => {
      if (e.key === 'Escape') setMenuOpen(false);
    };
    window.addEventListener('keydown', onEsc);
    return () => window.removeEventListener('keydown', onEsc);
  }, [menuOpen]);

  const closeMenu = () => setMenuOpen(false);

  const mobileMenu = menuOpen
    ? createPortal(
        <div className="nb-mobile">
          <div className="nb-mobile-top">
            <div className="nb-mobile-logo-group">
              <div className="nb-logo-mark-wrap">
                <div className="nb-logo-mark-glow" />
                <div className="nb-logo-mark">
                  <span className="nb-logo-initials">SY</span>
                </div>
                <span className="nb-logo-dot" />
              </div>
              <div className="nb-mobile-logo-text">
                <span className="nb-mobile-logo-name">
                  Subhash <span className="nb-logo-name-accent">Yadav</span>
                </span>
                <span className="nb-mobile-logo-role">.NET Full Stack Developer</span>
              </div>
            </div>
            <button onClick={closeMenu} className="nb-hamburger nb-hamburger-close" aria-label="Close menu">
              <X size={22} />
            </button>
          </div>

          <div className="nb-mobile-body">
            <p className="nb-mobile-label">Navigation</p>
            <ul className="nb-mobile-list">
              {NAV_LINKS.map((link, i) => {
                const Icon = link.icon;
                const isActive = active === link.href.replace('#', '');
                return (
                  <li key={link.href}>
                    <a href={link.href} onClick={closeMenu}
                       className={`nb-mobile-item ${isActive ? 'nb-mobile-active' : ''}`}>
                      <span className="nb-mobile-icon"><Icon size={18} /></span>
                      <span className="nb-mobile-text">{link.label}</span>
                      <span className="nb-mobile-num">{String(i + 1).padStart(2, '0')}</span>
                    </a>
                  </li>
                );
              })}
            </ul>
          </div>

          <div className="nb-mobile-bottom">
            <a href="#contact" onClick={closeMenu} className="nb-mobile-cta">
              <span>Let's Talk</span>
              <Send size={18} />
            </a>
            <div className="nb-mobile-socials">
              <a href={SOCIALS.github} target="_blank" rel="noopener noreferrer" className="nb-mobile-social"><FaGithub size={17} /></a>
              <a href={SOCIALS.linkedin} target="_blank" rel="noopener noreferrer" className="nb-mobile-social"><FaLinkedin size={17} /></a>
              <a href={SOCIALS.leetcode} target="_blank" rel="noopener noreferrer" className="nb-mobile-social"><SiLeetcode size={17} /></a>
              <a href={SOCIALS.hackerrank} target="_blank" rel="noopener noreferrer" className="nb-mobile-social"><SiHackerrank size={17} /></a>
              <a href={SOCIALS.instagram} target="_blank" rel="noopener noreferrer" className="nb-mobile-social"><FaInstagram size={17} /></a>
              <a href={SOCIALS.facebook} target="_blank" rel="noopener noreferrer" className="nb-mobile-social"><FaFacebook size={17} /></a>
            </div>
            <div className="nb-mobile-footer">
              <span className="nb-mobile-status">
                <span className="nb-mobile-status-dot" />
                Available for work
              </span>
              <span>{SOCIALS.location}</span>
            </div>
          </div>
        </div>,
        document.body
      )
    : null;

  return (
    <>
      <header className="nb-wrap">
        <nav className={`nb-bar ${scrolled ? 'nb-scrolled' : ''}`}>
          <a href="#home" className="nb-logo" aria-label="Home">
            <div className="nb-logo-mark-wrap">
              <div className="nb-logo-mark-glow" />
              <div className="nb-logo-mark">
                <span className="nb-logo-initials">SY</span>
              </div>
              <span className="nb-logo-dot" />
            </div>
            <div className="nb-logo-text">
              <span className="nb-logo-name">
                Subhash <span className="nb-logo-name-accent">Yadav</span>
              </span>
              <span className="nb-logo-role">.NET Full Stack Developer</span>
            </div>
          </a>

          <div className="nb-nav">
            {NAV_LINKS.map((link) => {
              const isActive = active === link.href.replace('#', '');
              return (
                <a key={link.href} href={link.href} className={`nb-link ${isActive ? 'nb-active' : ''}`}>
                  {link.label}
                </a>
              );
            })}
          </div>

          <div className="nb-actions">
            <button onClick={toggleTheme} className="nb-switch" aria-label="Toggle theme">
              <span className="nb-switch-sun"><Sun size={14} /></span>
              <span className="nb-switch-moon"><Moon size={14} /></span>
              <span className="nb-switch-knob">
                {theme === 'dark' ? <Moon size={13} /> : <Sun size={13} />}
              </span>
            </button>
            <a href="#contact" className="nb-cta">
              <span>Let's Talk</span>
              <Send size={15} />
            </a>
            <button onClick={() => setMenuOpen(true)} className="nb-hamburger" aria-label="Open menu">
              <Menu size={20} />
            </button>
          </div>
        </nav>
      </header>

      {mobileMenu}
    </>
  );
}
