import { useEffect, useState } from 'react';
import { useProfile } from '../../../hooks/useProfile';
import { useSocialLinks } from '../../../hooks/useSocialLinks';
import { Download, ArrowRight, MapPin, Sparkles } from 'lucide-react';
import { FaGithub, FaLinkedin, FaInstagram, FaFacebook, FaWhatsapp } from 'react-icons/fa';
import { SiLeetcode, SiHackerrank } from 'react-icons/si';
import '../../../styles/hero.css';

const iconMap = {
  1: FaGithub,
  2: FaLinkedin,
  3: SiLeetcode,
  4: SiHackerrank,
  5: FaInstagram,
  6: FaFacebook,
};

const TYPED_WORDS = [
  '.NET Full Stack Developer',
  'React + TypeScript Developer',
  'ASP.NET Core Specialist',
  'REST API Architect',
  'Problem Solver',
];

const STATS = [
  { num: '1+', label: 'Years Exp' },
  { num: '5+', label: 'Projects' },
  { num: '46+', label: 'Skills' },
];

export default function Hero() {
  const { profile } = useProfile();
  const { links } = useSocialLinks();
  const [mounted, setMounted] = useState(false);
  const [wordIndex, setWordIndex] = useState(0);
  const [typedText, setTypedText] = useState('');
  const [isDeleting, setIsDeleting] = useState(false);
  const [showCursor, setShowCursor] = useState(true);

  useEffect(() => { setMounted(true); }, []);

  useEffect(() => {
    const current = TYPED_WORDS[wordIndex];
    let t: ReturnType<typeof setTimeout>;
    if (!isDeleting && typedText === current) {
      t = setTimeout(() => setIsDeleting(true), 1800);
    } else if (isDeleting && typedText === '') {
      setIsDeleting(false);
      setWordIndex((i) => (i + 1) % TYPED_WORDS.length);
    } else {
      t = setTimeout(() => {
        setTypedText(isDeleting
          ? current.slice(0, typedText.length - 1)
          : current.slice(0, typedText.length + 1));
      }, isDeleting ? 32 : 72);
    }
    return () => clearTimeout(t);
  }, [typedText, isDeleting, wordIndex]);

  useEffect(() => {
    const c = setInterval(() => setShowCursor((v) => !v), 530);
    return () => clearInterval(c);
  }, []);

  const name = profile?.fullName || 'Subhash Yadav';
  const bio = profile?.shortBio || 'Building modern web applications across frontend, backend, APIs, and databases.';
  const location = profile?.location || 'Ameerpet, Hyderabad, India';
  const isAvailable = profile?.isAvailableForHire ?? true;

  return (
    <section id="home" className={`hero-section ${mounted ? 'is-mounted' : ''}`}>

      {/* LAYER 1: Base animated background */}
      <div className="hero-bg" aria-hidden="true">
        <div className="hero-bg-base" />
        <div className="hero-bg-grid" />
        <div className="hero-bg-orb hero-bg-orb-1" />
        <div className="hero-bg-orb hero-bg-orb-2" />
        <div className="hero-bg-orb hero-bg-orb-3" />
        <div className="hero-bg-conic" />
        <div className="hero-bg-particles">
          {Array.from({ length: 30 }).map((_, i) => (
            <span
              key={i}
              className="hero-particle"
              style={{
                left: `${(i * 37) % 100}%`,
                top: `${(i * 53) % 100}%`,
                animationDelay: `${(i * 0.3) % 5}s`,
                animationDuration: `${6 + (i % 5)}s`,
              }}
            />
          ))}
        </div>
        <div className="hero-bg-aurora" />
      </div>

      {/* LAYER 2: Profile image — seamless, blended */}
      <div className="hero-photo" aria-hidden="true">
        <img src="/images/profile/profile.png" alt="" loading="eager" />
      </div>

      {/* LAYER 3: Vignette overlay on top */}
      <div className="hero-overlay" aria-hidden="true" />

      {/* LAYER 4: Content */}
      <div className="hero-container">
        <div className="hero-content">
          {isAvailable && (
            <div className="hero-badge">
              <span className="hero-badge-ping" />
              <span className="hero-badge-dot" />
              <span className="hero-badge-text">Available for opportunities</span>
            </div>
          )}

          <div className="hero-greeting">
            <span className="hero-greeting-line" />
            <span>Hello, I&apos;m</span>
          </div>

          <h1 className="hero-name">
            <span className="hero-name-word">SUBHASH</span>
            <span className="hero-name-word hero-name-word--accent">YADAV</span>
          </h1>

          <div className="hero-title">
            <span className="hero-title-bracket">&lt;</span>
            <span className="hero-title-text">{typedText}</span>
            <span className={`hero-cursor ${showCursor ? '' : 'off'}`}>|</span>
            <span className="hero-title-bracket">/&gt;</span>
          </div>

          <p className="hero-bio">{bio}</p>

          <div className="hero-meta">
            <span className="hero-meta-item">
              <MapPin size={13} />
              {location}
            </span>
            <span className="hero-meta-sep" />
            <span className="hero-meta-item hero-meta-item--live">
              <span className="hero-meta-dot" />
              Open to work
            </span>
          </div>

          <div className="hero-ctas">
            <a href="#contact" className="hero-cta hero-cta--primary">
              <span>Hire Me</span>
              <ArrowRight size={15} />
            </a>
            <a
              href="/resume/Subhash_Yadav_Resume.pdf"
              target="_blank"
              rel="noopener noreferrer"
              className="hero-cta hero-cta--secondary"
            >
              <Download size={15} />
              <span>Download CV</span>
            </a>
          </div>

          <div className="hero-bottom">
            <div className="hero-stats">
              {STATS.map((s, i) => (
                <div key={s.label} className="hero-stat">
                  <span className="hero-stat-shine" aria-hidden="true" />
                  <span className="hero-stat-num">{s.num}</span>
                  <span className="hero-stat-lbl">{s.label}</span>
                  <span className="hero-stat-bar">
                    <span
                      className="hero-stat-bar-fill"
                      style={{ width: `${[28, 55, 92][i] || 60}%` }}
                    />
                  </span>
                </div>
              ))}
            </div>

            {links.length > 0 && (
              <div className="hero-socials">
                <span className="hero-socials-label">
                  <Sparkles size={10} />
                  Connect
                </span>
                <div className="hero-socials-list">
                  {links
                    .filter((l) => ['GitHub', 'LinkedIn'].includes(l.displayName))
                    .map((l) => {
                      const Icon = iconMap[l.platform as keyof typeof iconMap];
                      if (!Icon) return null;
                      const slug = l.displayName.toLowerCase();
                      return (
                        <a
                          key={l.id}
                          href={l.url}
                          target="_blank"
                          rel="noopener noreferrer"
                          className={`hero-social hero-social--${slug}`}
                          aria-label={l.displayName}
                          title={l.displayName}
                        >
                          <Icon size={18} />
                        </a>
                      );
                    })}
                  <a
                    href="https://wa.me/919572003492?text=Hi%20Subhash%2C%20I%20saw%20your%20portfolio!"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="hero-social hero-social--whatsapp"
                    aria-label="WhatsApp"
                    title="Chat on WhatsApp"
                  >
                    <FaWhatsapp size={18} />
                  </a>
                </div>
              </div>
            )}
          </div>
        </div>
      </div>

      <a href="#about" className="hero-scroll" aria-label="Scroll down">
        <span className="hero-scroll-mouse">
          <span className="hero-scroll-wheel" />
        </span>
        <span className="hero-scroll-text">Scroll</span>
      </a>
    </section>
  );
}
