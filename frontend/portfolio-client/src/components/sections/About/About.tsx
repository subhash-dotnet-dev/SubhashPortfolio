import { useProfile } from '../../../hooks/useProfile';
import { useSocialLinks } from '../../../hooks/useSocialLinks';
import {
  Code2,
  Layers,
  Server,
  Zap,
  Database,
  GitBranch,
  Send,
  Code,
  FileCode,
  MapPin,
  Phone,
  Mail,
  Sparkles,
} from 'lucide-react';
import { FaGithub, FaLinkedin, FaInstagram, FaFacebook } from 'react-icons/fa';
import { SiLeetcode, SiHackerrank } from 'react-icons/si';
import '../../../styles/about.css';

const iconMap = {
  1: FaGithub,
  2: FaLinkedin,
  3: SiLeetcode,
  4: SiHackerrank,
  5: FaInstagram,
  6: FaFacebook,
};

const TECH = [
  { name: 'C#', icon: Code2, color: '#a179dc' },
  { name: '.NET Core', icon: Layers, color: '#7f5af0' },
  { name: 'Web API', icon: Server, color: '#3b82f6' },
  { name: 'React.js', icon: Zap, color: '#22d3ee' },
  { name: 'SQL Server', icon: Database, color: '#ef4444' },
  { name: 'Git', icon: GitBranch, color: '#f97316' },
  { name: 'Postman', icon: Send, color: '#fb923c' },
  { name: 'HTML / CSS', icon: Code, color: '#3b82f6' },
  { name: 'EF Core', icon: Layers, color: '#8b5cf6' },
  { name: 'VS Code', icon: FileCode, color: '#22d3ee' },
];

const STATS = [
  { num: '1+', label: 'Years' },
  { num: '5+', label: 'Projects' },
  { num: '46+', label: 'Skills' },
];

export default function About() {
  const { profile } = useProfile();
  const { links } = useSocialLinks();

  const name = profile?.fullName || 'Subhash Yadav';
  const title = profile?.title || '.NET Full Stack Developer';
  const location = profile?.location || 'Ameerpet, Hyderabad, India';
  const phone = profile?.phone || '+91 9572003492';
  const email = profile?.email || 'subhash.dev79@gmail.com';

  const summary =
    profile?.professionalSummary ||
    'I build complete web applications — from responsive frontends to robust backends and databases. Focused on clean code, real-world problems, and reliable, maintainable solutions.';

  const trimmed =
    summary.length > 200 ? summary.slice(0, 200).trim() + '…' : summary;

  const parts = name.split(' ');
  const firstName = parts[0] || 'Subhash';
  const lastName = parts.slice(1).join(' ') || 'Yadav';

  return (
    <section id="about" className="ab-sec">

      {/* ==================== PREMIUM ANIMATED BACKGROUND ==================== */}
      <div className="ab-bg" aria-hidden="true">
        {/* Base gradient */}
        <div className="ab-bg-base" />

        {/* Grid pattern */}
        <div className="ab-bg-grid" />

        {/* Floating particles */}
        <div className="ab-bg-particles">
          {Array.from({ length: 30 }).map((_, i) => (
            <span
              key={i}
              className="ab-particle"
              style={{
                left: `${(i * 37) % 100}%`,
                top: `${(i * 53) % 100}%`,
                animationDelay: `${(i * 0.3) % 5}s`,
                animationDuration: `${6 + (i % 5)}s`,
              }}
            />
          ))}
        </div>

        {/* Moving orbs */}
        <div className="ab-bg-orb ab-bg-orb-1" />
        <div className="ab-bg-orb ab-bg-orb-2" />
        <div className="ab-bg-orb ab-bg-orb-3" />

        {/* Aurora waves */}
        <div className="ab-bg-aurora" />

        {/* Rotating conic glow */}
        <div className="ab-bg-conic" />

        {/* Animated lines */}
        <div className="ab-bg-lines">
          <span className="ab-line ab-line-1" />
          <span className="ab-line ab-line-2" />
          <span className="ab-line ab-line-3" />
        </div>

        {/* Vignette */}
        <div className="ab-bg-vignette" />
      </div>

      <div className="ab-wrap">

        {/* ==================== HEADER ==================== */}
        <header className="ab-hd">
          <span className="ab-pill">
            <Sparkles size={11} />
            <span>About Me</span>
          </span>

          <h2 className="ab-title">
            Building Ideas into{' '}
            <span className="ab-title-accent">Real Solutions</span>
          </h2>

          <p className="ab-desc">
            .NET Full Stack Developer crafting scalable, user-focused web
            applications that solve real-world problems.
          </p>
        </header>

        {/* ==================== GRID ==================== */}
        <div className="ab-grid">

          <div className="ab-left">
            <span className="ab-eyebrow">
              <span className="ab-eyebrow-line" />
              Who I am
            </span>

            <h3 className="ab-name">
              I&apos;m{' '}
              <span className="ab-name-grad">{firstName}</span>{' '}
              <span className="ab-name-grad-2">{lastName}</span>
            </h3>

            <p className="ab-role">{title}</p>

            <p className="ab-summary">{trimmed}</p>

            <div className="ab-stats-inline">
              {STATS.map((s, i) => (
                <div key={s.label} className="ab-stat-cell">
                  <strong className="ab-stat-num">{s.num}</strong>
                  <span className="ab-stat-lbl">{s.label}</span>
                  {i < STATS.length - 1 && <span className="ab-stat-sep" />}
                </div>
              ))}
            </div>
          </div>

          <div className="ab-right">
            <div className="ab-block-head">
              <span className="ab-eyebrow">
                <span className="ab-eyebrow-line" />
                Tech Stack
              </span>
              <span className="ab-hint">What I work with</span>
            </div>

            <div className="ab-tech">
              {TECH.map(({ name: n, icon: Icon, color }) => (
                <div
                  key={n}
                  className="ab-tech-item"
                  style={{ '--c': color } as React.CSSProperties}
                >
                  <span className="ab-tech-icon">
                    <Icon size={16} />
                  </span>
                  <span className="ab-tech-name">{n}</span>
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* ==================== INFO BAR ==================== */}
        <div className="ab-info-bar">
          <a className="ab-info-item" href={`mailto:${email}`}>
            <span className="ab-info-icon">
              <Mail size={14} />
            </span>
            <span className="ab-info-text">{email}</span>
          </a>

          <span className="ab-info-div" />

          <a className="ab-info-item" href={`tel:${phone}`}>
            <span className="ab-info-icon">
              <Phone size={14} />
            </span>
            <span className="ab-info-text">{phone}</span>
          </a>

          <span className="ab-info-div" />

          <div className="ab-info-item">
            <span className="ab-info-icon">
              <MapPin size={14} />
            </span>
            <span className="ab-info-text">{location}</span>
          </div>

          <div className="ab-info-socials">
            {links.map((link) => {
              const Icon = iconMap[link.platform as keyof typeof iconMap];
              if (!Icon) return null;
              return (
                <a
                  key={link.id}
                  href={link.url}
                  target="_blank"
                  rel="noopener noreferrer"
                  className="ab-social"
                  aria-label={link.displayName}
                  title={link.displayName}
                >
                  <Icon size={14} />
                </a>
              );
            })}
          </div>
        </div>

      </div>
    </section>
  );
}
