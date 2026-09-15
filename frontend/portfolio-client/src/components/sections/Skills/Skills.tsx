import { useMemo, useState, useEffect } from 'react';
import { createPortal } from 'react-dom';
import { useSkills } from '../../../hooks/useSkills';
import {
  Sparkles, Code2, Layers, Zap, Database, GitBranch, Shield, Wrench,
  Award, Server, Globe, Box, Terminal, KeyRound, FileCode, Braces,
  Hash, Palette, Workflow, Bug, Network, CheckCircle2, Lock, TestTube,
  ListChecks, Cpu, TrendingUp, ArrowUpRight, X,
} from 'lucide-react';
import '../../../styles/skills.css';

const SKILL_ICONS: Record<string, React.ComponentType<{ size?: number }>> = {
  'C#': Hash, 'JavaScript ES6+': Braces, 'TypeScript': FileCode, 'SQL': Database,
  '.NET Core': Layers, 'ASP.NET Core': Layers, 'ASP.NET MVC': Layers,
  'Web API': Server, 'Entity Framework': Box, 'EF Core': Box,
  'ADO.NET': Database, 'SignalR': Network, 'React.js': Zap,
  'HTML5': Code2, 'CSS3': Palette, 'Bootstrap': Palette, 'Tailwind CSS': Palette,
  'Microsoft SQL Server': Database, 'Joins': Network, 'Stored Procedures': Database,
  'Indexing': ListChecks, 'CRUD Operations': Database, 'Complex Queries': Braces,
  'Database Optimization': TrendingUp, 'MVC Architecture': Layers,
  'Repository Pattern': Box, 'Dependency Injection': Workflow,
  'RESTful APIs': Globe, 'JSON': Braces, 'Clean Architecture': Layers,
  'JWT Authentication': KeyRound, 'Role-Based Authorization': Lock,
  'Form Validation': CheckCircle2, 'Exception Handling': Bug, 'Debugging': Bug,
  'Visual Studio': FileCode, 'VS Code': FileCode, 'Git': GitBranch,
  'GitHub': GitBranch, 'Postman': Terminal, 'Swagger': Server,
  'Version Control': GitBranch, 'Responsive UI Development': Globe,
  'API Testing': TestTube, 'Agile/Scrum': Workflow, 'Problem Solving': Cpu,
};

const CATEGORY_INFO: Record<number, { name: string; icon: typeof Code2; color: string }> = {
  1: { name: 'Languages', icon: Code2, color: '#a179dc' },
  2: { name: '.NET Frameworks', icon: Layers, color: '#7f5af0' },
  3: { name: 'Frontend', icon: Zap, color: '#22d3ee' },
  4: { name: 'Database', icon: Database, color: '#ef4444' },
  5: { name: 'Concepts', icon: GitBranch, color: '#3b82f6' },
  6: { name: 'Security', icon: Shield, color: '#f59e0b' },
  7: { name: 'Tools', icon: Wrench, color: '#10b981' },
  8: { name: 'Practices', icon: Award, color: '#ec4899' },
};

const LEVELS: Record<number, string> = {
  1: 'Beginner', 2: 'Intermediate', 3: 'Advanced', 4: 'Expert',
};

const HERO_SKILLS = ['C#', '.NET Core', 'React.js', 'SQL'];

export default function Skills() {
  const { skills } = useSkills();
  const [openCat, setOpenCat] = useState<number | null>(null);

  // Lock body scroll when modal is open
  useEffect(() => {
    if (openCat !== null) {
      document.body.style.overflow = 'hidden';
      return () => { document.body.style.overflow = ''; };
    }
  }, [openCat]);
  // Close on ESC key
  useEffect(() => {
    const onKey = (e: KeyboardEvent) => {
      if (e.key === 'Escape') setOpenCat(null);
    };
    if (openCat !== null) {
      window.addEventListener('keydown', onKey);
      return () => window.removeEventListener('keydown', onKey);
    }
  }, [openCat]);

  // Auto-close on scroll
  useEffect(() => {
    if (openCat === null) return;
    const onScroll = () => setOpenCat(null);
    window.addEventListener('scroll', onScroll, { passive: true });
    return () => window.removeEventListener('scroll', onScroll);
  }, [openCat]);

  const grouped = useMemo(() => {
    const map = new Map<number, typeof skills>();
    skills.forEach((s) => {
      const arr = map.get(s.category) || [];
      arr.push(s);
      map.set(s.category, arr);
    });
    return map;
  }, [skills]);

  const categories = useMemo(
    () => Array.from(grouped.keys()).sort((a, b) => a - b),
    [grouped]
  );

  const heroSkills = useMemo(
    () => HERO_SKILLS.map((n) => skills.find((s) => s.name === n)).filter(Boolean) as typeof skills,
    [skills]
  );

  const catPct = (cat: number) => {
    const arr = grouped.get(cat) || [];
    if (!arr.length) return 0;
    const sum = arr.reduce((s, x) => s + x.proficiency, 0);
    return Math.round((sum / (arr.length * 4)) * 100);
  };


  const openModal = openCat !== null ? {
    info: CATEGORY_INFO[openCat],
    skills: grouped.get(openCat) || [],
  } : null;



  return (
    <section id="skills" className="sk-sec">

      {/* ============ ENHANCED BACKGROUND ============ */}
      <div className="sk-bg" aria-hidden="true">
        <div className="sk-bg-base" />
        <div className="sk-bg-grid" />

        <div className="sk-bg-orb sk-bg-orb-1" />
        <div className="sk-bg-orb sk-bg-orb-2" />
        <div className="sk-bg-orb sk-bg-orb-3" />

        <div className="sk-bg-particles">
          {Array.from({ length: 30 }).map((_, i) => (
            <span
              key={i}
              className="sk-particle"
              style={{
                left: `${(i * 37) % 100}%`,
                top: `${(i * 53) % 100}%`,
                animationDelay: `${(i * 0.3) % 5}s`,
                animationDuration: `${6 + (i % 5)}s`,
              }}
            />
          ))}
        </div>

        <div className="sk-bg-aurora" />
        <div className="sk-bg-conic" />

        <div className="sk-bg-lines">
          <span className="sk-line sk-line-1" />
          <span className="sk-line sk-line-2" />
          <span className="sk-line sk-line-3" />
        </div>

        <div className="sk-bg-vignette" />
      </div>

      <div className="sk-wrap">

        <header className="sk-hd">
          <span className="sk-pill">
            <Sparkles size={11} />
            <span>Skills &amp; Expertise</span>
          </span>
          <h2 className="sk-title">
            A focused toolkit for{' '}
            <span className="sk-title-accent">modern software</span>.
          </h2>
        </header>

        <div className="sk-bento">

          <article className="sk-hero">
            <div className="sk-hero-head">
              <span className="sk-hero-label">Signature Stack</span>
              <span className="sk-hero-count">
                {skills.length} skills · {categories.length} domains
              </span>
            </div>

            <h3 className="sk-hero-title">
              Full-Stack
              <br />
              <span className="sk-hero-title-accent">.NET Engineer</span>
            </h3>

            <p className="sk-hero-sub">
              Building across the stack — from typed APIs to polished React UIs.
            </p>

            <div className="sk-hero-skills">
              {heroSkills.map((s) => {
                const info = CATEGORY_INFO[s.category];
                const Icon = SKILL_ICONS[s.name] || info.icon;

                return (
                  <div
                    key={s.id}
                    className="sk-hero-skill"
                    style={{ '--c': info.color } as React.CSSProperties}
                  >
                    <span className="sk-hero-skill-icon">
                      <Icon size={16} />
                    </span>
                    <span className="sk-hero-skill-name">{s.name}</span>
                    <span className="sk-hero-skill-yr">
                      {s.yearsOfExperience}+y
                    </span>
                  </div>
                );
              })}
            </div>
          </article>

          {categories.map((cat, idx) => {
            const info = CATEGORY_INFO[cat];
            const Icon = info.icon;
            const items = grouped.get(cat) || [];
            const pct = catPct(cat);
            const preview = items.slice(0, 3);



            return (
              <button
                key={cat}
                className="sk-bento-card"
                onClick={() => setOpenCat(cat)}
                style={{ '--c': info.color } as React.CSSProperties}
              >
                <div className="sk-bento-head">
                  <span className="sk-bento-icon"><Icon size={18} /></span>
                  <span className="sk-bento-num">
                    {String(idx + 1).padStart(2, '0')}
                  </span>
                </div>

                <h4 className="sk-bento-name">{info.name}</h4>

                <div className="sk-bento-preview">
                  {preview.map((s) => (
                    <span key={s.id} className="sk-bento-chip">
                      {s.name}
                    </span>
                  ))}
                  {items.length > 3 && (
                    <span className="sk-bento-more">
                      +{items.length - 3}
                    </span>
                  )}
                </div>

                <div className="sk-bento-foot">
                  <span className="sk-bento-pct">{pct}%</span>
                  <span className="sk-bento-arrow">
                    <ArrowUpRight size={13} />
                  </span>
                </div>

                <span className="sk-bento-bar">
                  <span
                    className="sk-bento-bar-fill"
                    style={{ width: `${pct}%` }}
                  />
                </span>
              </button>
            );
          })}
        </div>

      </div>

      {openCat !== null && openModal && createPortal(
        <div
          className="sk-modal-backdrop"
          onClick={() => setOpenCat(null)}
        >
          <div
            className="sk-modal"
            onClick={(e) => e.stopPropagation()}
            style={{ '--c': openModal.info.color } as React.CSSProperties}
          >
            <header className="sk-modal-head">
              <span className="sk-modal-icon">
                <openModal.info.icon size={20} />
              </span>
              <div className="sk-modal-titles">
                <h3 className="sk-modal-title">{openModal.info.name}</h3>
                <span className="sk-modal-sub">
                  {openModal.skills.length} skills · {catPct(openCat)}% proficiency
                </span>
              </div>
              <button
                className="sk-modal-close"
                onClick={() => setOpenCat(null)}
                aria-label="Close"
              >
                <X size={18} />
              </button>
            </header>

            <div className="sk-modal-grid">
              {openModal.skills.map((s, idx) => {
                const Icon = SKILL_ICONS[s.name] || openModal.info.icon;

                return (
                  <article
                    key={s.id}
                    className="sk-modal-skill"
                    style={{ animationDelay: `${idx * 0.03}s` }}
                  >
                    <span className="sk-modal-skill-icon">
                      <Icon size={15} />
                    </span>
                    <div className="sk-modal-skill-body">
                      <div className="sk-modal-skill-row">
                        <span className="sk-modal-skill-name">{s.name}</span>
                        <span className="sk-modal-skill-yr">
                          {s.yearsOfExperience}+y
                        </span>
                      </div>
                      <div className="sk-modal-skill-meta">
                        <span>{LEVELS[s.proficiency]}</span>
                        <span className="sk-modal-skill-bar">
                          <span
                            style={{ width: `${(s.proficiency / 4) * 100}%` }}
                          />
                        </span>
                      </div>
                    </div>
                  </article>
                );
              })}
            </div>
          </div>
        </div>,
        document.body
      )}
    </section>
  );
}