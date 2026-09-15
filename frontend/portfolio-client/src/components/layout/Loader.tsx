import { useEffect, useState } from 'react';
import '../../styles/loader.css';

const BOOT_STEPS = [
  { text: 'INITIALIZING CORE', status: 'OK' },
  { text: 'LOADING MODULES', status: 'OK' },
  { text: 'CONNECTING DATABASE', status: 'OK' },
  { text: 'FETCHING ASSETS', status: 'OK' },
  { text: 'RENDERING INTERFACE', status: 'OK' },
];

export default function Loader() {
  const [progress, setProgress] = useState(0);
  const [isExiting, setIsExiting] = useState(false);
  const [isVisible, setIsVisible] = useState(true);
  const [completedSteps, setCompletedSteps] = useState<number[]>([]);
  const [currentStep, setCurrentStep] = useState(0);

  // Progress animation
  useEffect(() => {
    const duration = 2600;
    const startTime = Date.now();

    const tick = () => {
      const elapsed = Date.now() - startTime;
      const pct = Math.min((elapsed / duration) * 100, 100);
      setProgress(pct);

      // Steps progression
      const stepCount = Math.min(Math.floor(pct / 20), BOOT_STEPS.length);
      const steps: number[] = [];
      for (let i = 0; i < stepCount; i++) steps.push(i);
      setCompletedSteps(steps);
      setCurrentStep(Math.min(stepCount, BOOT_STEPS.length - 1));

      if (pct < 100) {
        requestAnimationFrame(tick);
      } else {
        setCompletedSteps([0, 1, 2, 3, 4]);
        setTimeout(() => setIsExiting(true), 400);
        setTimeout(() => setIsVisible(false), 1600);
      }
    };

    requestAnimationFrame(tick);
  }, []);

  if (!isVisible) return null;

  const pct = Math.round(progress);
  const circumference = 2 * Math.PI * 54;
  const strokeDashoffset = circumference - (progress / 100) * circumference;

  return (
    <div className={`ldr2-overlay ${isExiting ? 'ldr2-exit' : ''}`}>

      {/* Background layers */}
      <div className="ldr2-bg-grid" aria-hidden="true" />
      <div className="ldr2-bg-radial" aria-hidden="true" />
      <div className="ldr2-bg-scanline" aria-hidden="true" />

      {/* Floating orbs */}
      <div className="ldr2-orbs" aria-hidden="true">
        <span className="ldr2-orb ldr2-orb-a" />
        <span className="ldr2-orb ldr2-orb-b" />
        <span className="ldr2-orb ldr2-orb-c" />
      </div>

      {/* Main card */}
      <div className="ldr2-card">

        {/* Top bar */}
        <div className="ldr2-topbar">
          <div className="ldr2-topbar-left">
            <span className="ldr2-dot ldr2-dot-r" />
            <span className="ldr2-dot ldr2-dot-y" />
            <span className="ldr2-dot ldr2-dot-g" />
            <span className="ldr2-topbar-title">SYSTEM BOOT</span>
          </div>
          <div className="ldr2-topbar-right">
            <span className="ldr2-version">v2.0.0</span>
          </div>
        </div>

        {/* Body — 2 columns on desktop, 1 on mobile */}
        <div className="ldr2-body">

          {/* ─── LEFT: Logo + Progress Ring ─── */}
          <div className="ldr2-left">
            <div className="ldr2-ring-wrap">

              {/* Outer static ring */}
              <svg className="ldr2-ring-static" viewBox="0 0 130 130" aria-hidden="true">
                <circle cx="65" cy="65" r="60" fill="none" strokeWidth="1"
                  className="ldr2-static-outer" />
                <circle cx="65" cy="65" r="48" fill="none" strokeWidth="1"
                  strokeDasharray="2 6" className="ldr2-static-mid" />
              </svg>

              {/* Progress ring */}
              <svg className="ldr2-ring-progress" viewBox="0 0 130 130" aria-hidden="true">
                <defs>
                  <linearGradient id="ringGrad" x1="0%" y1="0%" x2="100%" y2="100%">
                    <stop offset="0%" stopColor="#3b82f6" />
                    <stop offset="50%" stopColor="#22d3ee" />
                    <stop offset="100%" stopColor="#8b5cf6" />
                  </linearGradient>
                </defs>
                <circle
                  cx="65" cy="65" r="54"
                  fill="none"
                  strokeWidth="3"
                  strokeLinecap="round"
                  stroke="url(#ringGrad)"
                  strokeDasharray={circumference}
                  strokeDashoffset={strokeDashoffset}
                  transform="rotate(-90 65 65)"
                  className="ldr2-progress-ring"
                />
              </svg>

              {/* Glow */}
              <div className="ldr2-ring-glow" aria-hidden="true" />

              {/* Logo core */}
              <div className="ldr2-logo-core">
                <span className="ldr2-logo-text">SY</span>
              </div>

            </div>

            {/* Percentage */}
            <div className="ldr2-pct-wrap">
              <span className="ldr2-pct-num">{pct.toString().padStart(3, '0')}</span>
              <span className="ldr2-pct-sign">%</span>
            </div>
            <div className="ldr2-pct-label">LOADING</div>
          </div>

          {/* ─── RIGHT: Content ─── */}
          <div className="ldr2-right">

            {/* Name */}
            <div className="ldr2-name-wrap">
              <span className="ldr2-name-word">SUBHASH</span>
              <span className="ldr2-name-word ldr2-name-accent">YADAV</span>
            </div>
            <div className="ldr2-role">
              <span className="ldr2-role-line" />
              .NET FULL STACK DEVELOPER
            </div>

            {/* Divider */}
            <div className="ldr2-divider" />

            {/* Boot steps */}
            <div className="ldr2-steps">
              {BOOT_STEPS.map((step, i) => {
                const isDone = completedSteps.includes(i);
                const isActive = currentStep === i && !isDone;
                return (
                  <div
                    key={i}
                    className={`ldr2-step ${isDone ? 'ldr2-step-done' : ''} ${isActive ? 'ldr2-step-active' : ''}`}
                  >
                    <span className="ldr2-step-marker">
                      {isDone ? '✓' : isActive ? '●' : '○'}
                    </span>
                    <span className="ldr2-step-text">{step.text}</span>
                    {isDone && <span className="ldr2-step-status">OK</span>}
                    {isActive && <span className="ldr2-step-status ldr2-step-status-active">...</span>}
                  </div>
                );
              })}
            </div>

          </div>
        </div>

        {/* Bottom progress bar */}
        <div className="ldr2-footer">
          <div className="ldr2-bar-track">
            <div className="ldr2-bar-fill" style={{ width: `${progress}%` }}>
              <div className="ldr2-bar-shine" />
            </div>
          </div>
          <div className="ldr2-footer-meta">
            <span className="ldr2-footer-left">
              <span className="ldr2-status-dot" />
              STATUS: {progress >= 100 ? 'READY' : 'LOADING'}
            </span>
            <span className="ldr2-footer-right">BUILT BY SUBHASH YADAV</span>
          </div>
        </div>

      </div>

      {/* Bottom watermark outside card */}
      <div className="ldr2-watermark">
        <span className="ldr2-wm-line" />
        <span className="ldr2-wm-text">PREMIUM PORTFOLIO</span>
        <span className="ldr2-wm-line" />
      </div>

    </div>
  );
}
