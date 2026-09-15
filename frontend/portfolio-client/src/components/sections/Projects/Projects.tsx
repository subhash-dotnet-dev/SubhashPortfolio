import { useEffect, useMemo, useState } from 'react';
import { createPortal } from 'react-dom';
import { useProjects } from '../../../hooks/useProjects';
import {
  Sparkles,
  ArrowUpRight,
  X,
  Layers,
  Cpu,
  Shield,
  Target,
  CheckCircle2,
  TrendingUp,
  GitBranch,
  ExternalLink,
  AlertCircle,
  Lightbulb,
  Box,
  Code2,
} from 'lucide-react';
import '../../../styles/projects.css';

const PROJECT_COLORS = ['#3b82f6', '#22d3ee', '#8b5cf6', '#f59e0b', '#10b981'];

export default function Projects() {
  const { projects } = useProjects();
  const [openId, setOpenId] = useState<string | null>(null);

  const sorted = useMemo(
    () => [...projects].sort((a, b) => a.displayOrder - b.displayOrder),
    [projects]
  );

  const openProject = useMemo(
    () => sorted.find((p) => p.id === openId) || null,
    [sorted, openId]
  );

  const openIndex = useMemo(
    () => sorted.findIndex((p) => p.id === openId),
    [sorted, openId]
  );

  useEffect(() => {
    if (!openId) return;
    const onEsc = (e: KeyboardEvent) => {
      if (e.key === 'Escape') setOpenId(null);
    };
    document.body.style.overflow = 'hidden';
    window.addEventListener('keydown', onEsc);
    return () => {
      document.body.style.overflow = '';
      window.removeEventListener('keydown', onEsc);
    };
  }, [openId]);

  const colorFor = (idx: number) => PROJECT_COLORS[idx % PROJECT_COLORS.length];

  return (
    <section id="projects" className="pr-sec">

      <div className="pr-bg" aria-hidden="true">
        <div className="pr-bg-base" />
        <div className="pr-bg-grid" />
        <div className="pr-bg-orb pr-bg-orb-1" />
        <div className="pr-bg-orb pr-bg-orb-2" />
        <div className="pr-bg-orb pr-bg-orb-3" />
        <div className="pr-bg-particles">
          {Array.from({ length: 24 }).map((_, i) => (
            <span
              key={i}
              className="pr-particle"
              style={{
                left: `${(i * 37) % 100}%`,
                top: `${(i * 53) % 100}%`,
                animationDelay: `${(i * 0.3) % 5}s`,
                animationDuration: `${6 + (i % 5)}s`,
              }}
            />
          ))}
        </div>
        <div className="pr-bg-aurora" />
        <div className="pr-bg-vignette" />
      </div>

      <div className="pr-wrap">

        <header className="pr-hd">
          <span className="pr-pill">
            <Sparkles size={10} />
            <span>Projects</span>
          </span>
          <h2 className="pr-title">
            Things I have{' '}
            <span className="pr-title-accent">designed and shipped</span>.
          </h2>
          <p className="pr-meta-line">
            <strong>{sorted.length}</strong> projects
            <span className="pr-meta-dot">/</span>
            Click to explore
          </p>
        </header>

        <div className="pr-grid">
          {sorted.map((p, idx) => {
            const color = colorFor(idx);

            return (
              <button
                key={p.id}
                className={`pr-card ${idx === 0 ? 'pr-card--featured' : ''}`}
                onClick={() => setOpenId(p.id)}
                style={{ '--c': color } as React.CSSProperties}
              >
                <span className="pr-card-num">
                  {String(idx + 1).padStart(2, '0')}
                </span>

                <header className="pr-card-hd">
                  <span className="pr-card-icon">
                    <Box size={16} />
                  </span>
                  <span className="pr-card-arrow">
                    <ArrowUpRight size={13} />
                  </span>
                </header>

                <h3 className="pr-card-title">{p.title}</h3>
                <p className="pr-card-sub">{p.subtitle}</p>

                <p className="pr-card-desc">{p.shortDescription}</p>

                <div className="pr-card-tech">
                  {p.technologies.slice(0, 3).map((t) => (
                    <span key={t.id} className="pr-card-chip">
                      {t.name}
                    </span>
                  ))}
                  {p.technologies.length > 3 && (
                    <span className="pr-card-more">
                      +{p.technologies.length - 3}
                    </span>
                  )}
                </div>

                <footer className="pr-card-foot">
                  <span className="pr-card-features">
                    <CheckCircle2 size={10} />
                    {p.features.length} features
                  </span>
                  <span className="pr-card-read">
                    Details
                    <ArrowUpRight size={11} />
                  </span>
                </footer>
              </button>
            );
          })}
        </div>

      </div>

      {/* ============ MODAL ============ */}
      {openProject && createPortal(
        <div
          className="pr-modal-backdrop"
          style={{ position: "fixed", top: 0, left: 0, right: 0, bottom: 0, width: "100vw", height: "100vh", zIndex: 2147483647, display: "flex", alignItems: "center", justifyContent: "center", padding: "20px", background: "rgba(2, 5, 12, 0.98)", backdropFilter: "blur(24px)", overflowY: "auto" }}
          onClick={() => setOpenId(null)}
        >
          <div
            className="pr-modal"
            onClick={(e) => e.stopPropagation()}
            style={{ '--c': colorFor(openIndex) } as React.CSSProperties}
          >
            <header className="pr-modal-hd">
              <span className="pr-modal-num">
                {String(openIndex + 1).padStart(2, '0')}
              </span>
              <div className="pr-modal-titles">
                <h3 className="pr-modal-title">{openProject.title}</h3>
                <p className="pr-modal-sub">{openProject.subtitle}</p>
              </div>
              <button
                className="pr-modal-close"
                onClick={() => setOpenId(null)}
                aria-label="Close"
              >
                <X size={16} />
              </button>
            </header>

            <div className="pr-modal-body">

              {/* 2-Column Layout */}
              <div className="pr-modal-cols">

                {/* LEFT COLUMN */}
                <div className="pr-modal-col">

                  <p className="pr-modal-desc">{openProject.shortDescription}</p>

                  <div className="pr-info-block pr-info-block--problem">
                    <span className="pr-info-label">
                      <AlertCircle size={11} />
                      Problem
                    </span>
                    <p>{openProject.problem}</p>
                  </div>

                  <div className="pr-info-block pr-info-block--solution">
                    <span className="pr-info-label">
                      <Lightbulb size={11} />
                      Solution
                    </span>
                    <p>{openProject.solution}</p>
                  </div>

                  <div className="pr-info-block pr-info-block--arch">
                    <span className="pr-info-label">
                      <Layers size={11} />
                      Architecture
                    </span>
                    <p>{openProject.architecture}</p>
                  </div>

                </div>

                {/* RIGHT COLUMN */}
                <div className="pr-modal-col">

                  <div className="pr-info-block pr-info-block--challenge">
                    <span className="pr-info-label">
                      <Cpu size={11} />
                      Challenges
                    </span>
                    <p>{openProject.engineeringChallenges}</p>
                  </div>

                  <div className="pr-info-block pr-info-block--results">
                    <span className="pr-info-label">
                      <TrendingUp size={11} />
                      Results
                    </span>
                    <p>{openProject.results}</p>
                  </div>

                  {openProject.securityNotes && (
                    <div className="pr-info-block pr-info-block--security">
                      <span className="pr-info-label">
                        <Shield size={11} />
                        Security
                      </span>
                      <p>{openProject.securityNotes}</p>
                    </div>
                  )}

                  <div className="pr-info-block pr-info-block--features">
                    <span className="pr-info-label">
                      <Target size={11} />
                      Features ({openProject.features.length})
                    </span>
                    <ul className="pr-features-list">
                      {openProject.features.map((f) => (
                        <li key={f.id} className="pr-feature-item">
                          <span className="pr-feature-dot" />
                          <span>{f.description}</span>
                        </li>
                      ))}
                    </ul>
                  </div>

                </div>

              </div>

              {/* Full-width tech bar */}
              <div className="pr-modal-tech">
                <span className="pr-modal-tech-label">
                  <Code2 size={11} />
                  Tech Stack
                </span>
                <div className="pr-tech-list">
                  {openProject.technologies.map((t) => (
                    <span key={t.id} className="pr-tech-chip">
                      {t.name}
                    </span>
                  ))}
                </div>
              </div>

              {/* Links */}
              {(openProject.githubUrl || openProject.liveDemoUrl) && (
                <div className="pr-modal-links">
                  {openProject.githubUrl && (
                    <a
                      href={openProject.githubUrl}
                      target="_blank"
                      rel="noopener noreferrer"
                      className="pr-link"
                    >
                      <GitBranch size={12} />
                      Source Code
                    </a>
                  )}
                  {openProject.liveDemoUrl && (
                    <a
                      href={openProject.liveDemoUrl}
                      target="_blank"
                      rel="noopener noreferrer"
                      className="pr-link pr-link--primary"
                    >
                      <ExternalLink size={12} />
                      Live Demo
                    </a>
                  )}
                </div>
              )}

            </div>
          </div>
        </div>,
        document.body
      )}

    </section>
  );
}