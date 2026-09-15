import { useExperience } from '../../../hooks/useExperience';
import {
  MapPin,
  Calendar,
  CheckCircle2,
  Sparkles,
  Code2,
} from 'lucide-react';
import '../../../styles/experience.css';

function fmt(iso: string): string {
  const d = new Date(iso);
  return d.toLocaleDateString('en-US', { month: 'short', year: 'numeric' });
}

function range(start: string, end: string | null, isCurrent: boolean): string {
  const s = fmt(start);
  if (isCurrent || !end) return s + ' - Present';
  return s + ' - ' + fmt(end);
}

export default function Experience() {
  const { experiences } = useExperience();

  return (
    <section id="experience" className="ex-sec">

      {/* ============ ENHANCED BACKGROUND ============ */}
      <div className="ex-bg" aria-hidden="true">
        <div className="ex-bg-base" />
        <div className="ex-bg-grid" />

        <div className="ex-bg-orb ex-bg-orb-1" />
        <div className="ex-bg-orb ex-bg-orb-2" />
        <div className="ex-bg-orb ex-bg-orb-3" />

        <div className="ex-bg-particles">
          {Array.from({ length: 26 }).map((_, i) => (
            <span
              key={i}
              className="ex-particle"
              style={{
                left: `${(i * 37) % 100}%`,
                top: `${(i * 53) % 100}%`,
                animationDelay: `${(i * 0.3) % 5}s`,
                animationDuration: `${6 + (i % 5)}s`,
              }}
            />
          ))}
        </div>

        <div className="ex-bg-aurora" />
        <div className="ex-bg-conic" />

        <div className="ex-bg-lines">
          <span className="ex-line ex-line-1" />
          <span className="ex-line ex-line-2" />
          <span className="ex-line ex-line-3" />
        </div>

        <div className="ex-bg-vignette" />
      </div>

      <div className="ex-wrap">

        <header className="ex-hd">
          <span className="ex-pill">
            <Sparkles size={10} />
            <span>Experience</span>
          </span>
          <h2 className="ex-title">
            Where I have{' '}
            <span className="ex-title-accent">built and grown</span>.
          </h2>
        </header>

        <div className="ex-grid">
          {experiences.map((exp, idx) => {
            const techs = exp.techEnvironment
              .split(/\s*[\u00B7\u2022\u2027]\s*/)
              .map((t) => t.trim())
              .filter(Boolean);

            return (
              <article key={exp.id} className="ex-card">

                <header className="ex-card-hd">
                  <div className="ex-card-left">
                    <span className="ex-card-num">
                      {String(idx + 1).padStart(2, '0')}
                    </span>
                    <div className="ex-card-title-wrap">
                      <h3 className="ex-card-company">{exp.company}</h3>
                      <span className="ex-card-role">{exp.role}</span>
                    </div>
                  </div>
                  {exp.isCurrent && (
                    <span className="ex-card-live">
                      <span className="ex-card-live-dot" />
                      Current
                    </span>
                  )}
                </header>

                <div className="ex-card-meta">
                  <span className="ex-meta">
                    <Calendar size={10} />
                    {range(exp.startDate, exp.endDate, exp.isCurrent)}
                  </span>
                  <span className="ex-meta">
                    <MapPin size={10} />
                    {exp.location}
                  </span>
                </div>

                <p className="ex-card-desc">{exp.description}</p>

                <div className="ex-card-res">
                  <span className="ex-res-label">
                    <CheckCircle2 size={10} />
                    Key Contributions
                  </span>
                  <ul className="ex-res-list">
                    {exp.responsibilities.map((r) => (
                      <li key={r.id} className="ex-res-item">
                        <span className="ex-res-dot" />
                        <span>{r.description}</span>
                      </li>
                    ))}
                  </ul>
                </div>

                <div className="ex-card-tech">
                  <span className="ex-tech-label">
                    <Code2 size={10} />
                    Tech
                  </span>
                  <div className="ex-tech-list">
                    {techs.map((t) => (
                      <span key={t} className="ex-chip">{t}</span>
                    ))}
                  </div>
                </div>

              </article>
            );
          })}
        </div>

      </div>
    </section>
  );
}
