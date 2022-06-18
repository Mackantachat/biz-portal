"use strict";

/*!
 * VERSION: 1.19.1
 * DATE: 2017-01-17
 * UPDATES AND DOCS AT: http://greensock.com
 *
 * @license Copyright (c) 2008-2017, GreenSock. All rights reserved.
 * This work is subject to the terms at http://greensock.com/standard-license or for
 * Club GreenSock members, the software agreement that was issued with your membership.
 * 
 * @author: Jack Doyle, jack@greensock.com
 */
var _gsScope = "undefined" != typeof module && module.exports && "undefined" != typeof global ? global : undefined || window; (_gsScope._gsQueue || (_gsScope._gsQueue = [])).push(function () {
    "use strict";
    _gsScope._gsDefine("TimelineMax", ["TimelineLite", "TweenLite", "easing.Ease"], function (a, b, c) {
        var d = function d(b) {
            a.call(this, b), this._repeat = this.vars.repeat || 0, this._repeatDelay = this.vars.repeatDelay || 0, this._cycle = 0, this._yoyo = this.vars.yoyo === !0, this._dirty = !0;
        },
            e = 1e-10,
            f = b._internals,
            g = f.lazyTweens,
            h = f.lazyRender,
            i = _gsScope._gsDefine.globals,
            j = new c(null, null, 1, 0),
            k = d.prototype = new a(); return k.constructor = d, k.kill()._gc = !1, d.version = "1.19.1", k.invalidate = function () {
                return this._yoyo = this.vars.yoyo === !0, this._repeat = this.vars.repeat || 0, this._repeatDelay = this.vars.repeatDelay || 0, this._uncache(!0), a.prototype.invalidate.call(this);
            }, k.addCallback = function (a, c, d, e) {
                return this.add(b.delayedCall(0, a, d, e), c);
            }, k.removeCallback = function (a, b) {
                if (a) if (null == b) this._kill(null, a); else for (var c = this.getTweensOf(a, !1), d = c.length, e = this._parseTimeOrLabel(b) ; --d > -1;) {
                    c[d]._startTime === e && c[d]._enabled(!1, !1);
                } return this;
            }, k.removePause = function (b) {
                return this.removeCallback(a._internals.pauseCallback, b);
            }, k.tweenTo = function (a, c) {
                c = c || {}; var d,
                    e,
                    f,
                    g = { ease: j, useFrames: this.usesFrames(), immediateRender: !1 },
                    h = c.repeat && i.TweenMax || b; for (e in c) {
                        g[e] = c[e];
                    } return g.time = this._parseTimeOrLabel(a), d = Math.abs(Number(g.time) - this._time) / this._timeScale || .001, f = new h(this, d, g), g.onStart = function () {
                        f.target.paused(!0), f.vars.time !== f.target.time() && d === f.duration() && f.duration(Math.abs(f.vars.time - f.target.time()) / f.target._timeScale), c.onStart && c.onStart.apply(c.onStartScope || c.callbackScope || f, c.onStartParams || []);
                    }, f;
            }, k.tweenFromTo = function (a, b, c) {
                c = c || {}, a = this._parseTimeOrLabel(a), c.startAt = { onComplete: this.seek, onCompleteParams: [a], callbackScope: this }, c.immediateRender = c.immediateRender !== !1; var d = this.tweenTo(b, c); return d.duration(Math.abs(d.vars.time - a) / this._timeScale || .001);
            }, k.render = function (a, b, c) {
                this._gc && this._enabled(!0, !1); var d,
                    f,
                    i,
                    j,
                    k,
                    l,
                    m,
                    n,
                    o = this._dirty ? this.totalDuration() : this._totalDuration,
                    p = this._duration,
                    q = this._time,
                    r = this._totalTime,
                    s = this._startTime,
                    t = this._timeScale,
                    u = this._rawPrevTime,
                    v = this._paused,
                    w = this._cycle; if (a >= o - 1e-7 && a >= 0) this._locked || (this._totalTime = o, this._cycle = this._repeat), this._reversed || this._hasPausedChild() || (f = !0, j = "onComplete", k = !!this._timeline.autoRemoveChildren, 0 === this._duration && (0 >= a && a >= -1e-7 || 0 > u || u === e) && u !== a && this._first && (k = !0, u > e && (j = "onReverseComplete"))), this._rawPrevTime = this._duration || !b || a || this._rawPrevTime === a ? a : e, this._yoyo && 0 !== (1 & this._cycle) ? this._time = a = 0 : (this._time = p, a = p + 1e-4); else if (1e-7 > a) {
                        if (this._locked || (this._totalTime = this._cycle = 0), this._time = 0, (0 !== q || 0 === p && u !== e && (u > 0 || 0 > a && u >= 0) && !this._locked) && (j = "onReverseComplete", f = this._reversed), 0 > a) this._active = !1, this._timeline.autoRemoveChildren && this._reversed ? (k = f = !0, j = "onReverseComplete") : u >= 0 && this._first && (k = !0), this._rawPrevTime = a; else {
                            if (this._rawPrevTime = p || !b || a || this._rawPrevTime === a ? a : e, 0 === a && f) for (d = this._first; d && 0 === d._startTime;) {
                                d._duration || (f = !1), d = d._next;
                            } a = 0, this._initted || (k = !0);
                        }
                    } else if (0 === p && 0 > u && (k = !0), this._time = this._rawPrevTime = a, this._locked || (this._totalTime = a, 0 !== this._repeat && (l = p + this._repeatDelay, this._cycle = this._totalTime / l >> 0, 0 !== this._cycle && this._cycle === this._totalTime / l && a >= r && this._cycle--, this._time = this._totalTime - this._cycle * l, this._yoyo && 0 !== (1 & this._cycle) && (this._time = p - this._time), this._time > p ? (this._time = p, a = p + 1e-4) : this._time < 0 ? this._time = a = 0 : a = this._time)), this._hasPause && !this._forcingPlayhead && !b && p > a) {
                        if (a = this._time, a >= q || this._repeat && w !== this._cycle) for (d = this._first; d && d._startTime <= a && !m;) {
                            d._duration || "isPause" !== d.data || d.ratio || 0 === d._startTime && 0 === this._rawPrevTime || (m = d), d = d._next;
                        } else for (d = this._last; d && d._startTime >= a && !m;) {
                            d._duration || "isPause" === d.data && d._rawPrevTime > 0 && (m = d), d = d._prev;
                        } m && (this._time = a = m._startTime, this._totalTime = a + this._cycle * (this._totalDuration + this._repeatDelay));
                    } if (this._cycle !== w && !this._locked) {
                        var x = this._yoyo && 0 !== (1 & w),
                            y = x === (this._yoyo && 0 !== (1 & this._cycle)),
                            z = this._totalTime,
                            A = this._cycle,
                            B = this._rawPrevTime,
                            C = this._time; if (this._totalTime = w * p, this._cycle < w ? x = !x : this._totalTime += p, this._time = q, this._rawPrevTime = 0 === p ? u - 1e-4 : u, this._cycle = w, this._locked = !0, q = x ? 0 : p, this.render(q, b, 0 === p), b || this._gc || this.vars.onRepeat && (this._cycle = A, this._locked = !1, this._callback("onRepeat")), q !== this._time) return; if (y && (this._cycle = w, this._locked = !0, q = x ? p + 1e-4 : -1e-4, this.render(q, !0, !1)), this._locked = !1, this._paused && !v) return; this._time = C, this._totalTime = z, this._cycle = A, this._rawPrevTime = B;
                    } if (!(this._time !== q && this._first || c || k || m)) return void (r !== this._totalTime && this._onUpdate && (b || this._callback("onUpdate"))); if (this._initted || (this._initted = !0), this._active || !this._paused && this._totalTime !== r && a > 0 && (this._active = !0), 0 === r && this.vars.onStart && (0 === this._totalTime && this._totalDuration || b || this._callback("onStart")), n = this._time, n >= q) for (d = this._first; d && (i = d._next, n === this._time && (!this._paused || v)) ;) {
                        (d._active || d._startTime <= this._time && !d._paused && !d._gc) && (m === d && this.pause(), d._reversed ? d.render((d._dirty ? d.totalDuration() : d._totalDuration) - (a - d._startTime) * d._timeScale, b, c) : d.render((a - d._startTime) * d._timeScale, b, c)), d = i;
                    } else for (d = this._last; d && (i = d._prev, n === this._time && (!this._paused || v)) ;) {
                        if (d._active || d._startTime <= q && !d._paused && !d._gc) {
                            if (m === d) {
                                for (m = d._prev; m && m.endTime() > this._time;) {
                                    m.render(m._reversed ? m.totalDuration() - (a - m._startTime) * m._timeScale : (a - m._startTime) * m._timeScale, b, c), m = m._prev;
                                } m = null, this.pause();
                            } d._reversed ? d.render((d._dirty ? d.totalDuration() : d._totalDuration) - (a - d._startTime) * d._timeScale, b, c) : d.render((a - d._startTime) * d._timeScale, b, c);
                        } d = i;
                    } this._onUpdate && (b || (g.length && h(), this._callback("onUpdate"))), j && (this._locked || this._gc || (s === this._startTime || t !== this._timeScale) && (0 === this._time || o >= this.totalDuration()) && (f && (g.length && h(), this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !b && this.vars[j] && this._callback(j)));
            }, k.getActive = function (a, b, c) {
                null == a && (a = !0), null == b && (b = !0), null == c && (c = !1); var d,
                    e,
                    f = [],
                    g = this.getChildren(a, b, c),
                    h = 0,
                    i = g.length; for (d = 0; i > d; d++) {
                        e = g[d], e.isActive() && (f[h++] = e);
                    } return f;
            }, k.getLabelAfter = function (a) {
                a || 0 !== a && (a = this._time); var b,
                    c = this.getLabelsArray(),
                    d = c.length; for (b = 0; d > b; b++) {
                        if (c[b].time > a) return c[b].name;
                    } return null;
            }, k.getLabelBefore = function (a) {
                null == a && (a = this._time); for (var b = this.getLabelsArray(), c = b.length; --c > -1;) {
                    if (b[c].time < a) return b[c].name;
                } return null;
            }, k.getLabelsArray = function () {
                var a,
                    b = [],
                    c = 0; for (a in this._labels) {
                        b[c++] = { time: this._labels[a], name: a };
                    } return b.sort(function (a, b) {
                        return a.time - b.time;
                    }), b;
            }, k.invalidate = function () {
                return this._locked = !1, a.prototype.invalidate.call(this);
            }, k.progress = function (a, b) {
                return arguments.length ? this.totalTime(this.duration() * (this._yoyo && 0 !== (1 & this._cycle) ? 1 - a : a) + this._cycle * (this._duration + this._repeatDelay), b) : this._time / this.duration();
            }, k.totalProgress = function (a, b) {
                return arguments.length ? this.totalTime(this.totalDuration() * a, b) : this._totalTime / this.totalDuration();
            }, k.totalDuration = function (b) {
                return arguments.length ? -1 !== this._repeat && b ? this.timeScale(this.totalDuration() / b) : this : (this._dirty && (a.prototype.totalDuration.call(this), this._totalDuration = -1 === this._repeat ? 999999999999 : this._duration * (this._repeat + 1) + this._repeatDelay * this._repeat), this._totalDuration);
            }, k.time = function (a, b) {
                return arguments.length ? (this._dirty && this.totalDuration(), a > this._duration && (a = this._duration), this._yoyo && 0 !== (1 & this._cycle) ? a = this._duration - a + this._cycle * (this._duration + this._repeatDelay) : 0 !== this._repeat && (a += this._cycle * (this._duration + this._repeatDelay)), this.totalTime(a, b)) : this._time;
            }, k.repeat = function (a) {
                return arguments.length ? (this._repeat = a, this._uncache(!0)) : this._repeat;
            }, k.repeatDelay = function (a) {
                return arguments.length ? (this._repeatDelay = a, this._uncache(!0)) : this._repeatDelay;
            }, k.yoyo = function (a) {
                return arguments.length ? (this._yoyo = a, this) : this._yoyo;
            }, k.currentLabel = function (a) {
                return arguments.length ? this.seek(a, !0) : this.getLabelBefore(this._time + 1e-8);
            }, d;
    }, !0), _gsScope._gsDefine("TimelineLite", ["core.Animation", "core.SimpleTimeline", "TweenLite"], function (a, b, c) {
        var d = function d(a) {
            b.call(this, a), this._labels = {}, this.autoRemoveChildren = this.vars.autoRemoveChildren === !0, this.smoothChildTiming = this.vars.smoothChildTiming === !0, this._sortChildren = !0, this._onUpdate = this.vars.onUpdate; var c,
                d,
                e = this.vars; for (d in e) {
                    c = e[d], i(c) && -1 !== c.join("").indexOf("{self}") && (e[d] = this._swapSelfInParams(c));
                } i(e.tweens) && this.add(e.tweens, 0, e.align, e.stagger);
        },
            e = 1e-10,
            f = c._internals,
            g = d._internals = {},
            h = f.isSelector,
            i = f.isArray,
            j = f.lazyTweens,
            k = f.lazyRender,
            l = _gsScope._gsDefine.globals,
            m = function m(a) {
                var b,
                    c = {}; for (b in a) {
                        c[b] = a[b];
                    } return c;
            },
            n = function n(a, b, c) {
                var d,
                    e,
                    f = a.cycle; for (d in f) {
                        e = f[d], a[d] = "function" == typeof e ? e(c, b[c]) : e[c % e.length];
                    } delete a.cycle;
            },
            o = g.pauseCallback = function () { },
            p = function p(a) {
                var b,
                    c = [],
                    d = a.length; for (b = 0; b !== d; c.push(a[b++])) { } return c;
            },
            q = d.prototype = new b(); return d.version = "1.19.1", q.constructor = d, q.kill()._gc = q._forcingPlayhead = q._hasPause = !1, q.to = function (a, b, d, e) {
                var f = d.repeat && l.TweenMax || c; return b ? this.add(new f(a, b, d), e) : this.set(a, d, e);
            }, q.from = function (a, b, d, e) {
                return this.add((d.repeat && l.TweenMax || c).from(a, b, d), e);
            }, q.fromTo = function (a, b, d, e, f) {
                var g = e.repeat && l.TweenMax || c; return b ? this.add(g.fromTo(a, b, d, e), f) : this.set(a, e, f);
            }, q.staggerTo = function (a, b, e, f, g, i, j, k) {
                var l,
                    o,
                    q = new d({ onComplete: i, onCompleteParams: j, callbackScope: k, smoothChildTiming: this.smoothChildTiming }),
                    r = e.cycle; for ("string" == typeof a && (a = c.selector(a) || a), a = a || [], h(a) && (a = p(a)), f = f || 0, 0 > f && (a = p(a), a.reverse(), f *= -1), o = 0; o < a.length; o++) {
                        l = m(e), l.startAt && (l.startAt = m(l.startAt), l.startAt.cycle && n(l.startAt, a, o)), r && (n(l, a, o), null != l.duration && (b = l.duration, delete l.duration)), q.to(a[o], b, l, o * f);
                    } return this.add(q, g);
            }, q.staggerFrom = function (a, b, c, d, e, f, g, h) {
                return c.immediateRender = 0 != c.immediateRender, c.runBackwards = !0, this.staggerTo(a, b, c, d, e, f, g, h);
            }, q.staggerFromTo = function (a, b, c, d, e, f, g, h, i) {
                return d.startAt = c, d.immediateRender = 0 != d.immediateRender && 0 != c.immediateRender, this.staggerTo(a, b, d, e, f, g, h, i);
            }, q.call = function (a, b, d, e) {
                return this.add(c.delayedCall(0, a, b, d), e);
            }, q.set = function (a, b, d) {
                return d = this._parseTimeOrLabel(d, 0, !0), null == b.immediateRender && (b.immediateRender = d === this._time && !this._paused), this.add(new c(a, 0, b), d);
            }, d.exportRoot = function (a, b) {
                a = a || {}, null == a.smoothChildTiming && (a.smoothChildTiming = !0); var e,
                    f,
                    g = new d(a),
                    h = g._timeline; for (null == b && (b = !0), h._remove(g, !0), g._startTime = 0, g._rawPrevTime = g._time = g._totalTime = h._time, e = h._first; e;) {
                        f = e._next, b && e instanceof c && e.target === e.vars.onComplete || g.add(e, e._startTime - e._delay), e = f;
                    } return h.add(g, 0), g;
            }, q.add = function (e, f, g, h) {
                var j, k, l, m, n, o; if ("number" != typeof f && (f = this._parseTimeOrLabel(f, 0, !0, e)), !(e instanceof a)) {
                    if (e instanceof Array || e && e.push && i(e)) {
                        for (g = g || "normal", h = h || 0, j = f, k = e.length, l = 0; k > l; l++) {
                            i(m = e[l]) && (m = new d({ tweens: m })), this.add(m, j), "string" != typeof m && "function" != typeof m && ("sequence" === g ? j = m._startTime + m.totalDuration() / m._timeScale : "start" === g && (m._startTime -= m.delay())), j += h;
                        } return this._uncache(!0);
                    } if ("string" == typeof e) return this.addLabel(e, f); if ("function" != typeof e) throw "Cannot add " + e + " into the timeline; it is not a tween, timeline, function, or string."; e = c.delayedCall(0, e);
                } if (b.prototype.add.call(this, e, f), (this._gc || this._time === this._duration) && !this._paused && this._duration < this.duration()) for (n = this, o = n.rawTime() > e._startTime; n._timeline;) {
                    o && n._timeline.smoothChildTiming ? n.totalTime(n._totalTime, !0) : n._gc && n._enabled(!0, !1), n = n._timeline;
                } return this;
            }, q.remove = function (b) {
                if (b instanceof a) {
                    this._remove(b, !1); var c = b._timeline = b.vars.useFrames ? a._rootFramesTimeline : a._rootTimeline; return b._startTime = (b._paused ? b._pauseTime : c._time) - (b._reversed ? b.totalDuration() - b._totalTime : b._totalTime) / b._timeScale, this;
                } if (b instanceof Array || b && b.push && i(b)) {
                    for (var d = b.length; --d > -1;) {
                        this.remove(b[d]);
                    } return this;
                } return "string" == typeof b ? this.removeLabel(b) : this.kill(null, b);
            }, q._remove = function (a, c) {
                b.prototype._remove.call(this, a, c); var d = this._last; return d ? this._time > this.duration() && (this._time = this._duration, this._totalTime = this._totalDuration) : this._time = this._totalTime = this._duration = this._totalDuration = 0, this;
            }, q.append = function (a, b) {
                return this.add(a, this._parseTimeOrLabel(null, b, !0, a));
            }, q.insert = q.insertMultiple = function (a, b, c, d) {
                return this.add(a, b || 0, c, d);
            }, q.appendMultiple = function (a, b, c, d) {
                return this.add(a, this._parseTimeOrLabel(null, b, !0, a), c, d);
            }, q.addLabel = function (a, b) {
                return this._labels[a] = this._parseTimeOrLabel(b), this;
            }, q.addPause = function (a, b, d, e) {
                var f = c.delayedCall(0, o, d, e || this); return f.vars.onComplete = f.vars.onReverseComplete = b, f.data = "isPause", this._hasPause = !0, this.add(f, a);
            }, q.removeLabel = function (a) {
                return delete this._labels[a], this;
            }, q.getLabelTime = function (a) {
                return null != this._labels[a] ? this._labels[a] : -1;
            }, q._parseTimeOrLabel = function (b, c, d, e) {
                var f; if (e instanceof a && e.timeline === this) this.remove(e); else if (e && (e instanceof Array || e.push && i(e))) for (f = e.length; --f > -1;) {
                    e[f] instanceof a && e[f].timeline === this && this.remove(e[f]);
                } if ("string" == typeof c) return this._parseTimeOrLabel(c, d && "number" == typeof b && null == this._labels[c] ? b - this.duration() : 0, d); if (c = c || 0, "string" != typeof b || !isNaN(b) && null == this._labels[b]) null == b && (b = this.duration()); else {
                    if (f = b.indexOf("="), -1 === f) return null == this._labels[b] ? d ? this._labels[b] = this.duration() + c : c : this._labels[b] + c; c = parseInt(b.charAt(f - 1) + "1", 10) * Number(b.substr(f + 1)), b = f > 1 ? this._parseTimeOrLabel(b.substr(0, f - 1), 0, d) : this.duration();
                } return Number(b) + c;
            }, q.seek = function (a, b) {
                return this.totalTime("number" == typeof a ? a : this._parseTimeOrLabel(a), b !== !1);
            }, q.stop = function () {
                return this.paused(!0);
            }, q.gotoAndPlay = function (a, b) {
                return this.play(a, b);
            }, q.gotoAndStop = function (a, b) {
                return this.pause(a, b);
            }, q.render = function (a, b, c) {
                this._gc && this._enabled(!0, !1); var d,
                    f,
                    g,
                    h,
                    i,
                    l,
                    m,
                    n = this._dirty ? this.totalDuration() : this._totalDuration,
                    o = this._time,
                    p = this._startTime,
                    q = this._timeScale,
                    r = this._paused; if (a >= n - 1e-7 && a >= 0) this._totalTime = this._time = n, this._reversed || this._hasPausedChild() || (f = !0, h = "onComplete", i = !!this._timeline.autoRemoveChildren, 0 === this._duration && (0 >= a && a >= -1e-7 || this._rawPrevTime < 0 || this._rawPrevTime === e) && this._rawPrevTime !== a && this._first && (i = !0, this._rawPrevTime > e && (h = "onReverseComplete"))), this._rawPrevTime = this._duration || !b || a || this._rawPrevTime === a ? a : e, a = n + 1e-4; else if (1e-7 > a) {
                        if (this._totalTime = this._time = 0, (0 !== o || 0 === this._duration && this._rawPrevTime !== e && (this._rawPrevTime > 0 || 0 > a && this._rawPrevTime >= 0)) && (h = "onReverseComplete", f = this._reversed), 0 > a) this._active = !1, this._timeline.autoRemoveChildren && this._reversed ? (i = f = !0, h = "onReverseComplete") : this._rawPrevTime >= 0 && this._first && (i = !0), this._rawPrevTime = a; else {
                            if (this._rawPrevTime = this._duration || !b || a || this._rawPrevTime === a ? a : e, 0 === a && f) for (d = this._first; d && 0 === d._startTime;) {
                                d._duration || (f = !1), d = d._next;
                            } a = 0, this._initted || (i = !0);
                        }
                    } else {
                        if (this._hasPause && !this._forcingPlayhead && !b) {
                            if (a >= o) for (d = this._first; d && d._startTime <= a && !l;) {
                                d._duration || "isPause" !== d.data || d.ratio || 0 === d._startTime && 0 === this._rawPrevTime || (l = d), d = d._next;
                            } else for (d = this._last; d && d._startTime >= a && !l;) {
                                d._duration || "isPause" === d.data && d._rawPrevTime > 0 && (l = d), d = d._prev;
                            } l && (this._time = a = l._startTime, this._totalTime = a + this._cycle * (this._totalDuration + this._repeatDelay));
                        } this._totalTime = this._time = this._rawPrevTime = a;
                    } if (this._time !== o && this._first || c || i || l) {
                        if (this._initted || (this._initted = !0), this._active || !this._paused && this._time !== o && a > 0 && (this._active = !0), 0 === o && this.vars.onStart && (0 === this._time && this._duration || b || this._callback("onStart")), m = this._time, m >= o) for (d = this._first; d && (g = d._next, m === this._time && (!this._paused || r)) ;) {
                            (d._active || d._startTime <= m && !d._paused && !d._gc) && (l === d && this.pause(), d._reversed ? d.render((d._dirty ? d.totalDuration() : d._totalDuration) - (a - d._startTime) * d._timeScale, b, c) : d.render((a - d._startTime) * d._timeScale, b, c)), d = g;
                        } else for (d = this._last; d && (g = d._prev, m === this._time && (!this._paused || r)) ;) {
                            if (d._active || d._startTime <= o && !d._paused && !d._gc) {
                                if (l === d) {
                                    for (l = d._prev; l && l.endTime() > this._time;) {
                                        l.render(l._reversed ? l.totalDuration() - (a - l._startTime) * l._timeScale : (a - l._startTime) * l._timeScale, b, c), l = l._prev;
                                    } l = null, this.pause();
                                } d._reversed ? d.render((d._dirty ? d.totalDuration() : d._totalDuration) - (a - d._startTime) * d._timeScale, b, c) : d.render((a - d._startTime) * d._timeScale, b, c);
                            } d = g;
                        } this._onUpdate && (b || (j.length && k(), this._callback("onUpdate"))), h && (this._gc || (p === this._startTime || q !== this._timeScale) && (0 === this._time || n >= this.totalDuration()) && (f && (j.length && k(), this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !b && this.vars[h] && this._callback(h)));
                    }
            }, q._hasPausedChild = function () {
                for (var a = this._first; a;) {
                    if (a._paused || a instanceof d && a._hasPausedChild()) return !0; a = a._next;
                } return !1;
            }, q.getChildren = function (a, b, d, e) {
                e = e || -9999999999; for (var f = [], g = this._first, h = 0; g;) {
                    g._startTime < e || (g instanceof c ? b !== !1 && (f[h++] = g) : (d !== !1 && (f[h++] = g), a !== !1 && (f = f.concat(g.getChildren(!0, b, d)), h = f.length))), g = g._next;
                } return f;
            }, q.getTweensOf = function (a, b) {
                var d,
                    e,
                    f = this._gc,
                    g = [],
                    h = 0; for (f && this._enabled(!0, !0), d = c.getTweensOf(a), e = d.length; --e > -1;) {
                        (d[e].timeline === this || b && this._contains(d[e])) && (g[h++] = d[e]);
                    } return f && this._enabled(!1, !0), g;
            }, q.recent = function () {
                return this._recent;
            }, q._contains = function (a) {
                for (var b = a.timeline; b;) {
                    if (b === this) return !0; b = b.timeline;
                } return !1;
            }, q.shiftChildren = function (a, b, c) {
                c = c || 0; for (var d, e = this._first, f = this._labels; e;) {
                    e._startTime >= c && (e._startTime += a), e = e._next;
                } if (b) for (d in f) {
                    f[d] >= c && (f[d] += a);
                } return this._uncache(!0);
            }, q._kill = function (a, b) {
                if (!a && !b) return this._enabled(!1, !1); for (var c = b ? this.getTweensOf(b) : this.getChildren(!0, !0, !1), d = c.length, e = !1; --d > -1;) {
                    c[d]._kill(a, b) && (e = !0);
                } return e;
            }, q.clear = function (a) {
                var b = this.getChildren(!1, !0, !0),
                    c = b.length; for (this._time = this._totalTime = 0; --c > -1;) {
                        b[c]._enabled(!1, !1);
                    } return a !== !1 && (this._labels = {}), this._uncache(!0);
            }, q.invalidate = function () {
                for (var b = this._first; b;) {
                    b.invalidate(), b = b._next;
                } return a.prototype.invalidate.call(this);
            }, q._enabled = function (a, c) {
                if (a === this._gc) for (var d = this._first; d;) {
                    d._enabled(a, !0), d = d._next;
                } return b.prototype._enabled.call(this, a, c);
            }, q.totalTime = function (b, c, d) {
                this._forcingPlayhead = !0; var e = a.prototype.totalTime.apply(this, arguments); return this._forcingPlayhead = !1, e;
            }, q.duration = function (a) {
                return arguments.length ? (0 !== this.duration() && 0 !== a && this.timeScale(this._duration / a), this) : (this._dirty && this.totalDuration(), this._duration);
            }, q.totalDuration = function (a) {
                if (!arguments.length) {
                    if (this._dirty) {
                        for (var b, c, d = 0, e = this._last, f = 999999999999; e;) {
                            b = e._prev, e._dirty && e.totalDuration(), e._startTime > f && this._sortChildren && !e._paused ? this.add(e, e._startTime - e._delay) : f = e._startTime, e._startTime < 0 && !e._paused && (d -= e._startTime, this._timeline.smoothChildTiming && (this._startTime += e._startTime / this._timeScale), this.shiftChildren(-e._startTime, !1, -9999999999), f = 0), c = e._startTime + e._totalDuration / e._timeScale, c > d && (d = c), e = b;
                        } this._duration = this._totalDuration = d, this._dirty = !1;
                    } return this._totalDuration;
                } return a && this.totalDuration() ? this.timeScale(this._totalDuration / a) : this;
            }, q.paused = function (b) {
                if (!b) for (var c = this._first, d = this._time; c;) {
                    c._startTime === d && "isPause" === c.data && (c._rawPrevTime = 0), c = c._next;
                } return a.prototype.paused.apply(this, arguments);
            }, q.usesFrames = function () {
                for (var b = this._timeline; b._timeline;) {
                    b = b._timeline;
                } return b === a._rootFramesTimeline;
            }, q.rawTime = function (a) {
                return a && (this._paused || this._repeat && this.time() > 0 && this.totalProgress() < 1) ? this._totalTime % (this._duration + this._repeatDelay) : this._paused ? this._totalTime : (this._timeline.rawTime(a) - this._startTime) * this._timeScale;
            }, d;
    }, !0);
}), _gsScope._gsDefine && _gsScope._gsQueue.pop()(), function (a) {
    "use strict";
    var b = function b() {
        return (_gsScope.GreenSockGlobals || _gsScope)[a];
    }; "function" == typeof define && define.amd ? define(["TweenLite"], b) : "undefined" != typeof module && module.exports && (require("./TweenLite.js"), module.exports = b());
}("TimelineMax");
"use strict";

var _typeof = typeof Symbol === "function" && typeof Symbol.iterator === "symbol" ? function (obj) { return typeof obj; } : function (obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; };

/*!
 * VERSION: 1.19.1
 * DATE: 2017-01-17
 * UPDATES AND DOCS AT: http://greensock.com
 * 
 * Includes all of the following: TweenLite, TweenMax, TimelineLite, TimelineMax, EasePack, CSSPlugin, RoundPropsPlugin, BezierPlugin, AttrPlugin, DirectionalRotationPlugin
 *
 * @license Copyright (c) 2008-2017, GreenSock. All rights reserved.
 * This work is subject to the terms at http://greensock.com/standard-license or for
 * Club GreenSock members, the software agreement that was issued with your membership.
 * 
 * @author: Jack Doyle, jack@greensock.com
 **/
var _gsScope = "undefined" != typeof module && module.exports && "undefined" != typeof global ? global : undefined || window; (_gsScope._gsQueue || (_gsScope._gsQueue = [])).push(function () {
    "use strict";
    _gsScope._gsDefine("TweenMax", ["core.Animation", "core.SimpleTimeline", "TweenLite"], function (a, b, c) {
        var d = function d(a) {
            var b,
                c = [],
                d = a.length; for (b = 0; b !== d; c.push(a[b++])) { } return c;
        },
            e = function e(a, b, c) {
                var d,
                    e,
                    f = a.cycle; for (d in f) {
                        e = f[d], a[d] = "function" == typeof e ? e(c, b[c]) : e[c % e.length];
                    } delete a.cycle;
            },
            f = function f(a, b, d) {
                c.call(this, a, b, d), this._cycle = 0, this._yoyo = this.vars.yoyo === !0, this._repeat = this.vars.repeat || 0, this._repeatDelay = this.vars.repeatDelay || 0, this._dirty = !0, this.render = f.prototype.render;
            },
            g = 1e-10,
            h = c._internals,
            i = h.isSelector,
            j = h.isArray,
            k = f.prototype = c.to({}, .1, {}),
            l = []; f.version = "1.19.1", k.constructor = f, k.kill()._gc = !1, f.killTweensOf = f.killDelayedCallsTo = c.killTweensOf, f.getTweensOf = c.getTweensOf, f.lagSmoothing = c.lagSmoothing, f.ticker = c.ticker, f.render = c.render, k.invalidate = function () {
                return this._yoyo = this.vars.yoyo === !0, this._repeat = this.vars.repeat || 0, this._repeatDelay = this.vars.repeatDelay || 0, this._uncache(!0), c.prototype.invalidate.call(this);
            }, k.updateTo = function (a, b) {
                var d,
                    e = this.ratio,
                    f = this.vars.immediateRender || a.immediateRender; b && this._startTime < this._timeline._time && (this._startTime = this._timeline._time, this._uncache(!1), this._gc ? this._enabled(!0, !1) : this._timeline.insert(this, this._startTime - this._delay)); for (d in a) {
                        this.vars[d] = a[d];
                    } if (this._initted || f) if (b) this._initted = !1, f && this.render(0, !0, !0); else if (this._gc && this._enabled(!0, !1), this._notifyPluginsOfEnabled && this._firstPT && c._onPluginEvent("_onDisable", this), this._time / this._duration > .998) {
                        var g = this._totalTime; this.render(0, !0, !1), this._initted = !1, this.render(g, !0, !1);
                    } else if (this._initted = !1, this._init(), this._time > 0 || f) for (var h, i = 1 / (1 - e), j = this._firstPT; j;) {
                        h = j.s + j.c, j.c *= i, j.s = h - j.c, j = j._next;
                    } return this;
            }, k.render = function (a, b, c) {
                this._initted || 0 === this._duration && this.vars.repeat && this.invalidate(); var d,
                    e,
                    f,
                    i,
                    j,
                    k,
                    l,
                    m,
                    n = this._dirty ? this.totalDuration() : this._totalDuration,
                    o = this._time,
                    p = this._totalTime,
                    q = this._cycle,
                    r = this._duration,
                    s = this._rawPrevTime; if (a >= n - 1e-7 && a >= 0 ? (this._totalTime = n, this._cycle = this._repeat, this._yoyo && 0 !== (1 & this._cycle) ? (this._time = 0, this.ratio = this._ease._calcEnd ? this._ease.getRatio(0) : 0) : (this._time = r, this.ratio = this._ease._calcEnd ? this._ease.getRatio(1) : 1), this._reversed || (d = !0, e = "onComplete", c = c || this._timeline.autoRemoveChildren), 0 === r && (this._initted || !this.vars.lazy || c) && (this._startTime === this._timeline._duration && (a = 0), (0 > s || 0 >= a && a >= -1e-7 || s === g && "isPause" !== this.data) && s !== a && (c = !0, s > g && (e = "onReverseComplete")), this._rawPrevTime = m = !b || a || s === a ? a : g)) : 1e-7 > a ? (this._totalTime = this._time = this._cycle = 0, this.ratio = this._ease._calcEnd ? this._ease.getRatio(0) : 0, (0 !== p || 0 === r && s > 0) && (e = "onReverseComplete", d = this._reversed), 0 > a && (this._active = !1, 0 === r && (this._initted || !this.vars.lazy || c) && (s >= 0 && (c = !0), this._rawPrevTime = m = !b || a || s === a ? a : g)), this._initted || (c = !0)) : (this._totalTime = this._time = a, 0 !== this._repeat && (i = r + this._repeatDelay, this._cycle = this._totalTime / i >> 0, 0 !== this._cycle && this._cycle === this._totalTime / i && a >= p && this._cycle--, this._time = this._totalTime - this._cycle * i, this._yoyo && 0 !== (1 & this._cycle) && (this._time = r - this._time), this._time > r ? this._time = r : this._time < 0 && (this._time = 0)), this._easeType ? (j = this._time / r, k = this._easeType, l = this._easePower, (1 === k || 3 === k && j >= .5) && (j = 1 - j), 3 === k && (j *= 2), 1 === l ? j *= j : 2 === l ? j *= j * j : 3 === l ? j *= j * j * j : 4 === l && (j *= j * j * j * j), 1 === k ? this.ratio = 1 - j : 2 === k ? this.ratio = j : this._time / r < .5 ? this.ratio = j / 2 : this.ratio = 1 - j / 2) : this.ratio = this._ease.getRatio(this._time / r)), o === this._time && !c && q === this._cycle) return void (p !== this._totalTime && this._onUpdate && (b || this._callback("onUpdate"))); if (!this._initted) {
                        if (this._init(), !this._initted || this._gc) return; if (!c && this._firstPT && (this.vars.lazy !== !1 && this._duration || this.vars.lazy && !this._duration)) return this._time = o, this._totalTime = p, this._rawPrevTime = s, this._cycle = q, h.lazyTweens.push(this), void (this._lazy = [a, b]); this._time && !d ? this.ratio = this._ease.getRatio(this._time / r) : d && this._ease._calcEnd && (this.ratio = this._ease.getRatio(0 === this._time ? 0 : 1));
                    } for (this._lazy !== !1 && (this._lazy = !1), this._active || !this._paused && this._time !== o && a >= 0 && (this._active = !0), 0 === p && (2 === this._initted && a > 0 && this._init(), this._startAt && (a >= 0 ? this._startAt.render(a, b, c) : e || (e = "_dummyGS")), this.vars.onStart && (0 !== this._totalTime || 0 === r) && (b || this._callback("onStart"))), f = this._firstPT; f;) {
                        f.f ? f.t[f.p](f.c * this.ratio + f.s) : f.t[f.p] = f.c * this.ratio + f.s, f = f._next;
                    } this._onUpdate && (0 > a && this._startAt && this._startTime && this._startAt.render(a, b, c), b || (this._totalTime !== p || e) && this._callback("onUpdate")), this._cycle !== q && (b || this._gc || this.vars.onRepeat && this._callback("onRepeat")), e && (!this._gc || c) && (0 > a && this._startAt && !this._onUpdate && this._startTime && this._startAt.render(a, b, c), d && (this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !b && this.vars[e] && this._callback(e), 0 === r && this._rawPrevTime === g && m !== g && (this._rawPrevTime = 0));
            }, f.to = function (a, b, c) {
                return new f(a, b, c);
            }, f.from = function (a, b, c) {
                return c.runBackwards = !0, c.immediateRender = 0 != c.immediateRender, new f(a, b, c);
            }, f.fromTo = function (a, b, c, d) {
                return d.startAt = c, d.immediateRender = 0 != d.immediateRender && 0 != c.immediateRender, new f(a, b, d);
            }, f.staggerTo = f.allTo = function (a, b, g, h, k, m, n) {
                h = h || 0; var o,
                    p,
                    q,
                    r,
                    s = 0,
                    t = [],
                    u = function u() {
                        g.onComplete && g.onComplete.apply(g.onCompleteScope || this, arguments), k.apply(n || g.callbackScope || this, m || l);
                    },
                    v = g.cycle,
                    w = g.startAt && g.startAt.cycle; for (j(a) || ("string" == typeof a && (a = c.selector(a) || a), i(a) && (a = d(a))), a = a || [], 0 > h && (a = d(a), a.reverse(), h *= -1), o = a.length - 1, q = 0; o >= q; q++) {
                        p = {}; for (r in g) {
                            p[r] = g[r];
                        } if (v && (e(p, a, q), null != p.duration && (b = p.duration, delete p.duration)), w) {
                            w = p.startAt = {}; for (r in g.startAt) {
                                w[r] = g.startAt[r];
                            } e(p.startAt, a, q);
                        } p.delay = s + (p.delay || 0), q === o && k && (p.onComplete = u), t[q] = new f(a[q], b, p), s += h;
                    } return t;
            }, f.staggerFrom = f.allFrom = function (a, b, c, d, e, g, h) {
                return c.runBackwards = !0, c.immediateRender = 0 != c.immediateRender, f.staggerTo(a, b, c, d, e, g, h);
            }, f.staggerFromTo = f.allFromTo = function (a, b, c, d, e, g, h, i) {
                return d.startAt = c, d.immediateRender = 0 != d.immediateRender && 0 != c.immediateRender, f.staggerTo(a, b, d, e, g, h, i);
            }, f.delayedCall = function (a, b, c, d, e) {
                return new f(b, 0, { delay: a, onComplete: b, onCompleteParams: c, callbackScope: d, onReverseComplete: b, onReverseCompleteParams: c, immediateRender: !1, useFrames: e, overwrite: 0 });
            }, f.set = function (a, b) {
                return new f(a, 0, b);
            }, f.isTweening = function (a) {
                return c.getTweensOf(a, !0).length > 0;
            }; var m = function m(a, b) {
                for (var d = [], e = 0, f = a._first; f;) {
                    f instanceof c ? d[e++] = f : (b && (d[e++] = f), d = d.concat(m(f, b)), e = d.length), f = f._next;
                } return d;
            },
                n = f.getAllTweens = function (b) {
                    return m(a._rootTimeline, b).concat(m(a._rootFramesTimeline, b));
                }; f.killAll = function (a, c, d, e) {
                    null == c && (c = !0), null == d && (d = !0); var f,
                        g,
                        h,
                        i = n(0 != e),
                        j = i.length,
                        k = c && d && e; for (h = 0; j > h; h++) {
                            g = i[h], (k || g instanceof b || (f = g.target === g.vars.onComplete) && d || c && !f) && (a ? g.totalTime(g._reversed ? 0 : g.totalDuration()) : g._enabled(!1, !1));
                        }
                }, f.killChildTweensOf = function (a, b) {
                    if (null != a) {
                        var e,
                            g,
                            k,
                            l,
                            m,
                            n = h.tweenLookup; if ("string" == typeof a && (a = c.selector(a) || a), i(a) && (a = d(a)), j(a)) for (l = a.length; --l > -1;) {
                                f.killChildTweensOf(a[l], b);
                            } else {
                                e = []; for (k in n) {
                                    for (g = n[k].target.parentNode; g;) {
                                        g === a && (e = e.concat(n[k].tweens)), g = g.parentNode;
                                    }
                                } for (m = e.length, l = 0; m > l; l++) {
                                    b && e[l].totalTime(e[l].totalDuration()), e[l]._enabled(!1, !1);
                                }
                            }
                    }
                }; var o = function o(a, c, d, e) {
                    c = c !== !1, d = d !== !1, e = e !== !1; for (var f, g, h = n(e), i = c && d && e, j = h.length; --j > -1;) {
                        g = h[j], (i || g instanceof b || (f = g.target === g.vars.onComplete) && d || c && !f) && g.paused(a);
                    }
                }; return f.pauseAll = function (a, b, c) {
                    o(!0, a, b, c);
                }, f.resumeAll = function (a, b, c) {
                    o(!1, a, b, c);
                }, f.globalTimeScale = function (b) {
                    var d = a._rootTimeline,
                        e = c.ticker.time; return arguments.length ? (b = b || g, d._startTime = e - (e - d._startTime) * d._timeScale / b, d = a._rootFramesTimeline, e = c.ticker.frame, d._startTime = e - (e - d._startTime) * d._timeScale / b, d._timeScale = a._rootTimeline._timeScale = b, b) : d._timeScale;
                }, k.progress = function (a, b) {
                    return arguments.length ? this.totalTime(this.duration() * (this._yoyo && 0 !== (1 & this._cycle) ? 1 - a : a) + this._cycle * (this._duration + this._repeatDelay), b) : this._time / this.duration();
                }, k.totalProgress = function (a, b) {
                    return arguments.length ? this.totalTime(this.totalDuration() * a, b) : this._totalTime / this.totalDuration();
                }, k.time = function (a, b) {
                    return arguments.length ? (this._dirty && this.totalDuration(), a > this._duration && (a = this._duration), this._yoyo && 0 !== (1 & this._cycle) ? a = this._duration - a + this._cycle * (this._duration + this._repeatDelay) : 0 !== this._repeat && (a += this._cycle * (this._duration + this._repeatDelay)), this.totalTime(a, b)) : this._time;
                }, k.duration = function (b) {
                    return arguments.length ? a.prototype.duration.call(this, b) : this._duration;
                }, k.totalDuration = function (a) {
                    return arguments.length ? -1 === this._repeat ? this : this.duration((a - this._repeat * this._repeatDelay) / (this._repeat + 1)) : (this._dirty && (this._totalDuration = -1 === this._repeat ? 999999999999 : this._duration * (this._repeat + 1) + this._repeatDelay * this._repeat, this._dirty = !1), this._totalDuration);
                }, k.repeat = function (a) {
                    return arguments.length ? (this._repeat = a, this._uncache(!0)) : this._repeat;
                }, k.repeatDelay = function (a) {
                    return arguments.length ? (this._repeatDelay = a, this._uncache(!0)) : this._repeatDelay;
                }, k.yoyo = function (a) {
                    return arguments.length ? (this._yoyo = a, this) : this._yoyo;
                }, f;
    }, !0), _gsScope._gsDefine("TimelineLite", ["core.Animation", "core.SimpleTimeline", "TweenLite"], function (a, b, c) {
        var d = function d(a) {
            b.call(this, a), this._labels = {}, this.autoRemoveChildren = this.vars.autoRemoveChildren === !0, this.smoothChildTiming = this.vars.smoothChildTiming === !0, this._sortChildren = !0, this._onUpdate = this.vars.onUpdate; var c,
                d,
                e = this.vars; for (d in e) {
                    c = e[d], i(c) && -1 !== c.join("").indexOf("{self}") && (e[d] = this._swapSelfInParams(c));
                } i(e.tweens) && this.add(e.tweens, 0, e.align, e.stagger);
        },
            e = 1e-10,
            f = c._internals,
            g = d._internals = {},
            h = f.isSelector,
            i = f.isArray,
            j = f.lazyTweens,
            k = f.lazyRender,
            l = _gsScope._gsDefine.globals,
            m = function m(a) {
                var b,
                    c = {}; for (b in a) {
                        c[b] = a[b];
                    } return c;
            },
            n = function n(a, b, c) {
                var d,
                    e,
                    f = a.cycle; for (d in f) {
                        e = f[d], a[d] = "function" == typeof e ? e(c, b[c]) : e[c % e.length];
                    } delete a.cycle;
            },
            o = g.pauseCallback = function () { },
            p = function p(a) {
                var b,
                    c = [],
                    d = a.length; for (b = 0; b !== d; c.push(a[b++])) { } return c;
            },
            q = d.prototype = new b(); return d.version = "1.19.1", q.constructor = d, q.kill()._gc = q._forcingPlayhead = q._hasPause = !1, q.to = function (a, b, d, e) {
                var f = d.repeat && l.TweenMax || c; return b ? this.add(new f(a, b, d), e) : this.set(a, d, e);
            }, q.from = function (a, b, d, e) {
                return this.add((d.repeat && l.TweenMax || c).from(a, b, d), e);
            }, q.fromTo = function (a, b, d, e, f) {
                var g = e.repeat && l.TweenMax || c; return b ? this.add(g.fromTo(a, b, d, e), f) : this.set(a, e, f);
            }, q.staggerTo = function (a, b, e, f, g, i, j, k) {
                var l,
                    o,
                    q = new d({ onComplete: i, onCompleteParams: j, callbackScope: k, smoothChildTiming: this.smoothChildTiming }),
                    r = e.cycle; for ("string" == typeof a && (a = c.selector(a) || a), a = a || [], h(a) && (a = p(a)), f = f || 0, 0 > f && (a = p(a), a.reverse(), f *= -1), o = 0; o < a.length; o++) {
                        l = m(e), l.startAt && (l.startAt = m(l.startAt), l.startAt.cycle && n(l.startAt, a, o)), r && (n(l, a, o), null != l.duration && (b = l.duration, delete l.duration)), q.to(a[o], b, l, o * f);
                    } return this.add(q, g);
            }, q.staggerFrom = function (a, b, c, d, e, f, g, h) {
                return c.immediateRender = 0 != c.immediateRender, c.runBackwards = !0, this.staggerTo(a, b, c, d, e, f, g, h);
            }, q.staggerFromTo = function (a, b, c, d, e, f, g, h, i) {
                return d.startAt = c, d.immediateRender = 0 != d.immediateRender && 0 != c.immediateRender, this.staggerTo(a, b, d, e, f, g, h, i);
            }, q.call = function (a, b, d, e) {
                return this.add(c.delayedCall(0, a, b, d), e);
            }, q.set = function (a, b, d) {
                return d = this._parseTimeOrLabel(d, 0, !0), null == b.immediateRender && (b.immediateRender = d === this._time && !this._paused), this.add(new c(a, 0, b), d);
            }, d.exportRoot = function (a, b) {
                a = a || {}, null == a.smoothChildTiming && (a.smoothChildTiming = !0); var e,
                    f,
                    g = new d(a),
                    h = g._timeline; for (null == b && (b = !0), h._remove(g, !0), g._startTime = 0, g._rawPrevTime = g._time = g._totalTime = h._time, e = h._first; e;) {
                        f = e._next, b && e instanceof c && e.target === e.vars.onComplete || g.add(e, e._startTime - e._delay), e = f;
                    } return h.add(g, 0), g;
            }, q.add = function (e, f, g, h) {
                var j, k, l, m, n, o; if ("number" != typeof f && (f = this._parseTimeOrLabel(f, 0, !0, e)), !(e instanceof a)) {
                    if (e instanceof Array || e && e.push && i(e)) {
                        for (g = g || "normal", h = h || 0, j = f, k = e.length, l = 0; k > l; l++) {
                            i(m = e[l]) && (m = new d({ tweens: m })), this.add(m, j), "string" != typeof m && "function" != typeof m && ("sequence" === g ? j = m._startTime + m.totalDuration() / m._timeScale : "start" === g && (m._startTime -= m.delay())), j += h;
                        } return this._uncache(!0);
                    } if ("string" == typeof e) return this.addLabel(e, f); if ("function" != typeof e) throw "Cannot add " + e + " into the timeline; it is not a tween, timeline, function, or string."; e = c.delayedCall(0, e);
                } if (b.prototype.add.call(this, e, f), (this._gc || this._time === this._duration) && !this._paused && this._duration < this.duration()) for (n = this, o = n.rawTime() > e._startTime; n._timeline;) {
                    o && n._timeline.smoothChildTiming ? n.totalTime(n._totalTime, !0) : n._gc && n._enabled(!0, !1), n = n._timeline;
                } return this;
            }, q.remove = function (b) {
                if (b instanceof a) {
                    this._remove(b, !1); var c = b._timeline = b.vars.useFrames ? a._rootFramesTimeline : a._rootTimeline; return b._startTime = (b._paused ? b._pauseTime : c._time) - (b._reversed ? b.totalDuration() - b._totalTime : b._totalTime) / b._timeScale, this;
                } if (b instanceof Array || b && b.push && i(b)) {
                    for (var d = b.length; --d > -1;) {
                        this.remove(b[d]);
                    } return this;
                } return "string" == typeof b ? this.removeLabel(b) : this.kill(null, b);
            }, q._remove = function (a, c) {
                b.prototype._remove.call(this, a, c); var d = this._last; return d ? this._time > this.duration() && (this._time = this._duration, this._totalTime = this._totalDuration) : this._time = this._totalTime = this._duration = this._totalDuration = 0, this;
            }, q.append = function (a, b) {
                return this.add(a, this._parseTimeOrLabel(null, b, !0, a));
            }, q.insert = q.insertMultiple = function (a, b, c, d) {
                return this.add(a, b || 0, c, d);
            }, q.appendMultiple = function (a, b, c, d) {
                return this.add(a, this._parseTimeOrLabel(null, b, !0, a), c, d);
            }, q.addLabel = function (a, b) {
                return this._labels[a] = this._parseTimeOrLabel(b), this;
            }, q.addPause = function (a, b, d, e) {
                var f = c.delayedCall(0, o, d, e || this); return f.vars.onComplete = f.vars.onReverseComplete = b, f.data = "isPause", this._hasPause = !0, this.add(f, a);
            }, q.removeLabel = function (a) {
                return delete this._labels[a], this;
            }, q.getLabelTime = function (a) {
                return null != this._labels[a] ? this._labels[a] : -1;
            }, q._parseTimeOrLabel = function (b, c, d, e) {
                var f; if (e instanceof a && e.timeline === this) this.remove(e); else if (e && (e instanceof Array || e.push && i(e))) for (f = e.length; --f > -1;) {
                    e[f] instanceof a && e[f].timeline === this && this.remove(e[f]);
                } if ("string" == typeof c) return this._parseTimeOrLabel(c, d && "number" == typeof b && null == this._labels[c] ? b - this.duration() : 0, d); if (c = c || 0, "string" != typeof b || !isNaN(b) && null == this._labels[b]) null == b && (b = this.duration()); else {
                    if (f = b.indexOf("="), -1 === f) return null == this._labels[b] ? d ? this._labels[b] = this.duration() + c : c : this._labels[b] + c; c = parseInt(b.charAt(f - 1) + "1", 10) * Number(b.substr(f + 1)), b = f > 1 ? this._parseTimeOrLabel(b.substr(0, f - 1), 0, d) : this.duration();
                } return Number(b) + c;
            }, q.seek = function (a, b) {
                return this.totalTime("number" == typeof a ? a : this._parseTimeOrLabel(a), b !== !1);
            }, q.stop = function () {
                return this.paused(!0);
            }, q.gotoAndPlay = function (a, b) {
                return this.play(a, b);
            }, q.gotoAndStop = function (a, b) {
                return this.pause(a, b);
            }, q.render = function (a, b, c) {
                this._gc && this._enabled(!0, !1); var d,
                    f,
                    g,
                    h,
                    i,
                    l,
                    m,
                    n = this._dirty ? this.totalDuration() : this._totalDuration,
                    o = this._time,
                    p = this._startTime,
                    q = this._timeScale,
                    r = this._paused; if (a >= n - 1e-7 && a >= 0) this._totalTime = this._time = n, this._reversed || this._hasPausedChild() || (f = !0, h = "onComplete", i = !!this._timeline.autoRemoveChildren, 0 === this._duration && (0 >= a && a >= -1e-7 || this._rawPrevTime < 0 || this._rawPrevTime === e) && this._rawPrevTime !== a && this._first && (i = !0, this._rawPrevTime > e && (h = "onReverseComplete"))), this._rawPrevTime = this._duration || !b || a || this._rawPrevTime === a ? a : e, a = n + 1e-4; else if (1e-7 > a) {
                        if (this._totalTime = this._time = 0, (0 !== o || 0 === this._duration && this._rawPrevTime !== e && (this._rawPrevTime > 0 || 0 > a && this._rawPrevTime >= 0)) && (h = "onReverseComplete", f = this._reversed), 0 > a) this._active = !1, this._timeline.autoRemoveChildren && this._reversed ? (i = f = !0, h = "onReverseComplete") : this._rawPrevTime >= 0 && this._first && (i = !0), this._rawPrevTime = a; else {
                            if (this._rawPrevTime = this._duration || !b || a || this._rawPrevTime === a ? a : e, 0 === a && f) for (d = this._first; d && 0 === d._startTime;) {
                                d._duration || (f = !1), d = d._next;
                            } a = 0, this._initted || (i = !0);
                        }
                    } else {
                        if (this._hasPause && !this._forcingPlayhead && !b) {
                            if (a >= o) for (d = this._first; d && d._startTime <= a && !l;) {
                                d._duration || "isPause" !== d.data || d.ratio || 0 === d._startTime && 0 === this._rawPrevTime || (l = d), d = d._next;
                            } else for (d = this._last; d && d._startTime >= a && !l;) {
                                d._duration || "isPause" === d.data && d._rawPrevTime > 0 && (l = d), d = d._prev;
                            } l && (this._time = a = l._startTime, this._totalTime = a + this._cycle * (this._totalDuration + this._repeatDelay));
                        } this._totalTime = this._time = this._rawPrevTime = a;
                    } if (this._time !== o && this._first || c || i || l) {
                        if (this._initted || (this._initted = !0), this._active || !this._paused && this._time !== o && a > 0 && (this._active = !0), 0 === o && this.vars.onStart && (0 === this._time && this._duration || b || this._callback("onStart")), m = this._time, m >= o) for (d = this._first; d && (g = d._next, m === this._time && (!this._paused || r)) ;) {
                            (d._active || d._startTime <= m && !d._paused && !d._gc) && (l === d && this.pause(), d._reversed ? d.render((d._dirty ? d.totalDuration() : d._totalDuration) - (a - d._startTime) * d._timeScale, b, c) : d.render((a - d._startTime) * d._timeScale, b, c)), d = g;
                        } else for (d = this._last; d && (g = d._prev, m === this._time && (!this._paused || r)) ;) {
                            if (d._active || d._startTime <= o && !d._paused && !d._gc) {
                                if (l === d) {
                                    for (l = d._prev; l && l.endTime() > this._time;) {
                                        l.render(l._reversed ? l.totalDuration() - (a - l._startTime) * l._timeScale : (a - l._startTime) * l._timeScale, b, c), l = l._prev;
                                    } l = null, this.pause();
                                } d._reversed ? d.render((d._dirty ? d.totalDuration() : d._totalDuration) - (a - d._startTime) * d._timeScale, b, c) : d.render((a - d._startTime) * d._timeScale, b, c);
                            } d = g;
                        } this._onUpdate && (b || (j.length && k(), this._callback("onUpdate"))), h && (this._gc || (p === this._startTime || q !== this._timeScale) && (0 === this._time || n >= this.totalDuration()) && (f && (j.length && k(), this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !b && this.vars[h] && this._callback(h)));
                    }
            }, q._hasPausedChild = function () {
                for (var a = this._first; a;) {
                    if (a._paused || a instanceof d && a._hasPausedChild()) return !0; a = a._next;
                } return !1;
            }, q.getChildren = function (a, b, d, e) {
                e = e || -9999999999; for (var f = [], g = this._first, h = 0; g;) {
                    g._startTime < e || (g instanceof c ? b !== !1 && (f[h++] = g) : (d !== !1 && (f[h++] = g), a !== !1 && (f = f.concat(g.getChildren(!0, b, d)), h = f.length))), g = g._next;
                } return f;
            }, q.getTweensOf = function (a, b) {
                var d,
                    e,
                    f = this._gc,
                    g = [],
                    h = 0; for (f && this._enabled(!0, !0), d = c.getTweensOf(a), e = d.length; --e > -1;) {
                        (d[e].timeline === this || b && this._contains(d[e])) && (g[h++] = d[e]);
                    } return f && this._enabled(!1, !0), g;
            }, q.recent = function () {
                return this._recent;
            }, q._contains = function (a) {
                for (var b = a.timeline; b;) {
                    if (b === this) return !0; b = b.timeline;
                } return !1;
            }, q.shiftChildren = function (a, b, c) {
                c = c || 0; for (var d, e = this._first, f = this._labels; e;) {
                    e._startTime >= c && (e._startTime += a), e = e._next;
                } if (b) for (d in f) {
                    f[d] >= c && (f[d] += a);
                } return this._uncache(!0);
            }, q._kill = function (a, b) {
                if (!a && !b) return this._enabled(!1, !1); for (var c = b ? this.getTweensOf(b) : this.getChildren(!0, !0, !1), d = c.length, e = !1; --d > -1;) {
                    c[d]._kill(a, b) && (e = !0);
                } return e;
            }, q.clear = function (a) {
                var b = this.getChildren(!1, !0, !0),
                    c = b.length; for (this._time = this._totalTime = 0; --c > -1;) {
                        b[c]._enabled(!1, !1);
                    } return a !== !1 && (this._labels = {}), this._uncache(!0);
            }, q.invalidate = function () {
                for (var b = this._first; b;) {
                    b.invalidate(), b = b._next;
                } return a.prototype.invalidate.call(this);
            }, q._enabled = function (a, c) {
                if (a === this._gc) for (var d = this._first; d;) {
                    d._enabled(a, !0), d = d._next;
                } return b.prototype._enabled.call(this, a, c);
            }, q.totalTime = function (b, c, d) {
                this._forcingPlayhead = !0; var e = a.prototype.totalTime.apply(this, arguments); return this._forcingPlayhead = !1, e;
            }, q.duration = function (a) {
                return arguments.length ? (0 !== this.duration() && 0 !== a && this.timeScale(this._duration / a), this) : (this._dirty && this.totalDuration(), this._duration);
            }, q.totalDuration = function (a) {
                if (!arguments.length) {
                    if (this._dirty) {
                        for (var b, c, d = 0, e = this._last, f = 999999999999; e;) {
                            b = e._prev, e._dirty && e.totalDuration(), e._startTime > f && this._sortChildren && !e._paused ? this.add(e, e._startTime - e._delay) : f = e._startTime, e._startTime < 0 && !e._paused && (d -= e._startTime, this._timeline.smoothChildTiming && (this._startTime += e._startTime / this._timeScale), this.shiftChildren(-e._startTime, !1, -9999999999), f = 0), c = e._startTime + e._totalDuration / e._timeScale, c > d && (d = c), e = b;
                        } this._duration = this._totalDuration = d, this._dirty = !1;
                    } return this._totalDuration;
                } return a && this.totalDuration() ? this.timeScale(this._totalDuration / a) : this;
            }, q.paused = function (b) {
                if (!b) for (var c = this._first, d = this._time; c;) {
                    c._startTime === d && "isPause" === c.data && (c._rawPrevTime = 0), c = c._next;
                } return a.prototype.paused.apply(this, arguments);
            }, q.usesFrames = function () {
                for (var b = this._timeline; b._timeline;) {
                    b = b._timeline;
                } return b === a._rootFramesTimeline;
            }, q.rawTime = function (a) {
                return a && (this._paused || this._repeat && this.time() > 0 && this.totalProgress() < 1) ? this._totalTime % (this._duration + this._repeatDelay) : this._paused ? this._totalTime : (this._timeline.rawTime(a) - this._startTime) * this._timeScale;
            }, d;
    }, !0), _gsScope._gsDefine("TimelineMax", ["TimelineLite", "TweenLite", "easing.Ease"], function (a, b, c) {
        var d = function d(b) {
            a.call(this, b), this._repeat = this.vars.repeat || 0, this._repeatDelay = this.vars.repeatDelay || 0, this._cycle = 0, this._yoyo = this.vars.yoyo === !0, this._dirty = !0;
        },
            e = 1e-10,
            f = b._internals,
            g = f.lazyTweens,
            h = f.lazyRender,
            i = _gsScope._gsDefine.globals,
            j = new c(null, null, 1, 0),
            k = d.prototype = new a(); return k.constructor = d, k.kill()._gc = !1, d.version = "1.19.1", k.invalidate = function () {
                return this._yoyo = this.vars.yoyo === !0, this._repeat = this.vars.repeat || 0, this._repeatDelay = this.vars.repeatDelay || 0, this._uncache(!0), a.prototype.invalidate.call(this);
            }, k.addCallback = function (a, c, d, e) {
                return this.add(b.delayedCall(0, a, d, e), c);
            }, k.removeCallback = function (a, b) {
                if (a) if (null == b) this._kill(null, a); else for (var c = this.getTweensOf(a, !1), d = c.length, e = this._parseTimeOrLabel(b) ; --d > -1;) {
                    c[d]._startTime === e && c[d]._enabled(!1, !1);
                } return this;
            }, k.removePause = function (b) {
                return this.removeCallback(a._internals.pauseCallback, b);
            }, k.tweenTo = function (a, c) {
                c = c || {}; var d,
                    e,
                    f,
                    g = { ease: j, useFrames: this.usesFrames(), immediateRender: !1 },
                    h = c.repeat && i.TweenMax || b; for (e in c) {
                        g[e] = c[e];
                    } return g.time = this._parseTimeOrLabel(a), d = Math.abs(Number(g.time) - this._time) / this._timeScale || .001, f = new h(this, d, g), g.onStart = function () {
                        f.target.paused(!0), f.vars.time !== f.target.time() && d === f.duration() && f.duration(Math.abs(f.vars.time - f.target.time()) / f.target._timeScale), c.onStart && c.onStart.apply(c.onStartScope || c.callbackScope || f, c.onStartParams || []);
                    }, f;
            }, k.tweenFromTo = function (a, b, c) {
                c = c || {}, a = this._parseTimeOrLabel(a), c.startAt = { onComplete: this.seek, onCompleteParams: [a], callbackScope: this }, c.immediateRender = c.immediateRender !== !1; var d = this.tweenTo(b, c); return d.duration(Math.abs(d.vars.time - a) / this._timeScale || .001);
            }, k.render = function (a, b, c) {
                this._gc && this._enabled(!0, !1); var d,
                    f,
                    i,
                    j,
                    k,
                    l,
                    m,
                    n,
                    o = this._dirty ? this.totalDuration() : this._totalDuration,
                    p = this._duration,
                    q = this._time,
                    r = this._totalTime,
                    s = this._startTime,
                    t = this._timeScale,
                    u = this._rawPrevTime,
                    v = this._paused,
                    w = this._cycle; if (a >= o - 1e-7 && a >= 0) this._locked || (this._totalTime = o, this._cycle = this._repeat), this._reversed || this._hasPausedChild() || (f = !0, j = "onComplete", k = !!this._timeline.autoRemoveChildren, 0 === this._duration && (0 >= a && a >= -1e-7 || 0 > u || u === e) && u !== a && this._first && (k = !0, u > e && (j = "onReverseComplete"))), this._rawPrevTime = this._duration || !b || a || this._rawPrevTime === a ? a : e, this._yoyo && 0 !== (1 & this._cycle) ? this._time = a = 0 : (this._time = p, a = p + 1e-4); else if (1e-7 > a) {
                        if (this._locked || (this._totalTime = this._cycle = 0), this._time = 0, (0 !== q || 0 === p && u !== e && (u > 0 || 0 > a && u >= 0) && !this._locked) && (j = "onReverseComplete", f = this._reversed), 0 > a) this._active = !1, this._timeline.autoRemoveChildren && this._reversed ? (k = f = !0, j = "onReverseComplete") : u >= 0 && this._first && (k = !0), this._rawPrevTime = a; else {
                            if (this._rawPrevTime = p || !b || a || this._rawPrevTime === a ? a : e, 0 === a && f) for (d = this._first; d && 0 === d._startTime;) {
                                d._duration || (f = !1), d = d._next;
                            } a = 0, this._initted || (k = !0);
                        }
                    } else if (0 === p && 0 > u && (k = !0), this._time = this._rawPrevTime = a, this._locked || (this._totalTime = a, 0 !== this._repeat && (l = p + this._repeatDelay, this._cycle = this._totalTime / l >> 0, 0 !== this._cycle && this._cycle === this._totalTime / l && a >= r && this._cycle--, this._time = this._totalTime - this._cycle * l, this._yoyo && 0 !== (1 & this._cycle) && (this._time = p - this._time), this._time > p ? (this._time = p, a = p + 1e-4) : this._time < 0 ? this._time = a = 0 : a = this._time)), this._hasPause && !this._forcingPlayhead && !b && p > a) {
                        if (a = this._time, a >= q || this._repeat && w !== this._cycle) for (d = this._first; d && d._startTime <= a && !m;) {
                            d._duration || "isPause" !== d.data || d.ratio || 0 === d._startTime && 0 === this._rawPrevTime || (m = d), d = d._next;
                        } else for (d = this._last; d && d._startTime >= a && !m;) {
                            d._duration || "isPause" === d.data && d._rawPrevTime > 0 && (m = d), d = d._prev;
                        } m && (this._time = a = m._startTime, this._totalTime = a + this._cycle * (this._totalDuration + this._repeatDelay));
                    } if (this._cycle !== w && !this._locked) {
                        var x = this._yoyo && 0 !== (1 & w),
                            y = x === (this._yoyo && 0 !== (1 & this._cycle)),
                            z = this._totalTime,
                            A = this._cycle,
                            B = this._rawPrevTime,
                            C = this._time; if (this._totalTime = w * p, this._cycle < w ? x = !x : this._totalTime += p, this._time = q, this._rawPrevTime = 0 === p ? u - 1e-4 : u, this._cycle = w, this._locked = !0, q = x ? 0 : p, this.render(q, b, 0 === p), b || this._gc || this.vars.onRepeat && (this._cycle = A, this._locked = !1, this._callback("onRepeat")), q !== this._time) return; if (y && (this._cycle = w, this._locked = !0, q = x ? p + 1e-4 : -1e-4, this.render(q, !0, !1)), this._locked = !1, this._paused && !v) return; this._time = C, this._totalTime = z, this._cycle = A, this._rawPrevTime = B;
                    } if (!(this._time !== q && this._first || c || k || m)) return void (r !== this._totalTime && this._onUpdate && (b || this._callback("onUpdate"))); if (this._initted || (this._initted = !0), this._active || !this._paused && this._totalTime !== r && a > 0 && (this._active = !0), 0 === r && this.vars.onStart && (0 === this._totalTime && this._totalDuration || b || this._callback("onStart")), n = this._time, n >= q) for (d = this._first; d && (i = d._next, n === this._time && (!this._paused || v)) ;) {
                        (d._active || d._startTime <= this._time && !d._paused && !d._gc) && (m === d && this.pause(), d._reversed ? d.render((d._dirty ? d.totalDuration() : d._totalDuration) - (a - d._startTime) * d._timeScale, b, c) : d.render((a - d._startTime) * d._timeScale, b, c)), d = i;
                    } else for (d = this._last; d && (i = d._prev, n === this._time && (!this._paused || v)) ;) {
                        if (d._active || d._startTime <= q && !d._paused && !d._gc) {
                            if (m === d) {
                                for (m = d._prev; m && m.endTime() > this._time;) {
                                    m.render(m._reversed ? m.totalDuration() - (a - m._startTime) * m._timeScale : (a - m._startTime) * m._timeScale, b, c), m = m._prev;
                                } m = null, this.pause();
                            } d._reversed ? d.render((d._dirty ? d.totalDuration() : d._totalDuration) - (a - d._startTime) * d._timeScale, b, c) : d.render((a - d._startTime) * d._timeScale, b, c);
                        } d = i;
                    } this._onUpdate && (b || (g.length && h(), this._callback("onUpdate"))), j && (this._locked || this._gc || (s === this._startTime || t !== this._timeScale) && (0 === this._time || o >= this.totalDuration()) && (f && (g.length && h(), this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !b && this.vars[j] && this._callback(j)));
            }, k.getActive = function (a, b, c) {
                null == a && (a = !0), null == b && (b = !0), null == c && (c = !1); var d,
                    e,
                    f = [],
                    g = this.getChildren(a, b, c),
                    h = 0,
                    i = g.length; for (d = 0; i > d; d++) {
                        e = g[d], e.isActive() && (f[h++] = e);
                    } return f;
            }, k.getLabelAfter = function (a) {
                a || 0 !== a && (a = this._time); var b,
                    c = this.getLabelsArray(),
                    d = c.length; for (b = 0; d > b; b++) {
                        if (c[b].time > a) return c[b].name;
                    } return null;
            }, k.getLabelBefore = function (a) {
                null == a && (a = this._time); for (var b = this.getLabelsArray(), c = b.length; --c > -1;) {
                    if (b[c].time < a) return b[c].name;
                } return null;
            }, k.getLabelsArray = function () {
                var a,
                    b = [],
                    c = 0; for (a in this._labels) {
                        b[c++] = { time: this._labels[a], name: a };
                    } return b.sort(function (a, b) {
                        return a.time - b.time;
                    }), b;
            }, k.invalidate = function () {
                return this._locked = !1, a.prototype.invalidate.call(this);
            }, k.progress = function (a, b) {
                return arguments.length ? this.totalTime(this.duration() * (this._yoyo && 0 !== (1 & this._cycle) ? 1 - a : a) + this._cycle * (this._duration + this._repeatDelay), b) : this._time / this.duration();
            }, k.totalProgress = function (a, b) {
                return arguments.length ? this.totalTime(this.totalDuration() * a, b) : this._totalTime / this.totalDuration();
            }, k.totalDuration = function (b) {
                return arguments.length ? -1 !== this._repeat && b ? this.timeScale(this.totalDuration() / b) : this : (this._dirty && (a.prototype.totalDuration.call(this), this._totalDuration = -1 === this._repeat ? 999999999999 : this._duration * (this._repeat + 1) + this._repeatDelay * this._repeat), this._totalDuration);
            }, k.time = function (a, b) {
                return arguments.length ? (this._dirty && this.totalDuration(), a > this._duration && (a = this._duration), this._yoyo && 0 !== (1 & this._cycle) ? a = this._duration - a + this._cycle * (this._duration + this._repeatDelay) : 0 !== this._repeat && (a += this._cycle * (this._duration + this._repeatDelay)), this.totalTime(a, b)) : this._time;
            }, k.repeat = function (a) {
                return arguments.length ? (this._repeat = a, this._uncache(!0)) : this._repeat;
            }, k.repeatDelay = function (a) {
                return arguments.length ? (this._repeatDelay = a, this._uncache(!0)) : this._repeatDelay;
            }, k.yoyo = function (a) {
                return arguments.length ? (this._yoyo = a, this) : this._yoyo;
            }, k.currentLabel = function (a) {
                return arguments.length ? this.seek(a, !0) : this.getLabelBefore(this._time + 1e-8);
            }, d;
    }, !0), function () {
        var a = 180 / Math.PI,
            b = [],
            c = [],
            d = [],
            e = {},
            f = _gsScope._gsDefine.globals,
            g = function g(a, b, c, d) {
                c === d && (c = d - (d - b) / 1e6), a === b && (b = a + (c - a) / 1e6), this.a = a, this.b = b, this.c = c, this.d = d, this.da = d - a, this.ca = c - a, this.ba = b - a;
            },
            h = ",x,y,z,left,top,right,bottom,marginTop,marginLeft,marginRight,marginBottom,paddingLeft,paddingTop,paddingRight,paddingBottom,backgroundPosition,backgroundPosition_y,",
            i = function i(a, b, c, d) {
                var e = { a: a },
                    f = {},
                    g = {},
                    h = { c: d },
                    i = (a + b) / 2,
                    j = (b + c) / 2,
                    k = (c + d) / 2,
                    l = (i + j) / 2,
                    m = (j + k) / 2,
                    n = (m - l) / 8; return e.b = i + (a - i) / 4, f.b = l + n, e.c = f.a = (e.b + f.b) / 2, f.c = g.a = (l + m) / 2, g.b = m - n, h.b = k + (d - k) / 4, g.c = h.a = (g.b + h.b) / 2, [e, f, g, h];
            },
            j = function j(a, e, f, g, h) {
                var j,
                    k,
                    l,
                    m,
                    n,
                    o,
                    p,
                    q,
                    r,
                    s,
                    t,
                    u,
                    v,
                    w = a.length - 1,
                    x = 0,
                    y = a[0].a; for (j = 0; w > j; j++) {
                        n = a[x], k = n.a, l = n.d, m = a[x + 1].d, h ? (t = b[j], u = c[j], v = (u + t) * e * .25 / (g ? .5 : d[j] || .5), o = l - (l - k) * (g ? .5 * e : 0 !== t ? v / t : 0), p = l + (m - l) * (g ? .5 * e : 0 !== u ? v / u : 0), q = l - (o + ((p - o) * (3 * t / (t + u) + .5) / 4 || 0))) : (o = l - (l - k) * e * .5, p = l + (m - l) * e * .5, q = l - (o + p) / 2), o += q, p += q, n.c = r = o, 0 !== j ? n.b = y : n.b = y = n.a + .6 * (n.c - n.a), n.da = l - k, n.ca = r - k, n.ba = y - k, f ? (s = i(k, y, r, l), a.splice(x, 1, s[0], s[1], s[2], s[3]), x += 4) : x++, y = p;
                    } n = a[x], n.b = y, n.c = y + .4 * (n.d - y), n.da = n.d - n.a, n.ca = n.c - n.a, n.ba = y - n.a, f && (s = i(n.a, y, n.c, n.d), a.splice(x, 1, s[0], s[1], s[2], s[3]));
            },
            k = function k(a, d, e, f) {
                var h,
                    i,
                    j,
                    k,
                    l,
                    m,
                    n = []; if (f) for (a = [f].concat(a), i = a.length; --i > -1;) {
                        "string" == typeof (m = a[i][d]) && "=" === m.charAt(1) && (a[i][d] = f[d] + Number(m.charAt(0) + m.substr(2)));
                    } if (h = a.length - 2, 0 > h) return n[0] = new g(a[0][d], 0, 0, a[-1 > h ? 0 : 1][d]), n; for (i = 0; h > i; i++) {
                        j = a[i][d], k = a[i + 1][d], n[i] = new g(j, 0, 0, k), e && (l = a[i + 2][d], b[i] = (b[i] || 0) + (k - j) * (k - j), c[i] = (c[i] || 0) + (l - k) * (l - k));
                    } return n[i] = new g(a[i][d], 0, 0, a[i + 1][d]), n;
            },
            l = function l(a, f, g, i, _l, m) {
                var n,
                    o,
                    p,
                    q,
                    r,
                    s,
                    t,
                    u,
                    v = {},
                    w = [],
                    x = m || a[0]; _l = "string" == typeof _l ? "," + _l + "," : h, null == f && (f = 1); for (o in a[0]) {
                        w.push(o);
                    } if (a.length > 1) {
                        for (u = a[a.length - 1], t = !0, n = w.length; --n > -1;) {
                            if (o = w[n], Math.abs(x[o] - u[o]) > .05) {
                                t = !1; break;
                            }
                        } t && (a = a.concat(), m && a.unshift(m), a.push(a[1]), m = a[a.length - 3]);
                    } for (b.length = c.length = d.length = 0, n = w.length; --n > -1;) {
                        o = w[n], e[o] = -1 !== _l.indexOf("," + o + ","), v[o] = k(a, o, e[o], m);
                    } for (n = b.length; --n > -1;) {
                        b[n] = Math.sqrt(b[n]), c[n] = Math.sqrt(c[n]);
                    } if (!i) {
                        for (n = w.length; --n > -1;) {
                            if (e[o]) for (p = v[w[n]], s = p.length - 1, q = 0; s > q; q++) {
                                r = p[q + 1].da / c[q] + p[q].da / b[q] || 0, d[q] = (d[q] || 0) + r * r;
                            }
                        } for (n = d.length; --n > -1;) {
                            d[n] = Math.sqrt(d[n]);
                        }
                    } for (n = w.length, q = g ? 4 : 1; --n > -1;) {
                        o = w[n], p = v[o], j(p, f, g, i, e[o]), t && (p.splice(0, q), p.splice(p.length - q, q));
                    } return v;
            },
            m = function m(a, b, c) {
                b = b || "soft"; var d,
                    e,
                    f,
                    h,
                    i,
                    j,
                    k,
                    l,
                    m,
                    n,
                    o,
                    p = {},
                    q = "cubic" === b ? 3 : 2,
                    r = "soft" === b,
                    s = []; if (r && c && (a = [c].concat(a)), null == a || a.length < q + 1) throw "invalid Bezier data"; for (m in a[0]) {
                        s.push(m);
                    } for (j = s.length; --j > -1;) {
                        for (m = s[j], p[m] = i = [], n = 0, l = a.length, k = 0; l > k; k++) {
                            d = null == c ? a[k][m] : "string" == typeof (o = a[k][m]) && "=" === o.charAt(1) ? c[m] + Number(o.charAt(0) + o.substr(2)) : Number(o), r && k > 1 && l - 1 > k && (i[n++] = (d + i[n - 2]) / 2), i[n++] = d;
                        } for (l = n - q + 1, n = 0, k = 0; l > k; k += q) {
                            d = i[k], e = i[k + 1], f = i[k + 2], h = 2 === q ? 0 : i[k + 3], i[n++] = o = 3 === q ? new g(d, e, f, h) : new g(d, (2 * e + d) / 3, (2 * e + f) / 3, f);
                        } i.length = n;
                    } return p;
            },
            n = function n(a, b, c) {
                for (var d, e, f, g, h, i, j, k, l, m, n, o = 1 / c, p = a.length; --p > -1;) {
                    for (m = a[p], f = m.a, g = m.d - f, h = m.c - f, i = m.b - f, d = e = 0, k = 1; c >= k; k++) {
                        j = o * k, l = 1 - j, d = e - (e = (j * j * g + 3 * l * (j * h + l * i)) * j), n = p * c + k - 1, b[n] = (b[n] || 0) + d * d;
                    }
                }
            },
            o = function o(a, b) {
                b = b >> 0 || 6; var c,
                    d,
                    e,
                    f,
                    g = [],
                    h = [],
                    i = 0,
                    j = 0,
                    k = b - 1,
                    l = [],
                    m = []; for (c in a) {
                        n(a[c], g, b);
                    } for (e = g.length, d = 0; e > d; d++) {
                        i += Math.sqrt(g[d]), f = d % b, m[f] = i, f === k && (j += i, f = d / b >> 0, l[f] = m, h[f] = j, i = 0, m = []);
                    } return { length: j, lengths: h, segments: l };
            },
            p = _gsScope._gsDefine.plugin({
                propName: "bezier", priority: -1, version: "1.3.7", API: 2, global: !0, init: function init(a, b, c) {
                    this._target = a, b instanceof Array && (b = { values: b }), this._func = {}, this._mod = {}, this._props = [], this._timeRes = null == b.timeResolution ? 6 : parseInt(b.timeResolution, 10); var d,
                        e,
                        f,
                        g,
                        h,
                        i = b.values || [],
                        j = {},
                        k = i[0],
                        n = b.autoRotate || c.vars.orientToBezier; this._autoRotate = n ? n instanceof Array ? n : [["x", "y", "rotation", n === !0 ? 0 : Number(n) || 0]] : null; for (d in k) {
                            this._props.push(d);
                        } for (f = this._props.length; --f > -1;) {
                            d = this._props[f], this._overwriteProps.push(d), e = this._func[d] = "function" == typeof a[d], j[d] = e ? a[d.indexOf("set") || "function" != typeof a["get" + d.substr(3)] ? d : "get" + d.substr(3)]() : parseFloat(a[d]), h || j[d] !== i[0][d] && (h = j);
                        } if (this._beziers = "cubic" !== b.type && "quadratic" !== b.type && "soft" !== b.type ? l(i, isNaN(b.curviness) ? 1 : b.curviness, !1, "thruBasic" === b.type, b.correlate, h) : m(i, b.type, j), this._segCount = this._beziers[d].length, this._timeRes) {
                            var p = o(this._beziers, this._timeRes); this._length = p.length, this._lengths = p.lengths, this._segments = p.segments, this._l1 = this._li = this._s1 = this._si = 0, this._l2 = this._lengths[0], this._curSeg = this._segments[0], this._s2 = this._curSeg[0], this._prec = 1 / this._curSeg.length;
                        } if (n = this._autoRotate) for (this._initialRotations = [], n[0] instanceof Array || (this._autoRotate = n = [n]), f = n.length; --f > -1;) {
                            for (g = 0; 3 > g; g++) {
                                d = n[f][g], this._func[d] = "function" == typeof a[d] ? a[d.indexOf("set") || "function" != typeof a["get" + d.substr(3)] ? d : "get" + d.substr(3)] : !1;
                            } d = n[f][2], this._initialRotations[f] = (this._func[d] ? this._func[d].call(this._target) : this._target[d]) || 0, this._overwriteProps.push(d);
                        } return this._startRatio = c.vars.runBackwards ? 1 : 0, !0;
                }, set: function set(b) {
                    var c,
                        d,
                        e,
                        f,
                        g,
                        h,
                        i,
                        j,
                        k,
                        l,
                        m = this._segCount,
                        n = this._func,
                        o = this._target,
                        p = b !== this._startRatio; if (this._timeRes) {
                            if (k = this._lengths, l = this._curSeg, b *= this._length, e = this._li, b > this._l2 && m - 1 > e) {
                                for (j = m - 1; j > e && (this._l2 = k[++e]) <= b;) { } this._l1 = k[e - 1], this._li = e, this._curSeg = l = this._segments[e], this._s2 = l[this._s1 = this._si = 0];
                            } else if (b < this._l1 && e > 0) {
                                for (; e > 0 && (this._l1 = k[--e]) >= b;) { } 0 === e && b < this._l1 ? this._l1 = 0 : e++, this._l2 = k[e], this._li = e, this._curSeg = l = this._segments[e], this._s1 = l[(this._si = l.length - 1) - 1] || 0, this._s2 = l[this._si];
                            } if (c = e, b -= this._l1, e = this._si, b > this._s2 && e < l.length - 1) {
                                for (j = l.length - 1; j > e && (this._s2 = l[++e]) <= b;) { } this._s1 = l[e - 1], this._si = e;
                            } else if (b < this._s1 && e > 0) {
                                for (; e > 0 && (this._s1 = l[--e]) >= b;) { } 0 === e && b < this._s1 ? this._s1 = 0 : e++, this._s2 = l[e], this._si = e;
                            } h = (e + (b - this._s1) / (this._s2 - this._s1)) * this._prec || 0;
                        } else c = 0 > b ? 0 : b >= 1 ? m - 1 : m * b >> 0, h = (b - c * (1 / m)) * m; for (d = 1 - h, e = this._props.length; --e > -1;) {
                            f = this._props[e], g = this._beziers[f][c], i = (h * h * g.da + 3 * d * (h * g.ca + d * g.ba)) * h + g.a, this._mod[f] && (i = this._mod[f](i, o)), n[f] ? o[f](i) : o[f] = i;
                        } if (this._autoRotate) {
                            var q,
                                r,
                                s,
                                t,
                                u,
                                v,
                                w,
                                x = this._autoRotate; for (e = x.length; --e > -1;) {
                                    f = x[e][2], v = x[e][3] || 0, w = x[e][4] === !0 ? 1 : a, g = this._beziers[x[e][0]], q = this._beziers[x[e][1]], g && q && (g = g[c], q = q[c], r = g.a + (g.b - g.a) * h, t = g.b + (g.c - g.b) * h, r += (t - r) * h, t += (g.c + (g.d - g.c) * h - t) * h, s = q.a + (q.b - q.a) * h, u = q.b + (q.c - q.b) * h, s += (u - s) * h, u += (q.c + (q.d - q.c) * h - u) * h, i = p ? Math.atan2(u - s, t - r) * w + v : this._initialRotations[e], this._mod[f] && (i = this._mod[f](i, o)), n[f] ? o[f](i) : o[f] = i);
                                }
                        }
                }
            }),
            q = p.prototype; p.bezierThrough = l, p.cubicToQuadratic = i, p._autoCSS = !0, p.quadraticToCubic = function (a, b, c) {
                return new g(a, (2 * b + a) / 3, (2 * b + c) / 3, c);
            }, p._cssRegister = function () {
                var a = f.CSSPlugin; if (a) {
                    var b = a._internals,
                        c = b._parseToProxy,
                        d = b._setPluginRatio,
                        e = b.CSSPropTween; b._registerComplexSpecialProp("bezier", {
                            parser: function parser(a, b, f, g, h, i) {
                                b instanceof Array && (b = { values: b }), i = new p(); var j,
                                    k,
                                    l,
                                    m = b.values,
                                    n = m.length - 1,
                                    o = [],
                                    q = {}; if (0 > n) return h; for (j = 0; n >= j; j++) {
                                        l = c(a, m[j], g, h, i, n !== j), o[j] = l.end;
                                    } for (k in b) {
                                        q[k] = b[k];
                                    } return q.values = o, h = new e(a, "bezier", 0, 0, l.pt, 2), h.data = l, h.plugin = i, h.setRatio = d, 0 === q.autoRotate && (q.autoRotate = !0), !q.autoRotate || q.autoRotate instanceof Array || (j = q.autoRotate === !0 ? 0 : Number(q.autoRotate), q.autoRotate = null != l.end.left ? [["left", "top", "rotation", j, !1]] : null != l.end.x ? [["x", "y", "rotation", j, !1]] : !1), q.autoRotate && (g._transform || g._enableTransforms(!1), l.autoRotate = g._target._gsTransform, l.proxy.rotation = l.autoRotate.rotation || 0, g._overwriteProps.push("rotation")), i._onInitTween(l.proxy, q, g._tween), h;
                            }
                        });
                }
            }, q._mod = function (a) {
                for (var b, c = this._overwriteProps, d = c.length; --d > -1;) {
                    b = a[c[d]], b && "function" == typeof b && (this._mod[c[d]] = b);
                }
            }, q._kill = function (a) {
                var b,
                    c,
                    d = this._props; for (b in this._beziers) {
                        if (b in a) for (delete this._beziers[b], delete this._func[b], c = d.length; --c > -1;) {
                            d[c] === b && d.splice(c, 1);
                        }
                    } if (d = this._autoRotate) for (c = d.length; --c > -1;) {
                        a[d[c][2]] && d.splice(c, 1);
                    } return this._super._kill.call(this, a);
            };
    }(), _gsScope._gsDefine("plugins.CSSPlugin", ["plugins.TweenPlugin", "TweenLite"], function (a, b) {
        var c,
            d,
            e,
            f,
            g = function g() {
                a.call(this, "css"), this._overwriteProps.length = 0, this.setRatio = g.prototype.setRatio;
            },
            h = _gsScope._gsDefine.globals,
            i = {},
            j = g.prototype = new a("css"); j.constructor = g, g.version = "1.19.1", g.API = 2, g.defaultTransformPerspective = 0, g.defaultSkewType = "compensated", g.defaultSmoothOrigin = !0, j = "px", g.suffixMap = { top: j, right: j, bottom: j, left: j, width: j, height: j, fontSize: j, padding: j, margin: j, perspective: j, lineHeight: "" }; var k,
            l,
            m,
            n,
            o,
            p,
            q,
            r,
            s = /(?:\-|\.|\b)(\d|\.|e\-)+/g,
            t = /(?:\d|\-\d|\.\d|\-\.\d|\+=\d|\-=\d|\+=.\d|\-=\.\d)+/g,
            u = /(?:\+=|\-=|\-|\b)[\d\-\.]+[a-zA-Z0-9]*(?:%|\b)/gi,
            v = /(?![+-]?\d*\.?\d+|[+-]|e[+-]\d+)[^0-9]/g,
            w = /(?:\d|\-|\+|=|#|\.)*/g,
            x = /opacity *= *([^)]*)/i,
            y = /opacity:([^;]*)/i,
            z = /alpha\(opacity *=.+?\)/i,
            A = /^(rgb|hsl)/,
            B = /([A-Z])/g,
            C = /-([a-z])/gi,
            D = /(^(?:url\(\"|url\())|(?:(\"\))$|\)$)/gi,
            E = function E(a, b) {
                return b.toUpperCase();
            },
            F = /(?:Left|Right|Width)/i,
            G = /(M11|M12|M21|M22)=[\d\-\.e]+/gi,
            H = /progid\:DXImageTransform\.Microsoft\.Matrix\(.+?\)/i,
            I = /,(?=[^\)]*(?:\(|$))/gi,
            J = /[\s,\(]/i,
            K = Math.PI / 180,
            L = 180 / Math.PI,
            M = {},
            N = { style: {} },
            O = _gsScope.document || {
                createElement: function createElement() {
                    return N;
                }
            },
            P = function P(a, b) {
                return O.createElementNS ? O.createElementNS(b || "http://www.w3.org/1999/xhtml", a) : O.createElement(a);
            },
            Q = P("div"),
            R = P("img"),
            S = g._internals = { _specialProps: i },
            T = (_gsScope.navigator || {}).userAgent || "",
            U = function () {
                var a = T.indexOf("Android"),
                    b = P("a"); return m = -1 !== T.indexOf("Safari") && -1 === T.indexOf("Chrome") && (-1 === a || parseFloat(T.substr(a + 8, 2)) > 3), o = m && parseFloat(T.substr(T.indexOf("Version/") + 8, 2)) < 6, n = -1 !== T.indexOf("Firefox"), (/MSIE ([0-9]{1,}[\.0-9]{0,})/.exec(T) || /Trident\/.*rv:([0-9]{1,}[\.0-9]{0,})/.exec(T)) && (p = parseFloat(RegExp.$1)), b ? (b.style.cssText = "top:1px;opacity:.55;", /^0.55/.test(b.style.opacity)) : !1;
            }(),
            V = function V(a) {
                return x.test("string" == typeof a ? a : (a.currentStyle ? a.currentStyle.filter : a.style.filter) || "") ? parseFloat(RegExp.$1) / 100 : 1;
            },
            W = function W(a) {
                _gsScope.console && console.log(a);
            },
            X = "",
            Y = "",
            Z = function Z(a, b) {
                b = b || Q; var c,
                    d,
                    e = b.style; if (void 0 !== e[a]) return a; for (a = a.charAt(0).toUpperCase() + a.substr(1), c = ["O", "Moz", "ms", "Ms", "Webkit"], d = 5; --d > -1 && void 0 === e[c[d] + a];) { } return d >= 0 ? (Y = 3 === d ? "ms" : c[d], X = "-" + Y.toLowerCase() + "-", Y + a) : null;
            },
            $ = O.defaultView ? O.defaultView.getComputedStyle : function () { },
            _ = g.getStyle = function (a, b, c, d, e) {
                var f; return U || "opacity" !== b ? (!d && a.style[b] ? f = a.style[b] : (c = c || $(a)) ? f = c[b] || c.getPropertyValue(b) || c.getPropertyValue(b.replace(B, "-$1").toLowerCase()) : a.currentStyle && (f = a.currentStyle[b]), null == e || f && "none" !== f && "auto" !== f && "auto auto" !== f ? f : e) : V(a);
            },
            aa = S.convertToPixels = function (a, c, d, e, f) {
                if ("px" === e || !e) return d; if ("auto" === e || !d) return 0; var h,
                    i,
                    j,
                    k = F.test(c),
                    l = a,
                    m = Q.style,
                    n = 0 > d,
                    o = 1 === d; if (n && (d = -d), o && (d *= 100), "%" === e && -1 !== c.indexOf("border")) h = d / 100 * (k ? a.clientWidth : a.clientHeight); else {
                        if (m.cssText = "border:0 solid red;position:" + _(a, "position") + ";line-height:0;", "%" !== e && l.appendChild && "v" !== e.charAt(0) && "rem" !== e) m[k ? "borderLeftWidth" : "borderTopWidth"] = d + e; else {
                            if (l = a.parentNode || O.body, i = l._gsCache, j = b.ticker.frame, i && k && i.time === j) return i.width * d / 100; m[k ? "width" : "height"] = d + e;
                        } l.appendChild(Q), h = parseFloat(Q[k ? "offsetWidth" : "offsetHeight"]), l.removeChild(Q), k && "%" === e && g.cacheWidths !== !1 && (i = l._gsCache = l._gsCache || {}, i.time = j, i.width = h / d * 100), 0 !== h || f || (h = aa(a, c, d, e, !0));
                    } return o && (h /= 100), n ? -h : h;
            },
            ba = S.calculateOffset = function (a, b, c) {
                if ("absolute" !== _(a, "position", c)) return 0; var d = "left" === b ? "Left" : "Top",
                    e = _(a, "margin" + d, c); return a["offset" + d] - (aa(a, b, parseFloat(e), e.replace(w, "")) || 0);
            },
            ca = function ca(a, b) {
                var c,
                    d,
                    e,
                    f = {}; if (b = b || $(a, null)) {
                        if (c = b.length) for (; --c > -1;) {
                            e = b[c], (-1 === e.indexOf("-transform") || Da === e) && (f[e.replace(C, E)] = b.getPropertyValue(e));
                        } else for (c in b) {
                            (-1 === c.indexOf("Transform") || Ca === c) && (f[c] = b[c]);
                        }
                    } else if (b = a.currentStyle || a.style) for (c in b) {
                        "string" == typeof c && void 0 === f[c] && (f[c.replace(C, E)] = b[c]);
                    } return U || (f.opacity = V(a)), d = Ra(a, b, !1), f.rotation = d.rotation, f.skewX = d.skewX, f.scaleX = d.scaleX, f.scaleY = d.scaleY, f.x = d.x, f.y = d.y, Fa && (f.z = d.z, f.rotationX = d.rotationX, f.rotationY = d.rotationY, f.scaleZ = d.scaleZ), f.filters && delete f.filters, f;
            },
            da = function da(a, b, c, d, e) {
                var f,
                    g,
                    h,
                    i = {},
                    j = a.style; for (g in c) {
                        "cssText" !== g && "length" !== g && isNaN(g) && (b[g] !== (f = c[g]) || e && e[g]) && -1 === g.indexOf("Origin") && ("number" == typeof f || "string" == typeof f) && (i[g] = "auto" !== f || "left" !== g && "top" !== g ? "" !== f && "auto" !== f && "none" !== f || "string" != typeof b[g] || "" === b[g].replace(v, "") ? f : 0 : ba(a, g), void 0 !== j[g] && (h = new sa(j, g, j[g], h)));
                    } if (d) for (g in d) {
                        "className" !== g && (i[g] = d[g]);
                    } return { difs: i, firstMPT: h };
            },
            ea = { width: ["Left", "Right"], height: ["Top", "Bottom"] },
            fa = ["marginLeft", "marginRight", "marginTop", "marginBottom"],
            ga = function ga(a, b, c) {
                if ("svg" === (a.nodeName + "").toLowerCase()) return (c || $(a))[b] || 0; if (a.getCTM && Oa(a)) return a.getBBox()[b] || 0; var d = parseFloat("width" === b ? a.offsetWidth : a.offsetHeight),
                    e = ea[b],
                    f = e.length; for (c = c || $(a, null) ; --f > -1;) {
                        d -= parseFloat(_(a, "padding" + e[f], c, !0)) || 0, d -= parseFloat(_(a, "border" + e[f] + "Width", c, !0)) || 0;
                    } return d;
            },
            ha = function ha(a, b) {
                if ("contain" === a || "auto" === a || "auto auto" === a) return a + " "; (null == a || "" === a) && (a = "0 0"); var c,
                    d = a.split(" "),
                    e = -1 !== a.indexOf("left") ? "0%" : -1 !== a.indexOf("right") ? "100%" : d[0],
                    f = -1 !== a.indexOf("top") ? "0%" : -1 !== a.indexOf("bottom") ? "100%" : d[1]; if (d.length > 3 && !b) {
                        for (d = a.split(", ").join(",").split(","), a = [], c = 0; c < d.length; c++) {
                            a.push(ha(d[c]));
                        } return a.join(",");
                    } return null == f ? f = "center" === e ? "50%" : "0" : "center" === f && (f = "50%"), ("center" === e || isNaN(parseFloat(e)) && -1 === (e + "").indexOf("=")) && (e = "50%"), a = e + " " + f + (d.length > 2 ? " " + d[2] : ""), b && (b.oxp = -1 !== e.indexOf("%"), b.oyp = -1 !== f.indexOf("%"), b.oxr = "=" === e.charAt(1), b.oyr = "=" === f.charAt(1), b.ox = parseFloat(e.replace(v, "")), b.oy = parseFloat(f.replace(v, "")), b.v = a), b || a;
            },
            ia = function ia(a, b) {
                return "function" == typeof a && (a = a(r, q)), "string" == typeof a && "=" === a.charAt(1) ? parseInt(a.charAt(0) + "1", 10) * parseFloat(a.substr(2)) : parseFloat(a) - parseFloat(b) || 0;
            },
            ja = function ja(a, b) {
                return "function" == typeof a && (a = a(r, q)), null == a ? b : "string" == typeof a && "=" === a.charAt(1) ? parseInt(a.charAt(0) + "1", 10) * parseFloat(a.substr(2)) + b : parseFloat(a) || 0;
            },
            ka = function ka(a, b, c, d) {
                var e,
                    f,
                    g,
                    h,
                    i,
                    j = 1e-6; return "function" == typeof a && (a = a(r, q)), null == a ? h = b : "number" == typeof a ? h = a : (e = 360, f = a.split("_"), i = "=" === a.charAt(1), g = (i ? parseInt(a.charAt(0) + "1", 10) * parseFloat(f[0].substr(2)) : parseFloat(f[0])) * (-1 === a.indexOf("rad") ? 1 : L) - (i ? 0 : b), f.length && (d && (d[c] = b + g), -1 !== a.indexOf("short") && (g %= e, g !== g % (e / 2) && (g = 0 > g ? g + e : g - e)), -1 !== a.indexOf("_cw") && 0 > g ? g = (g + 9999999999 * e) % e - (g / e | 0) * e : -1 !== a.indexOf("ccw") && g > 0 && (g = (g - 9999999999 * e) % e - (g / e | 0) * e)), h = b + g), j > h && h > -j && (h = 0), h;
            },
            la = { aqua: [0, 255, 255], lime: [0, 255, 0], silver: [192, 192, 192], black: [0, 0, 0], maroon: [128, 0, 0], teal: [0, 128, 128], blue: [0, 0, 255], navy: [0, 0, 128], white: [255, 255, 255], fuchsia: [255, 0, 255], olive: [128, 128, 0], yellow: [255, 255, 0], orange: [255, 165, 0], gray: [128, 128, 128], purple: [128, 0, 128], green: [0, 128, 0], red: [255, 0, 0], pink: [255, 192, 203], cyan: [0, 255, 255], transparent: [255, 255, 255, 0] },
            ma = function ma(a, b, c) {
                return a = 0 > a ? a + 1 : a > 1 ? a - 1 : a, 255 * (1 > 6 * a ? b + (c - b) * a * 6 : .5 > a ? c : 2 > 3 * a ? b + (c - b) * (2 / 3 - a) * 6 : b) + .5 | 0;
            },
            na = g.parseColor = function (a, b) {
                var c, d, e, f, g, h, i, j, k, l, m; if (a) {
                    if ("number" == typeof a) c = [a >> 16, a >> 8 & 255, 255 & a]; else {
                        if ("," === a.charAt(a.length - 1) && (a = a.substr(0, a.length - 1)), la[a]) c = la[a]; else if ("#" === a.charAt(0)) 4 === a.length && (d = a.charAt(1), e = a.charAt(2), f = a.charAt(3), a = "#" + d + d + e + e + f + f), a = parseInt(a.substr(1), 16), c = [a >> 16, a >> 8 & 255, 255 & a]; else if ("hsl" === a.substr(0, 3)) {
                            if (c = m = a.match(s), b) {
                                if (-1 !== a.indexOf("=")) return a.match(t);
                            } else g = Number(c[0]) % 360 / 360, h = Number(c[1]) / 100, i = Number(c[2]) / 100, e = .5 >= i ? i * (h + 1) : i + h - i * h, d = 2 * i - e, c.length > 3 && (c[3] = Number(a[3])), c[0] = ma(g + 1 / 3, d, e), c[1] = ma(g, d, e), c[2] = ma(g - 1 / 3, d, e);
                        } else c = a.match(s) || la.transparent; c[0] = Number(c[0]), c[1] = Number(c[1]), c[2] = Number(c[2]), c.length > 3 && (c[3] = Number(c[3]));
                    }
                } else c = la.black; return b && !m && (d = c[0] / 255, e = c[1] / 255, f = c[2] / 255, j = Math.max(d, e, f), k = Math.min(d, e, f), i = (j + k) / 2, j === k ? g = h = 0 : (l = j - k, h = i > .5 ? l / (2 - j - k) : l / (j + k), g = j === d ? (e - f) / l + (f > e ? 6 : 0) : j === e ? (f - d) / l + 2 : (d - e) / l + 4, g *= 60), c[0] = g + .5 | 0, c[1] = 100 * h + .5 | 0, c[2] = 100 * i + .5 | 0), c;
            },
            oa = function oa(a, b) {
                var c,
                    d,
                    e,
                    f = a.match(pa) || [],
                    g = 0,
                    h = f.length ? "" : a; for (c = 0; c < f.length; c++) {
                        d = f[c], e = a.substr(g, a.indexOf(d, g) - g), g += e.length + d.length, d = na(d, b), 3 === d.length && d.push(1), h += e + (b ? "hsla(" + d[0] + "," + d[1] + "%," + d[2] + "%," + d[3] : "rgba(" + d.join(",")) + ")";
                    } return h + a.substr(g);
            },
            pa = "(?:\\b(?:(?:rgb|rgba|hsl|hsla)\\(.+?\\))|\\B#(?:[0-9a-f]{3}){1,2}\\b"; for (j in la) {
                pa += "|" + j + "\\b";
            } pa = new RegExp(pa + ")", "gi"), g.colorStringFilter = function (a) {
                var b,
                    c = a[0] + a[1]; pa.test(c) && (b = -1 !== c.indexOf("hsl(") || -1 !== c.indexOf("hsla("), a[0] = oa(a[0], b), a[1] = oa(a[1], b)), pa.lastIndex = 0;
            }, b.defaultStringFilter || (b.defaultStringFilter = g.colorStringFilter); var qa = function qa(a, b, c, d) {
                if (null == a) return function (a) {
                    return a;
                }; var e,
                    f = b ? (a.match(pa) || [""])[0] : "",
                    g = a.split(f).join("").match(u) || [],
                    h = a.substr(0, a.indexOf(g[0])),
                    i = ")" === a.charAt(a.length - 1) ? ")" : "",
                    j = -1 !== a.indexOf(" ") ? " " : ",",
                    k = g.length,
                    l = k > 0 ? g[0].replace(s, "") : ""; return k ? e = b ? function (a) {
                        var b, m, n, o; if ("number" == typeof a) a += l; else if (d && I.test(a)) {
                            for (o = a.replace(I, "|").split("|"), n = 0; n < o.length; n++) {
                                o[n] = e(o[n]);
                            } return o.join(",");
                        } if (b = (a.match(pa) || [f])[0], m = a.split(b).join("").match(u) || [], n = m.length, k > n--) for (; ++n < k;) {
                            m[n] = c ? m[(n - 1) / 2 | 0] : g[n];
                        } return h + m.join(j) + j + b + i + (-1 !== a.indexOf("inset") ? " inset" : "");
                    } : function (a) {
                        var b, f, m; if ("number" == typeof a) a += l; else if (d && I.test(a)) {
                            for (f = a.replace(I, "|").split("|"), m = 0; m < f.length; m++) {
                                f[m] = e(f[m]);
                            } return f.join(",");
                        } if (b = a.match(u) || [], m = b.length, k > m--) for (; ++m < k;) {
                            b[m] = c ? b[(m - 1) / 2 | 0] : g[m];
                        } return h + b.join(j) + i;
                    } : function (a) {
                        return a;
                    };
            },
                ra = function ra(a) {
                    return a = a.split(","), function (b, c, d, e, f, g, h) {
                        var i,
                            j = (c + "").split(" "); for (h = {}, i = 0; 4 > i; i++) {
                                h[a[i]] = j[i] = j[i] || j[(i - 1) / 2 >> 0];
                            } return e.parse(b, h, f, g);
                    };
                },
                sa = (S._setPluginRatio = function (a) {
                    this.plugin.setRatio(a); for (var b, c, d, e, f, g = this.data, h = g.proxy, i = g.firstMPT, j = 1e-6; i;) {
                        b = h[i.v], i.r ? b = Math.round(b) : j > b && b > -j && (b = 0), i.t[i.p] = b, i = i._next;
                    } if (g.autoRotate && (g.autoRotate.rotation = g.mod ? g.mod(h.rotation, this.t) : h.rotation), 1 === a || 0 === a) for (i = g.firstMPT, f = 1 === a ? "e" : "b"; i;) {
                        if (c = i.t, c.type) {
                            if (1 === c.type) {
                                for (e = c.xs0 + c.s + c.xs1, d = 1; d < c.l; d++) {
                                    e += c["xn" + d] + c["xs" + (d + 1)];
                                } c[f] = e;
                            }
                        } else c[f] = c.s + c.xs0; i = i._next;
                    }
                }, function (a, b, c, d, e) {
                    this.t = a, this.p = b, this.v = c, this.r = e, d && (d._prev = this, this._next = d);
                }),
                ta = (S._parseToProxy = function (a, b, c, d, e, f) {
                    var g,
                        h,
                        i,
                        j,
                        k,
                        l = d,
                        m = {},
                        n = {},
                        o = c._transform,
                        p = M; for (c._transform = null, M = b, d = k = c.parse(a, b, d, e), M = p, f && (c._transform = o, l && (l._prev = null, l._prev && (l._prev._next = null))) ; d && d !== l;) {
                            if (d.type <= 1 && (h = d.p, n[h] = d.s + d.c, m[h] = d.s, f || (j = new sa(d, "s", h, j, d.r), d.c = 0), 1 === d.type)) for (g = d.l; --g > 0;) {
                                i = "xn" + g, h = d.p + "_" + i, n[h] = d.data[i], m[h] = d[i], f || (j = new sa(d, i, h, j, d.rxp[i]));
                            } d = d._next;
                        } return { proxy: m, end: n, firstMPT: j, pt: k };
                }, S.CSSPropTween = function (a, b, d, e, g, h, i, j, k, l, m) {
                    this.t = a, this.p = b, this.s = d, this.c = e, this.n = i || b, a instanceof ta || f.push(this.n), this.r = j, this.type = h || 0, k && (this.pr = k, c = !0), this.b = void 0 === l ? d : l, this.e = void 0 === m ? d + e : m, g && (this._next = g, g._prev = this);
                }),
                ua = function ua(a, b, c, d, e, f) {
                    var g = new ta(a, b, c, d - c, e, -1, f); return g.b = c, g.e = g.xs0 = d, g;
                },
                va = g.parseComplex = function (a, b, c, d, e, f, h, i, j, l) {
                    c = c || f || "", "function" == typeof d && (d = d(r, q)), h = new ta(a, b, 0, 0, h, l ? 2 : 1, null, !1, i, c, d), d += "", e && pa.test(d + c) && (d = [c, d], g.colorStringFilter(d), c = d[0], d = d[1]); var m,
                        n,
                        o,
                        p,
                        u,
                        v,
                        w,
                        x,
                        y,
                        z,
                        A,
                        B,
                        C,
                        D = c.split(", ").join(",").split(" "),
                        E = d.split(", ").join(",").split(" "),
                        F = D.length,
                        G = k !== !1; for ((-1 !== d.indexOf(",") || -1 !== c.indexOf(",")) && (D = D.join(" ").replace(I, ", ").split(" "), E = E.join(" ").replace(I, ", ").split(" "), F = D.length), F !== E.length && (D = (f || "").split(" "), F = D.length), h.plugin = j, h.setRatio = l, pa.lastIndex = 0, m = 0; F > m; m++) {
                            if (p = D[m], u = E[m], x = parseFloat(p), x || 0 === x) h.appendXtra("", x, ia(u, x), u.replace(t, ""), G && -1 !== u.indexOf("px"), !0); else if (e && pa.test(p)) B = u.indexOf(")") + 1, B = ")" + (B ? u.substr(B) : ""), C = -1 !== u.indexOf("hsl") && U, p = na(p, C), u = na(u, C), y = p.length + u.length > 6, y && !U && 0 === u[3] ? (h["xs" + h.l] += h.l ? " transparent" : "transparent", h.e = h.e.split(E[m]).join("transparent")) : (U || (y = !1), C ? h.appendXtra(y ? "hsla(" : "hsl(", p[0], ia(u[0], p[0]), ",", !1, !0).appendXtra("", p[1], ia(u[1], p[1]), "%,", !1).appendXtra("", p[2], ia(u[2], p[2]), y ? "%," : "%" + B, !1) : h.appendXtra(y ? "rgba(" : "rgb(", p[0], u[0] - p[0], ",", !0, !0).appendXtra("", p[1], u[1] - p[1], ",", !0).appendXtra("", p[2], u[2] - p[2], y ? "," : B, !0), y && (p = p.length < 4 ? 1 : p[3], h.appendXtra("", p, (u.length < 4 ? 1 : u[3]) - p, B, !1))), pa.lastIndex = 0; else if (v = p.match(s)) {
                                if (w = u.match(t), !w || w.length !== v.length) return h; for (o = 0, n = 0; n < v.length; n++) {
                                    A = v[n], z = p.indexOf(A, o), h.appendXtra(p.substr(o, z - o), Number(A), ia(w[n], A), "", G && "px" === p.substr(z + A.length, 2), 0 === n), o = z + A.length;
                                } h["xs" + h.l] += p.substr(o);
                            } else h["xs" + h.l] += h.l || h["xs" + h.l] ? " " + u : u;
                        } if (-1 !== d.indexOf("=") && h.data) {
                            for (B = h.xs0 + h.data.s, m = 1; m < h.l; m++) {
                                B += h["xs" + m] + h.data["xn" + m];
                            } h.e = B + h["xs" + m];
                        } return h.l || (h.type = -1, h.xs0 = h.e), h.xfirst || h;
                },
                wa = 9; for (j = ta.prototype, j.l = j.pr = 0; --wa > 0;) {
                    j["xn" + wa] = 0, j["xs" + wa] = "";
                } j.xs0 = "", j._next = j._prev = j.xfirst = j.data = j.plugin = j.setRatio = j.rxp = null, j.appendXtra = function (a, b, c, d, e, f) {
                    var g = this,
                        h = g.l; return g["xs" + h] += f && (h || g["xs" + h]) ? " " + a : a || "", c || 0 === h || g.plugin ? (g.l++, g.type = g.setRatio ? 2 : 1, g["xs" + g.l] = d || "", h > 0 ? (g.data["xn" + h] = b + c, g.rxp["xn" + h] = e, g["xn" + h] = b, g.plugin || (g.xfirst = new ta(g, "xn" + h, b, c, g.xfirst || g, 0, g.n, e, g.pr), g.xfirst.xs0 = 0), g) : (g.data = { s: b + c }, g.rxp = {}, g.s = b, g.c = c, g.r = e, g)) : (g["xs" + h] += b + (d || ""), g);
                }; var xa = function xa(a, b) {
                    b = b || {}, this.p = b.prefix ? Z(a) || a : a, i[a] = i[this.p] = this, this.format = b.formatter || qa(b.defaultValue, b.color, b.collapsible, b.multi), b.parser && (this.parse = b.parser), this.clrs = b.color, this.multi = b.multi, this.keyword = b.keyword, this.dflt = b.defaultValue, this.pr = b.priority || 0;
                },
                    ya = S._registerComplexSpecialProp = function (a, b, c) {
                        "object" != (typeof b === "undefined" ? "undefined" : _typeof(b)) && (b = { parser: c }); var d,
                            e,
                            f = a.split(","),
                            g = b.defaultValue; for (c = c || [g], d = 0; d < f.length; d++) {
                                b.prefix = 0 === d && b.prefix, b.defaultValue = c[d] || g, e = new xa(f[d], b);
                            }
                    },
                    za = S._registerPluginProp = function (a) {
                        if (!i[a]) {
                            var b = a.charAt(0).toUpperCase() + a.substr(1) + "Plugin"; ya(a, {
                                parser: function parser(a, c, d, e, f, g, j) {
                                    var k = h.com.greensock.plugins[b]; return k ? (k._cssRegister(), i[d].parse(a, c, d, e, f, g, j)) : (W("Error: " + b + " js file not loaded."), f);
                                }
                            });
                        }
                    }; j = xa.prototype, j.parseComplex = function (a, b, c, d, e, f) {
                        var g,
                            h,
                            i,
                            j,
                            k,
                            l,
                            m = this.keyword; if (this.multi && (I.test(c) || I.test(b) ? (h = b.replace(I, "|").split("|"), i = c.replace(I, "|").split("|")) : m && (h = [b], i = [c])), i) {
                                for (j = i.length > h.length ? i.length : h.length, g = 0; j > g; g++) {
                                    b = h[g] = h[g] || this.dflt, c = i[g] = i[g] || this.dflt, m && (k = b.indexOf(m), l = c.indexOf(m), k !== l && (-1 === l ? h[g] = h[g].split(m).join("") : -1 === k && (h[g] += " " + m)));
                                } b = h.join(", "), c = i.join(", ");
                            } return va(a, this.p, b, c, this.clrs, this.dflt, d, this.pr, e, f);
                    }, j.parse = function (a, b, c, d, f, g, h) {
                        return this.parseComplex(a.style, this.format(_(a, this.p, e, !1, this.dflt)), this.format(b), f, g);
                    }, g.registerSpecialProp = function (a, b, c) {
                        ya(a, {
                            parser: function parser(a, d, e, f, g, h, i) {
                                var j = new ta(a, e, 0, 0, g, 2, e, !1, c); return j.plugin = h, j.setRatio = b(a, d, f._tween, e), j;
                            }, priority: c
                        });
                    }, g.useSVGTransformAttr = !0; var Aa,
                        Ba = "scaleX,scaleY,scaleZ,x,y,z,skewX,skewY,rotation,rotationX,rotationY,perspective,xPercent,yPercent".split(","),
                        Ca = Z("transform"),
                        Da = X + "transform",
                        Ea = Z("transformOrigin"),
                        Fa = null !== Z("perspective"),
                        Ga = S.Transform = function () {
                            this.perspective = parseFloat(g.defaultTransformPerspective) || 0, this.force3D = g.defaultForce3D !== !1 && Fa ? g.defaultForce3D || "auto" : !1;
                        },
                        Ha = _gsScope.SVGElement,
                        Ia = function Ia(a, b, c) {
                            var d,
                                e = O.createElementNS("http://www.w3.org/2000/svg", a),
                                f = /([a-z])([A-Z])/g; for (d in c) {
                                    e.setAttributeNS(null, d.replace(f, "$1-$2").toLowerCase(), c[d]);
                                } return b.appendChild(e), e;
                        },
                        Ja = O.documentElement || {},
                        Ka = function () {
                            var a,
                                b,
                                c,
                                d = p || /Android/i.test(T) && !_gsScope.chrome; return O.createElementNS && !d && (a = Ia("svg", Ja), b = Ia("rect", a, { width: 100, height: 50, x: 100 }), c = b.getBoundingClientRect().width, b.style[Ea] = "50% 50%", b.style[Ca] = "scaleX(0.5)", d = c === b.getBoundingClientRect().width && !(n && Fa), Ja.removeChild(a)), d;
                        }(),
                        La = function La(a, b, c, d, e, f) {
                            var h,
                                i,
                                j,
                                k,
                                l,
                                m,
                                n,
                                o,
                                p,
                                q,
                                r,
                                s,
                                t,
                                u,
                                v = a._gsTransform,
                                w = Qa(a, !0); v && (t = v.xOrigin, u = v.yOrigin), (!d || (h = d.split(" ")).length < 2) && (n = a.getBBox(), 0 === n.x && 0 === n.y && n.width + n.height === 0 && (n = { x: parseFloat(a.hasAttribute("x") ? a.getAttribute("x") : a.hasAttribute("cx") ? a.getAttribute("cx") : 0) || 0, y: parseFloat(a.hasAttribute("y") ? a.getAttribute("y") : a.hasAttribute("cy") ? a.getAttribute("cy") : 0) || 0, width: 0, height: 0 }), b = ha(b).split(" "), h = [(-1 !== b[0].indexOf("%") ? parseFloat(b[0]) / 100 * n.width : parseFloat(b[0])) + n.x, (-1 !== b[1].indexOf("%") ? parseFloat(b[1]) / 100 * n.height : parseFloat(b[1])) + n.y]), c.xOrigin = k = parseFloat(h[0]), c.yOrigin = l = parseFloat(h[1]), d && w !== Pa && (m = w[0], n = w[1], o = w[2], p = w[3], q = w[4], r = w[5], s = m * p - n * o, s && (i = k * (p / s) + l * (-o / s) + (o * r - p * q) / s, j = k * (-n / s) + l * (m / s) - (m * r - n * q) / s, k = c.xOrigin = h[0] = i, l = c.yOrigin = h[1] = j)), v && (f && (c.xOffset = v.xOffset, c.yOffset = v.yOffset, v = c), e || e !== !1 && g.defaultSmoothOrigin !== !1 ? (i = k - t, j = l - u, v.xOffset += i * w[0] + j * w[2] - i, v.yOffset += i * w[1] + j * w[3] - j) : v.xOffset = v.yOffset = 0), f || a.setAttribute("data-svg-origin", h.join(" "));
                        },
                        Ma = function Ma(a) {
                            var b,
                                c = P("svg", this.ownerSVGElement.getAttribute("xmlns") || "http://www.w3.org/2000/svg"),
                                d = this.parentNode,
                                e = this.nextSibling,
                                f = this.style.cssText; if (Ja.appendChild(c), c.appendChild(this), this.style.display = "block", a) try {
                                    b = this.getBBox(), this._originalGetBBox = this.getBBox, this.getBBox = Ma;
                                } catch (g) { } else this._originalGetBBox && (b = this._originalGetBBox()); return e ? d.insertBefore(this, e) : d.appendChild(this), Ja.removeChild(c), this.style.cssText = f, b;
                        },
                        Na = function Na(a) {
                            try {
                                return a.getBBox();
                            } catch (b) {
                                return Ma.call(a, !0);
                            }
                        },
                        Oa = function Oa(a) {
                            return !(!(Ha && a.getCTM && Na(a)) || a.parentNode && !a.ownerSVGElement);
                        },
                        Pa = [1, 0, 0, 1, 0, 0],
                        Qa = function Qa(a, b) {
                            var c,
                                d,
                                e,
                                f,
                                g,
                                h,
                                i = a._gsTransform || new Ga(),
                                j = 1e5,
                                k = a.style; if (Ca ? d = _(a, Da, null, !0) : a.currentStyle && (d = a.currentStyle.filter.match(G), d = d && 4 === d.length ? [d[0].substr(4), Number(d[2].substr(4)), Number(d[1].substr(4)), d[3].substr(4), i.x || 0, i.y || 0].join(",") : ""), c = !d || "none" === d || "matrix(1, 0, 0, 1, 0, 0)" === d, c && Ca && ((h = "none" === $(a).display) || !a.parentNode) && (h && (f = k.display, k.display = "block"), a.parentNode || (g = 1, Ja.appendChild(a)), d = _(a, Da, null, !0), c = !d || "none" === d || "matrix(1, 0, 0, 1, 0, 0)" === d, f ? k.display = f : h && Va(k, "display"), g && Ja.removeChild(a)), (i.svg || a.getCTM && Oa(a)) && (c && -1 !== (k[Ca] + "").indexOf("matrix") && (d = k[Ca], c = 0), e = a.getAttribute("transform"), c && e && (-1 !== e.indexOf("matrix") ? (d = e, c = 0) : -1 !== e.indexOf("translate") && (d = "matrix(1,0,0,1," + e.match(/(?:\-|\b)[\d\-\.e]+\b/gi).join(",") + ")", c = 0))), c) return Pa; for (e = (d || "").match(s) || [], wa = e.length; --wa > -1;) {
                                    f = Number(e[wa]), e[wa] = (g = f - (f |= 0)) ? (g * j + (0 > g ? -.5 : .5) | 0) / j + f : f;
                                } return b && e.length > 6 ? [e[0], e[1], e[4], e[5], e[12], e[13]] : e;
                        },
                        Ra = S.getTransform = function (a, c, d, e) {
                            if (a._gsTransform && d && !e) return a._gsTransform; var f,
                                h,
                                i,
                                j,
                                k,
                                l,
                                m = d ? a._gsTransform || new Ga() : new Ga(),
                                n = m.scaleX < 0,
                                o = 2e-5,
                                p = 1e5,
                                q = Fa ? parseFloat(_(a, Ea, c, !1, "0 0 0").split(" ")[2]) || m.zOrigin || 0 : 0,
                                r = parseFloat(g.defaultTransformPerspective) || 0; if (m.svg = !(!a.getCTM || !Oa(a)), m.svg && (La(a, _(a, Ea, c, !1, "50% 50%") + "", m, a.getAttribute("data-svg-origin")), Aa = g.useSVGTransformAttr || Ka), f = Qa(a), f !== Pa) {
                                    if (16 === f.length) {
                                        var s,
                                            t,
                                            u,
                                            v,
                                            w,
                                            x = f[0],
                                            y = f[1],
                                            z = f[2],
                                            A = f[3],
                                            B = f[4],
                                            C = f[5],
                                            D = f[6],
                                            E = f[7],
                                            F = f[8],
                                            G = f[9],
                                            H = f[10],
                                            I = f[12],
                                            J = f[13],
                                            K = f[14],
                                            M = f[11],
                                            N = Math.atan2(D, H); m.zOrigin && (K = -m.zOrigin, I = F * K - f[12], J = G * K - f[13], K = H * K + m.zOrigin - f[14]), m.rotationX = N * L, N && (v = Math.cos(-N), w = Math.sin(-N), s = B * v + F * w, t = C * v + G * w, u = D * v + H * w, F = B * -w + F * v, G = C * -w + G * v, H = D * -w + H * v, M = E * -w + M * v, B = s, C = t, D = u), N = Math.atan2(-z, H), m.rotationY = N * L, N && (v = Math.cos(-N), w = Math.sin(-N), s = x * v - F * w, t = y * v - G * w, u = z * v - H * w, G = y * w + G * v, H = z * w + H * v, M = A * w + M * v, x = s, y = t, z = u), N = Math.atan2(y, x), m.rotation = N * L, N && (v = Math.cos(-N), w = Math.sin(-N), x = x * v + B * w, t = y * v + C * w, C = y * -w + C * v, D = z * -w + D * v, y = t), m.rotationX && Math.abs(m.rotationX) + Math.abs(m.rotation) > 359.9 && (m.rotationX = m.rotation = 0, m.rotationY = 180 - m.rotationY), m.scaleX = (Math.sqrt(x * x + y * y) * p + .5 | 0) / p, m.scaleY = (Math.sqrt(C * C + G * G) * p + .5 | 0) / p, m.scaleZ = (Math.sqrt(D * D + H * H) * p + .5 | 0) / p, m.rotationX || m.rotationY ? m.skewX = 0 : (m.skewX = B || C ? Math.atan2(B, C) * L + m.rotation : m.skewX || 0, Math.abs(m.skewX) > 90 && Math.abs(m.skewX) < 270 && (n ? (m.scaleX *= -1, m.skewX += m.rotation <= 0 ? 180 : -180, m.rotation += m.rotation <= 0 ? 180 : -180) : (m.scaleY *= -1, m.skewX += m.skewX <= 0 ? 180 : -180))), m.perspective = M ? 1 / (0 > M ? -M : M) : 0, m.x = I, m.y = J, m.z = K, m.svg && (m.x -= m.xOrigin - (m.xOrigin * x - m.yOrigin * B), m.y -= m.yOrigin - (m.yOrigin * y - m.xOrigin * C));
                                    } else if (!Fa || e || !f.length || m.x !== f[4] || m.y !== f[5] || !m.rotationX && !m.rotationY) {
                                        var O = f.length >= 6,
                                            P = O ? f[0] : 1,
                                            Q = f[1] || 0,
                                            R = f[2] || 0,
                                            S = O ? f[3] : 1; m.x = f[4] || 0, m.y = f[5] || 0, i = Math.sqrt(P * P + Q * Q), j = Math.sqrt(S * S + R * R), k = P || Q ? Math.atan2(Q, P) * L : m.rotation || 0, l = R || S ? Math.atan2(R, S) * L + k : m.skewX || 0, Math.abs(l) > 90 && Math.abs(l) < 270 && (n ? (i *= -1, l += 0 >= k ? 180 : -180, k += 0 >= k ? 180 : -180) : (j *= -1, l += 0 >= l ? 180 : -180)), m.scaleX = i, m.scaleY = j, m.rotation = k, m.skewX = l, Fa && (m.rotationX = m.rotationY = m.z = 0, m.perspective = r, m.scaleZ = 1), m.svg && (m.x -= m.xOrigin - (m.xOrigin * P + m.yOrigin * R), m.y -= m.yOrigin - (m.xOrigin * Q + m.yOrigin * S));
                                    } m.zOrigin = q; for (h in m) {
                                        m[h] < o && m[h] > -o && (m[h] = 0);
                                    }
                                } return d && (a._gsTransform = m, m.svg && (Aa && a.style[Ca] ? b.delayedCall(.001, function () {
                                    Va(a.style, Ca);
                                }) : !Aa && a.getAttribute("transform") && b.delayedCall(.001, function () {
                                    a.removeAttribute("transform");
                                }))), m;
                        },
                        Sa = function Sa(a) {
                            var b,
                                c,
                                d = this.data,
                                e = -d.rotation * K,
                                f = e + d.skewX * K,
                                g = 1e5,
                                h = (Math.cos(e) * d.scaleX * g | 0) / g,
                                i = (Math.sin(e) * d.scaleX * g | 0) / g,
                                j = (Math.sin(f) * -d.scaleY * g | 0) / g,
                                k = (Math.cos(f) * d.scaleY * g | 0) / g,
                                l = this.t.style,
                                m = this.t.currentStyle; if (m) {
                                    c = i, i = -j, j = -c, b = m.filter, l.filter = ""; var n,
                                        o,
                                        q = this.t.offsetWidth,
                                        r = this.t.offsetHeight,
                                        s = "absolute" !== m.position,
                                        t = "progid:DXImageTransform.Microsoft.Matrix(M11=" + h + ", M12=" + i + ", M21=" + j + ", M22=" + k,
                                        u = d.x + q * d.xPercent / 100,
                                        v = d.y + r * d.yPercent / 100; if (null != d.ox && (n = (d.oxp ? q * d.ox * .01 : d.ox) - q / 2, o = (d.oyp ? r * d.oy * .01 : d.oy) - r / 2, u += n - (n * h + o * i), v += o - (n * j + o * k)), s ? (n = q / 2, o = r / 2, t += ", Dx=" + (n - (n * h + o * i) + u) + ", Dy=" + (o - (n * j + o * k) + v) + ")") : t += ", sizingMethod='auto expand')", -1 !== b.indexOf("DXImageTransform.Microsoft.Matrix(") ? l.filter = b.replace(H, t) : l.filter = t + " " + b, (0 === a || 1 === a) && 1 === h && 0 === i && 0 === j && 1 === k && (s && -1 === t.indexOf("Dx=0, Dy=0") || x.test(b) && 100 !== parseFloat(RegExp.$1) || -1 === b.indexOf(b.indexOf("Alpha")) && l.removeAttribute("filter")), !s) {
                                            var y,
                                                z,
                                                A,
                                                B = 8 > p ? 1 : -1; for (n = d.ieOffsetX || 0, o = d.ieOffsetY || 0, d.ieOffsetX = Math.round((q - ((0 > h ? -h : h) * q + (0 > i ? -i : i) * r)) / 2 + u), d.ieOffsetY = Math.round((r - ((0 > k ? -k : k) * r + (0 > j ? -j : j) * q)) / 2 + v), wa = 0; 4 > wa; wa++) {
                                                    z = fa[wa], y = m[z], c = -1 !== y.indexOf("px") ? parseFloat(y) : aa(this.t, z, parseFloat(y), y.replace(w, "")) || 0, A = c !== d[z] ? 2 > wa ? -d.ieOffsetX : -d.ieOffsetY : 2 > wa ? n - d.ieOffsetX : o - d.ieOffsetY, l[z] = (d[z] = Math.round(c - A * (0 === wa || 2 === wa ? 1 : B))) + "px";
                                                }
                                        }
                                }
                        },
                        Ta = S.set3DTransformRatio = S.setTransformRatio = function (a) {
                            var b,
                                c,
                                d,
                                e,
                                f,
                                g,
                                h,
                                i,
                                j,
                                k,
                                l,
                                m,
                                o,
                                p,
                                q,
                                r,
                                s,
                                t,
                                u,
                                v,
                                w,
                                x,
                                y,
                                z = this.data,
                                A = this.t.style,
                                B = z.rotation,
                                C = z.rotationX,
                                D = z.rotationY,
                                E = z.scaleX,
                                F = z.scaleY,
                                G = z.scaleZ,
                                H = z.x,
                                I = z.y,
                                J = z.z,
                                L = z.svg,
                                M = z.perspective,
                                N = z.force3D,
                                O = z.skewY,
                                P = z.skewX; if (O && (P += O, B += O), ((1 === a || 0 === a) && "auto" === N && (this.tween._totalTime === this.tween._totalDuration || !this.tween._totalTime) || !N) && !J && !M && !D && !C && 1 === G || Aa && L || !Fa) return void (B || P || L ? (B *= K, x = P * K, y = 1e5, c = Math.cos(B) * E, f = Math.sin(B) * E, d = Math.sin(B - x) * -F, g = Math.cos(B - x) * F, x && "simple" === z.skewType && (b = Math.tan(x - O * K), b = Math.sqrt(1 + b * b), d *= b, g *= b, O && (b = Math.tan(O * K), b = Math.sqrt(1 + b * b), c *= b, f *= b)), L && (H += z.xOrigin - (z.xOrigin * c + z.yOrigin * d) + z.xOffset, I += z.yOrigin - (z.xOrigin * f + z.yOrigin * g) + z.yOffset, Aa && (z.xPercent || z.yPercent) && (q = this.t.getBBox(), H += .01 * z.xPercent * q.width, I += .01 * z.yPercent * q.height), q = 1e-6, q > H && H > -q && (H = 0), q > I && I > -q && (I = 0)), u = (c * y | 0) / y + "," + (f * y | 0) / y + "," + (d * y | 0) / y + "," + (g * y | 0) / y + "," + H + "," + I + ")", L && Aa ? this.t.setAttribute("transform", "matrix(" + u) : A[Ca] = (z.xPercent || z.yPercent ? "translate(" + z.xPercent + "%," + z.yPercent + "%) matrix(" : "matrix(") + u) : A[Ca] = (z.xPercent || z.yPercent ? "translate(" + z.xPercent + "%," + z.yPercent + "%) matrix(" : "matrix(") + E + ",0,0," + F + "," + H + "," + I + ")"); if (n && (q = 1e-4, q > E && E > -q && (E = G = 2e-5), q > F && F > -q && (F = G = 2e-5), !M || z.z || z.rotationX || z.rotationY || (M = 0)), B || P) B *= K, r = c = Math.cos(B), s = f = Math.sin(B), P && (B -= P * K, r = Math.cos(B), s = Math.sin(B), "simple" === z.skewType && (b = Math.tan((P - O) * K), b = Math.sqrt(1 + b * b), r *= b, s *= b, z.skewY && (b = Math.tan(O * K), b = Math.sqrt(1 + b * b), c *= b, f *= b))), d = -s, g = r; else {
                                    if (!(D || C || 1 !== G || M || L)) return void (A[Ca] = (z.xPercent || z.yPercent ? "translate(" + z.xPercent + "%," + z.yPercent + "%) translate3d(" : "translate3d(") + H + "px," + I + "px," + J + "px)" + (1 !== E || 1 !== F ? " scale(" + E + "," + F + ")" : "")); c = g = 1, d = f = 0;
                                } k = 1, e = h = i = j = l = m = 0, o = M ? -1 / M : 0, p = z.zOrigin, q = 1e-6, v = ",", w = "0", B = D * K, B && (r = Math.cos(B), s = Math.sin(B), i = -s, l = o * -s, e = c * s, h = f * s, k = r, o *= r, c *= r, f *= r), B = C * K, B && (r = Math.cos(B), s = Math.sin(B), b = d * r + e * s, t = g * r + h * s, j = k * s, m = o * s, e = d * -s + e * r, h = g * -s + h * r, k *= r, o *= r, d = b, g = t), 1 !== G && (e *= G, h *= G, k *= G, o *= G), 1 !== F && (d *= F, g *= F, j *= F, m *= F), 1 !== E && (c *= E, f *= E, i *= E, l *= E), (p || L) && (p && (H += e * -p, I += h * -p, J += k * -p + p), L && (H += z.xOrigin - (z.xOrigin * c + z.yOrigin * d) + z.xOffset, I += z.yOrigin - (z.xOrigin * f + z.yOrigin * g) + z.yOffset), q > H && H > -q && (H = w), q > I && I > -q && (I = w), q > J && J > -q && (J = 0)), u = z.xPercent || z.yPercent ? "translate(" + z.xPercent + "%," + z.yPercent + "%) matrix3d(" : "matrix3d(", u += (q > c && c > -q ? w : c) + v + (q > f && f > -q ? w : f) + v + (q > i && i > -q ? w : i), u += v + (q > l && l > -q ? w : l) + v + (q > d && d > -q ? w : d) + v + (q > g && g > -q ? w : g), C || D || 1 !== G ? (u += v + (q > j && j > -q ? w : j) + v + (q > m && m > -q ? w : m) + v + (q > e && e > -q ? w : e), u += v + (q > h && h > -q ? w : h) + v + (q > k && k > -q ? w : k) + v + (q > o && o > -q ? w : o) + v) : u += ",0,0,0,0,1,0,", u += H + v + I + v + J + v + (M ? 1 + -J / M : 1) + ")", A[Ca] = u;
                        }; j = Ga.prototype, j.x = j.y = j.z = j.skewX = j.skewY = j.rotation = j.rotationX = j.rotationY = j.zOrigin = j.xPercent = j.yPercent = j.xOffset = j.yOffset = 0, j.scaleX = j.scaleY = j.scaleZ = 1, ya("transform,scale,scaleX,scaleY,scaleZ,x,y,z,rotation,rotationX,rotationY,rotationZ,skewX,skewY,shortRotation,shortRotationX,shortRotationY,shortRotationZ,transformOrigin,svgOrigin,transformPerspective,directionalRotation,parseTransform,force3D,skewType,xPercent,yPercent,smoothOrigin", {
                            parser: function parser(a, b, c, d, f, h, i) {
                                if (d._lastParsedTransform === i) return f; d._lastParsedTransform = i; var j,
                                    k = i.scale && "function" == typeof i.scale ? i.scale : 0; "function" == typeof i[c] && (j = i[c], i[c] = b), k && (i.scale = k(r, a)); var l,
                                    m,
                                    n,
                                    o,
                                    p,
                                    s,
                                    t,
                                    u,
                                    v,
                                    w = a._gsTransform,
                                    x = a.style,
                                    y = 1e-6,
                                    z = Ba.length,
                                    A = i,
                                    B = {},
                                    C = "transformOrigin",
                                    D = Ra(a, e, !0, A.parseTransform),
                                    E = A.transform && ("function" == typeof A.transform ? A.transform(r, q) : A.transform); if (d._transform = D, E && "string" == typeof E && Ca) m = Q.style, m[Ca] = E, m.display = "block", m.position = "absolute", O.body.appendChild(Q), l = Ra(Q, null, !1), D.svg && (s = D.xOrigin, t = D.yOrigin, l.x -= D.xOffset, l.y -= D.yOffset, (A.transformOrigin || A.svgOrigin) && (E = {}, La(a, ha(A.transformOrigin), E, A.svgOrigin, A.smoothOrigin, !0), s = E.xOrigin, t = E.yOrigin, l.x -= E.xOffset - D.xOffset, l.y -= E.yOffset - D.yOffset), (s || t) && (u = Qa(Q, !0), l.x -= s - (s * u[0] + t * u[2]), l.y -= t - (s * u[1] + t * u[3]))), O.body.removeChild(Q), l.perspective || (l.perspective = D.perspective), null != A.xPercent && (l.xPercent = ja(A.xPercent, D.xPercent)), null != A.yPercent && (l.yPercent = ja(A.yPercent, D.yPercent)); else if ("object" == (typeof A === "undefined" ? "undefined" : _typeof(A))) {
                                        if (l = {
                                            scaleX: ja(null != A.scaleX ? A.scaleX : A.scale, D.scaleX), scaleY: ja(null != A.scaleY ? A.scaleY : A.scale, D.scaleY), scaleZ: ja(A.scaleZ, D.scaleZ), x: ja(A.x, D.x), y: ja(A.y, D.y), z: ja(A.z, D.z), xPercent: ja(A.xPercent, D.xPercent),
                                            yPercent: ja(A.yPercent, D.yPercent), perspective: ja(A.transformPerspective, D.perspective)
                                        }, p = A.directionalRotation, null != p) if ("object" == (typeof p === "undefined" ? "undefined" : _typeof(p))) for (m in p) {
                                            A[m] = p[m];
                                        } else A.rotation = p; "string" == typeof A.x && -1 !== A.x.indexOf("%") && (l.x = 0, l.xPercent = ja(A.x, D.xPercent)), "string" == typeof A.y && -1 !== A.y.indexOf("%") && (l.y = 0, l.yPercent = ja(A.y, D.yPercent)), l.rotation = ka("rotation" in A ? A.rotation : "shortRotation" in A ? A.shortRotation + "_short" : "rotationZ" in A ? A.rotationZ : D.rotation, D.rotation, "rotation", B), Fa && (l.rotationX = ka("rotationX" in A ? A.rotationX : "shortRotationX" in A ? A.shortRotationX + "_short" : D.rotationX || 0, D.rotationX, "rotationX", B), l.rotationY = ka("rotationY" in A ? A.rotationY : "shortRotationY" in A ? A.shortRotationY + "_short" : D.rotationY || 0, D.rotationY, "rotationY", B)), l.skewX = ka(A.skewX, D.skewX), l.skewY = ka(A.skewY, D.skewY);
                                    } for (Fa && null != A.force3D && (D.force3D = A.force3D, o = !0), D.skewType = A.skewType || D.skewType || g.defaultSkewType, n = D.force3D || D.z || D.rotationX || D.rotationY || l.z || l.rotationX || l.rotationY || l.perspective, n || null == A.scale || (l.scaleZ = 1) ; --z > -1;) {
                                        v = Ba[z], E = l[v] - D[v], (E > y || -y > E || null != A[v] || null != M[v]) && (o = !0, f = new ta(D, v, D[v], E, f), v in B && (f.e = B[v]), f.xs0 = 0, f.plugin = h, d._overwriteProps.push(f.n));
                                    } return E = A.transformOrigin, D.svg && (E || A.svgOrigin) && (s = D.xOffset, t = D.yOffset, La(a, ha(E), l, A.svgOrigin, A.smoothOrigin), f = ua(D, "xOrigin", (w ? D : l).xOrigin, l.xOrigin, f, C), f = ua(D, "yOrigin", (w ? D : l).yOrigin, l.yOrigin, f, C), (s !== D.xOffset || t !== D.yOffset) && (f = ua(D, "xOffset", w ? s : D.xOffset, D.xOffset, f, C), f = ua(D, "yOffset", w ? t : D.yOffset, D.yOffset, f, C)), E = "0px 0px"), (E || Fa && n && D.zOrigin) && (Ca ? (o = !0, v = Ea, E = (E || _(a, v, e, !1, "50% 50%")) + "", f = new ta(x, v, 0, 0, f, -1, C), f.b = x[v], f.plugin = h, Fa ? (m = D.zOrigin, E = E.split(" "), D.zOrigin = (E.length > 2 && (0 === m || "0px" !== E[2]) ? parseFloat(E[2]) : m) || 0, f.xs0 = f.e = E[0] + " " + (E[1] || "50%") + " 0px", f = new ta(D, "zOrigin", 0, 0, f, -1, f.n), f.b = m, f.xs0 = f.e = D.zOrigin) : f.xs0 = f.e = E) : ha(E + "", D)), o && (d._transformType = D.svg && Aa || !n && 3 !== this._transformType ? 2 : 3), j && (i[c] = j), k && (i.scale = k), f;
                            }, prefix: !0
                        }), ya("boxShadow", { defaultValue: "0px 0px 0px 0px #999", prefix: !0, color: !0, multi: !0, keyword: "inset" }), ya("borderRadius", {
                            defaultValue: "0px", parser: function parser(a, b, c, f, g, h) {
                                b = this.format(b); var i,
                                    j,
                                    k,
                                    l,
                                    m,
                                    n,
                                    o,
                                    p,
                                    q,
                                    r,
                                    s,
                                    t,
                                    u,
                                    v,
                                    w,
                                    x,
                                    y = ["borderTopLeftRadius", "borderTopRightRadius", "borderBottomRightRadius", "borderBottomLeftRadius"],
                                    z = a.style; for (q = parseFloat(a.offsetWidth), r = parseFloat(a.offsetHeight), i = b.split(" "), j = 0; j < y.length; j++) {
                                        this.p.indexOf("border") && (y[j] = Z(y[j])), m = l = _(a, y[j], e, !1, "0px"), -1 !== m.indexOf(" ") && (l = m.split(" "), m = l[0], l = l[1]), n = k = i[j], o = parseFloat(m), t = m.substr((o + "").length), u = "=" === n.charAt(1), u ? (p = parseInt(n.charAt(0) + "1", 10), n = n.substr(2), p *= parseFloat(n), s = n.substr((p + "").length - (0 > p ? 1 : 0)) || "") : (p = parseFloat(n), s = n.substr((p + "").length)), "" === s && (s = d[c] || t), s !== t && (v = aa(a, "borderLeft", o, t), w = aa(a, "borderTop", o, t), "%" === s ? (m = v / q * 100 + "%", l = w / r * 100 + "%") : "em" === s ? (x = aa(a, "borderLeft", 1, "em"), m = v / x + "em", l = w / x + "em") : (m = v + "px", l = w + "px"), u && (n = parseFloat(m) + p + s, k = parseFloat(l) + p + s)), g = va(z, y[j], m + " " + l, n + " " + k, !1, "0px", g);
                                    } return g;
                            }, prefix: !0, formatter: qa("0px 0px 0px 0px", !1, !0)
                        }), ya("borderBottomLeftRadius,borderBottomRightRadius,borderTopLeftRadius,borderTopRightRadius", {
                            defaultValue: "0px", parser: function parser(a, b, c, d, f, g) {
                                return va(a.style, c, this.format(_(a, c, e, !1, "0px 0px")), this.format(b), !1, "0px", f);
                            }, prefix: !0, formatter: qa("0px 0px", !1, !0)
                        }), ya("backgroundPosition", {
                            defaultValue: "0 0", parser: function parser(a, b, c, d, f, g) {
                                var h,
                                    i,
                                    j,
                                    k,
                                    l,
                                    m,
                                    n = "background-position",
                                    o = e || $(a, null),
                                    q = this.format((o ? p ? o.getPropertyValue(n + "-x") + " " + o.getPropertyValue(n + "-y") : o.getPropertyValue(n) : a.currentStyle.backgroundPositionX + " " + a.currentStyle.backgroundPositionY) || "0 0"),
                                    r = this.format(b); if (-1 !== q.indexOf("%") != (-1 !== r.indexOf("%")) && r.split(",").length < 2 && (m = _(a, "backgroundImage").replace(D, ""), m && "none" !== m)) {
                                        for (h = q.split(" "), i = r.split(" "), R.setAttribute("src", m), j = 2; --j > -1;) {
                                            q = h[j], k = -1 !== q.indexOf("%"), k !== (-1 !== i[j].indexOf("%")) && (l = 0 === j ? a.offsetWidth - R.width : a.offsetHeight - R.height, h[j] = k ? parseFloat(q) / 100 * l + "px" : parseFloat(q) / l * 100 + "%");
                                        } q = h.join(" ");
                                    } return this.parseComplex(a.style, q, r, f, g);
                            }, formatter: ha
                        }), ya("backgroundSize", {
                            defaultValue: "0 0", formatter: function formatter(a) {
                                return a += "", ha(-1 === a.indexOf(" ") ? a + " " + a : a);
                            }
                        }), ya("perspective", { defaultValue: "0px", prefix: !0 }), ya("perspectiveOrigin", { defaultValue: "50% 50%", prefix: !0 }), ya("transformStyle", { prefix: !0 }), ya("backfaceVisibility", { prefix: !0 }), ya("userSelect", { prefix: !0 }), ya("margin", { parser: ra("marginTop,marginRight,marginBottom,marginLeft") }), ya("padding", { parser: ra("paddingTop,paddingRight,paddingBottom,paddingLeft") }), ya("clip", {
                            defaultValue: "rect(0px,0px,0px,0px)", parser: function parser(a, b, c, d, f, g) {
                                var h, i, j; return 9 > p ? (i = a.currentStyle, j = 8 > p ? " " : ",", h = "rect(" + i.clipTop + j + i.clipRight + j + i.clipBottom + j + i.clipLeft + ")", b = this.format(b).split(",").join(j)) : (h = this.format(_(a, this.p, e, !1, this.dflt)), b = this.format(b)), this.parseComplex(a.style, h, b, f, g);
                            }
                        }), ya("textShadow", { defaultValue: "0px 0px 0px #999", color: !0, multi: !0 }), ya("autoRound,strictUnits", {
                            parser: function parser(a, b, c, d, e) {
                                return e;
                            }
                        }), ya("border", {
                            defaultValue: "0px solid #000", parser: function parser(a, b, c, d, f, g) {
                                var h = _(a, "borderTopWidth", e, !1, "0px"),
                                    i = this.format(b).split(" "),
                                    j = i[0].replace(w, ""); return "px" !== j && (h = parseFloat(h) / aa(a, "borderTopWidth", 1, j) + j), this.parseComplex(a.style, this.format(h + " " + _(a, "borderTopStyle", e, !1, "solid") + " " + _(a, "borderTopColor", e, !1, "#000")), i.join(" "), f, g);
                            }, color: !0, formatter: function formatter(a) {
                                var b = a.split(" "); return b[0] + " " + (b[1] || "solid") + " " + (a.match(pa) || ["#000"])[0];
                            }
                        }), ya("borderWidth", { parser: ra("borderTopWidth,borderRightWidth,borderBottomWidth,borderLeftWidth") }), ya("float,cssFloat,styleFloat", {
                            parser: function parser(a, b, c, d, e, f) {
                                var g = a.style,
                                    h = "cssFloat" in g ? "cssFloat" : "styleFloat"; return new ta(g, h, 0, 0, e, -1, c, !1, 0, g[h], b);
                            }
                        }); var Ua = function Ua(a) {
                            var b,
                                c = this.t,
                                d = c.filter || _(this.data, "filter") || "",
                                e = this.s + this.c * a | 0; 100 === e && (-1 === d.indexOf("atrix(") && -1 === d.indexOf("radient(") && -1 === d.indexOf("oader(") ? (c.removeAttribute("filter"), b = !_(this.data, "filter")) : (c.filter = d.replace(z, ""), b = !0)), b || (this.xn1 && (c.filter = d = d || "alpha(opacity=" + e + ")"), -1 === d.indexOf("pacity") ? 0 === e && this.xn1 || (c.filter = d + " alpha(opacity=" + e + ")") : c.filter = d.replace(x, "opacity=" + e));
                        }; ya("opacity,alpha,autoAlpha", {
                            defaultValue: "1", parser: function parser(a, b, c, d, f, g) {
                                var h = parseFloat(_(a, "opacity", e, !1, "1")),
                                    i = a.style,
                                    j = "autoAlpha" === c; return "string" == typeof b && "=" === b.charAt(1) && (b = ("-" === b.charAt(0) ? -1 : 1) * parseFloat(b.substr(2)) + h), j && 1 === h && "hidden" === _(a, "visibility", e) && 0 !== b && (h = 0), U ? f = new ta(i, "opacity", h, b - h, f) : (f = new ta(i, "opacity", 100 * h, 100 * (b - h), f), f.xn1 = j ? 1 : 0, i.zoom = 1, f.type = 2, f.b = "alpha(opacity=" + f.s + ")", f.e = "alpha(opacity=" + (f.s + f.c) + ")", f.data = a, f.plugin = g, f.setRatio = Ua), j && (f = new ta(i, "visibility", 0, 0, f, -1, null, !1, 0, 0 !== h ? "inherit" : "hidden", 0 === b ? "hidden" : "inherit"), f.xs0 = "inherit", d._overwriteProps.push(f.n), d._overwriteProps.push(c)), f;
                            }
                        }); var Va = function Va(a, b) {
                            b && (a.removeProperty ? (("ms" === b.substr(0, 2) || "webkit" === b.substr(0, 6)) && (b = "-" + b), a.removeProperty(b.replace(B, "-$1").toLowerCase())) : a.removeAttribute(b));
                        },
                        Wa = function Wa(a) {
                            if (this.t._gsClassPT = this, 1 === a || 0 === a) {
                                this.t.setAttribute("class", 0 === a ? this.b : this.e); for (var b = this.data, c = this.t.style; b;) {
                                    b.v ? c[b.p] = b.v : Va(c, b.p), b = b._next;
                                } 1 === a && this.t._gsClassPT === this && (this.t._gsClassPT = null);
                            } else this.t.getAttribute("class") !== this.e && this.t.setAttribute("class", this.e);
                        }; ya("className", {
                            parser: function parser(a, b, d, f, g, h, i) {
                                var j,
                                    k,
                                    l,
                                    m,
                                    n,
                                    o = a.getAttribute("class") || "",
                                    p = a.style.cssText; if (g = f._classNamePT = new ta(a, d, 0, 0, g, 2), g.setRatio = Wa, g.pr = -11, c = !0, g.b = o, k = ca(a, e), l = a._gsClassPT) {
                                        for (m = {}, n = l.data; n;) {
                                            m[n.p] = 1, n = n._next;
                                        } l.setRatio(1);
                                    } return a._gsClassPT = g, g.e = "=" !== b.charAt(1) ? b : o.replace(new RegExp("(?:\\s|^)" + b.substr(2) + "(?![\\w-])"), "") + ("+" === b.charAt(0) ? " " + b.substr(2) : ""), a.setAttribute("class", g.e), j = da(a, k, ca(a), i, m), a.setAttribute("class", o), g.data = j.firstMPT, a.style.cssText = p, g = g.xfirst = f.parse(a, j.difs, g, h);
                            }
                        }); var Xa = function Xa(a) {
                            if ((1 === a || 0 === a) && this.data._totalTime === this.data._totalDuration && "isFromStart" !== this.data.data) {
                                var b,
                                    c,
                                    d,
                                    e,
                                    f,
                                    g = this.t.style,
                                    h = i.transform.parse; if ("all" === this.e) g.cssText = "", e = !0; else for (b = this.e.split(" ").join("").split(","), d = b.length; --d > -1;) {
                                        c = b[d], i[c] && (i[c].parse === h ? e = !0 : c = "transformOrigin" === c ? Ea : i[c].p), Va(g, c);
                                    } e && (Va(g, Ca), f = this.t._gsTransform, f && (f.svg && (this.t.removeAttribute("data-svg-origin"), this.t.removeAttribute("transform")), delete this.t._gsTransform));
                            }
                        }; for (ya("clearProps", {
            parser: function parser(a, b, d, e, f) {
                            return f = new ta(a, d, 0, 0, f, 2), f.setRatio = Xa, f.e = b, f.pr = -10, f.data = e._tween, c = !0, f;
                        }
                        }), j = "bezier,throwProps,physicsProps,physics2D".split(","), wa = j.length; wa--;) {
                            za(j[wa]);
                        } j = g.prototype, j._firstPT = j._lastParsedTransform = j._transform = null, j._onInitTween = function (a, b, h, j) {
                            if (!a.nodeType) return !1; this._target = q = a, this._tween = h, this._vars = b, r = j, k = b.autoRound, c = !1, d = b.suffixMap || g.suffixMap, e = $(a, ""), f = this._overwriteProps; var n,
                                p,
                                s,
                                t,
                                u,
                                v,
                                w,
                                x,
                                z,
                                A = a.style; if (l && "" === A.zIndex && (n = _(a, "zIndex", e), ("auto" === n || "" === n) && this._addLazySet(A, "zIndex", 0)), "string" == typeof b && (t = A.cssText, n = ca(a, e), A.cssText = t + ";" + b, n = da(a, n, ca(a)).difs, !U && y.test(b) && (n.opacity = parseFloat(RegExp.$1)), b = n, A.cssText = t), b.className ? this._firstPT = p = i.className.parse(a, b.className, "className", this, null, null, b) : this._firstPT = p = this.parse(a, b, null), this._transformType) {
                                    for (z = 3 === this._transformType, Ca ? m && (l = !0, "" === A.zIndex && (w = _(a, "zIndex", e), ("auto" === w || "" === w) && this._addLazySet(A, "zIndex", 0)), o && this._addLazySet(A, "WebkitBackfaceVisibility", this._vars.WebkitBackfaceVisibility || (z ? "visible" : "hidden"))) : A.zoom = 1, s = p; s && s._next;) {
                                        s = s._next;
                                    } x = new ta(a, "transform", 0, 0, null, 2), this._linkCSSP(x, null, s), x.setRatio = Ca ? Ta : Sa, x.data = this._transform || Ra(a, e, !0), x.tween = h, x.pr = -1, f.pop();
                                } if (c) {
                                    for (; p;) {
                                        for (v = p._next, s = t; s && s.pr > p.pr;) {
                                            s = s._next;
                                        } (p._prev = s ? s._prev : u) ? p._prev._next = p : t = p, (p._next = s) ? s._prev = p : u = p, p = v;
                                    } this._firstPT = t;
                                } return !0;
                        }, j.parse = function (a, b, c, f) {
                            var g,
                                h,
                                j,
                                l,
                                m,
                                n,
                                o,
                                p,
                                s,
                                t,
                                u = a.style; for (g in b) {
                                    n = b[g], "function" == typeof n && (n = n(r, q)), h = i[g], h ? c = h.parse(a, n, g, this, c, f, b) : (m = _(a, g, e) + "", s = "string" == typeof n, "color" === g || "fill" === g || "stroke" === g || -1 !== g.indexOf("Color") || s && A.test(n) ? (s || (n = na(n), n = (n.length > 3 ? "rgba(" : "rgb(") + n.join(",") + ")"), c = va(u, g, m, n, !0, "transparent", c, 0, f)) : s && J.test(n) ? c = va(u, g, m, n, !0, null, c, 0, f) : (j = parseFloat(m), o = j || 0 === j ? m.substr((j + "").length) : "", ("" === m || "auto" === m) && ("width" === g || "height" === g ? (j = ga(a, g, e), o = "px") : "left" === g || "top" === g ? (j = ba(a, g, e), o = "px") : (j = "opacity" !== g ? 0 : 1, o = "")), t = s && "=" === n.charAt(1), t ? (l = parseInt(n.charAt(0) + "1", 10), n = n.substr(2), l *= parseFloat(n), p = n.replace(w, "")) : (l = parseFloat(n), p = s ? n.replace(w, "") : ""), "" === p && (p = g in d ? d[g] : o), n = l || 0 === l ? (t ? l + j : l) + p : b[g], o !== p && "" !== p && (l || 0 === l) && j && (j = aa(a, g, j, o), "%" === p ? (j /= aa(a, g, 100, "%") / 100, b.strictUnits !== !0 && (m = j + "%")) : "em" === p || "rem" === p || "vw" === p || "vh" === p ? j /= aa(a, g, 1, p) : "px" !== p && (l = aa(a, g, l, p), p = "px"), t && (l || 0 === l) && (n = l + j + p)), t && (l += j), !j && 0 !== j || !l && 0 !== l ? void 0 !== u[g] && (n || n + "" != "NaN" && null != n) ? (c = new ta(u, g, l || j || 0, 0, c, -1, g, !1, 0, m, n), c.xs0 = "none" !== n || "display" !== g && -1 === g.indexOf("Style") ? n : m) : W("invalid " + g + " tween value: " + b[g]) : (c = new ta(u, g, j, l - j, c, 0, g, k !== !1 && ("px" === p || "zIndex" === g), 0, m, n), c.xs0 = p))), f && c && !c.plugin && (c.plugin = f);
                                } return c;
                        }, j.setRatio = function (a) {
                            var b,
                                c,
                                d,
                                e = this._firstPT,
                                f = 1e-6; if (1 !== a || this._tween._time !== this._tween._duration && 0 !== this._tween._time) {
                                    if (a || this._tween._time !== this._tween._duration && 0 !== this._tween._time || this._tween._rawPrevTime === -1e-6) for (; e;) {
                                        if (b = e.c * a + e.s, e.r ? b = Math.round(b) : f > b && b > -f && (b = 0), e.type) {
                                            if (1 === e.type) {
                                                if (d = e.l, 2 === d) e.t[e.p] = e.xs0 + b + e.xs1 + e.xn1 + e.xs2; else if (3 === d) e.t[e.p] = e.xs0 + b + e.xs1 + e.xn1 + e.xs2 + e.xn2 + e.xs3; else if (4 === d) e.t[e.p] = e.xs0 + b + e.xs1 + e.xn1 + e.xs2 + e.xn2 + e.xs3 + e.xn3 + e.xs4; else if (5 === d) e.t[e.p] = e.xs0 + b + e.xs1 + e.xn1 + e.xs2 + e.xn2 + e.xs3 + e.xn3 + e.xs4 + e.xn4 + e.xs5; else {
                                                    for (c = e.xs0 + b + e.xs1, d = 1; d < e.l; d++) {
                                                        c += e["xn" + d] + e["xs" + (d + 1)];
                                                    } e.t[e.p] = c;
                                                }
                                            } else -1 === e.type ? e.t[e.p] = e.xs0 : e.setRatio && e.setRatio(a);
                                        } else e.t[e.p] = b + e.xs0; e = e._next;
                                    } else for (; e;) {
                                        2 !== e.type ? e.t[e.p] = e.b : e.setRatio(a), e = e._next;
                                    }
                                } else for (; e;) {
                                    if (2 !== e.type) {
                                        if (e.r && -1 !== e.type) {
                                            if (b = Math.round(e.s + e.c), e.type) {
                                                if (1 === e.type) {
                                                    for (d = e.l, c = e.xs0 + b + e.xs1, d = 1; d < e.l; d++) {
                                                        c += e["xn" + d] + e["xs" + (d + 1)];
                                                    } e.t[e.p] = c;
                                                }
                                            } else e.t[e.p] = b + e.xs0;
                                        } else e.t[e.p] = e.e;
                                    } else e.setRatio(a); e = e._next;
                                }
                        }, j._enableTransforms = function (a) {
                            this._transform = this._transform || Ra(this._target, e, !0), this._transformType = this._transform.svg && Aa || !a && 3 !== this._transformType ? 2 : 3;
                        }; var Ya = function Ya(a) {
                            this.t[this.p] = this.e, this.data._linkCSSP(this, this._next, null, !0);
                        }; j._addLazySet = function (a, b, c) {
                            var d = this._firstPT = new ta(a, b, 0, 0, this._firstPT, 2); d.e = c, d.setRatio = Ya, d.data = this;
                        }, j._linkCSSP = function (a, b, c, d) {
                            return a && (b && (b._prev = a), a._next && (a._next._prev = a._prev), a._prev ? a._prev._next = a._next : this._firstPT === a && (this._firstPT = a._next, d = !0), c ? c._next = a : d || null !== this._firstPT || (this._firstPT = a), a._next = b, a._prev = c), a;
                        }, j._mod = function (a) {
                            for (var b = this._firstPT; b;) {
                                "function" == typeof a[b.p] && a[b.p] === Math.round && (b.r = 1), b = b._next;
                            }
                        }, j._kill = function (b) {
                            var c,
                                d,
                                e,
                                f = b; if (b.autoAlpha || b.alpha) {
                                    f = {}; for (d in b) {
                                        f[d] = b[d];
                                    } f.opacity = 1, f.autoAlpha && (f.visibility = 1);
                                } for (b.className && (c = this._classNamePT) && (e = c.xfirst, e && e._prev ? this._linkCSSP(e._prev, c._next, e._prev._prev) : e === this._firstPT && (this._firstPT = c._next), c._next && this._linkCSSP(c._next, c._next._next, e._prev), this._classNamePT = null), c = this._firstPT; c;) {
                                    c.plugin && c.plugin !== d && c.plugin._kill && (c.plugin._kill(b), d = c.plugin), c = c._next;
                                } return a.prototype._kill.call(this, f);
                        }; var Za = function Za(a, b, c) {
                            var d, e, f, g; if (a.slice) for (e = a.length; --e > -1;) {
                                Za(a[e], b, c);
                            } else for (d = a.childNodes, e = d.length; --e > -1;) {
                                f = d[e], g = f.type, f.style && (b.push(ca(f)), c && c.push(f)), 1 !== g && 9 !== g && 11 !== g || !f.childNodes.length || Za(f, b, c);
                            }
                        }; return g.cascadeTo = function (a, c, d) {
                            var e,
                                f,
                                g,
                                h,
                                i = b.to(a, c, d),
                                j = [i],
                                k = [],
                                l = [],
                                m = [],
                                n = b._internals.reservedProps; for (a = i._targets || i.target, Za(a, k, m), i.render(c, !0, !0), Za(a, l), i.render(0, !0, !0), i._enabled(!0), e = m.length; --e > -1;) {
                                    if (f = da(m[e], k[e], l[e]), f.firstMPT) {
                                        f = f.difs; for (g in d) {
                                            n[g] && (f[g] = d[g]);
                                        } h = {}; for (g in f) {
                                            h[g] = k[e][g];
                                        } j.push(b.fromTo(m[e], c, h, f));
                                    }
                                } return j;
                        }, a.activate([g]), g;
    }, !0), function () {
        var a = _gsScope._gsDefine.plugin({
            propName: "roundProps", version: "1.6.0", priority: -1, API: 2, init: function init(a, b, c) {
                return this._tween = c, !0;
            }
        }),
            b = function b(a) {
                for (; a;) {
                    a.f || a.blob || (a.m = Math.round), a = a._next;
                }
            },
            c = a.prototype; c._onInitAllProps = function () {
                for (var a, c, d, e = this._tween, f = e.vars.roundProps.join ? e.vars.roundProps : e.vars.roundProps.split(","), g = f.length, h = {}, i = e._propLookup.roundProps; --g > -1;) {
                    h[f[g]] = Math.round;
                } for (g = f.length; --g > -1;) {
                    for (a = f[g], c = e._firstPT; c;) {
                        d = c._next, c.pg ? c.t._mod(h) : c.n === a && (2 === c.f && c.t ? b(c.t._firstPT) : (this._add(c.t, a, c.s, c.c), d && (d._prev = c._prev), c._prev ? c._prev._next = d : e._firstPT === c && (e._firstPT = d), c._next = c._prev = null, e._propLookup[a] = i)), c = d;
                    }
                } return !1;
            }, c._add = function (a, b, c, d) {
                this._addTween(a, b, c, c + d, b, Math.round), this._overwriteProps.push(b);
            };
    }(), function () {
        _gsScope._gsDefine.plugin({
            propName: "attr", API: 2, version: "0.6.0", init: function init(a, b, c, d) {
                var e, f; if ("function" != typeof a.setAttribute) return !1; for (e in b) {
                    f = b[e], "function" == typeof f && (f = f(d, a)), this._addTween(a, "setAttribute", a.getAttribute(e) + "", f + "", e, !1, e), this._overwriteProps.push(e);
                } return !0;
            }
        });
    }(), _gsScope._gsDefine.plugin({
        propName: "directionalRotation", version: "0.3.0", API: 2, init: function init(a, b, c, d) {
            "object" != (typeof b === "undefined" ? "undefined" : _typeof(b)) && (b = { rotation: b }), this.finals = {}; var e,
                f,
                g,
                h,
                i,
                j,
                k = b.useRadians === !0 ? 2 * Math.PI : 360,
                l = 1e-6; for (e in b) {
                    "useRadians" !== e && (h = b[e], "function" == typeof h && (h = h(d, a)), j = (h + "").split("_"), f = j[0], g = parseFloat("function" != typeof a[e] ? a[e] : a[e.indexOf("set") || "function" != typeof a["get" + e.substr(3)] ? e : "get" + e.substr(3)]()), h = this.finals[e] = "string" == typeof f && "=" === f.charAt(1) ? g + parseInt(f.charAt(0) + "1", 10) * Number(f.substr(2)) : Number(f) || 0, i = h - g, j.length && (f = j.join("_"), -1 !== f.indexOf("short") && (i %= k, i !== i % (k / 2) && (i = 0 > i ? i + k : i - k)), -1 !== f.indexOf("_cw") && 0 > i ? i = (i + 9999999999 * k) % k - (i / k | 0) * k : -1 !== f.indexOf("ccw") && i > 0 && (i = (i - 9999999999 * k) % k - (i / k | 0) * k)), (i > l || -l > i) && (this._addTween(a, e, g, g + i, e), this._overwriteProps.push(e)));
                } return !0;
        }, set: function set(a) {
            var b; if (1 !== a) this._super.setRatio.call(this, a); else for (b = this._firstPT; b;) {
                b.f ? b.t[b.p](this.finals[b.p]) : b.t[b.p] = this.finals[b.p], b = b._next;
            }
        }
    })._autoCSS = !0, _gsScope._gsDefine("easing.Back", ["easing.Ease"], function (a) {
        var b,
            c,
            d,
            e = _gsScope.GreenSockGlobals || _gsScope,
            f = e.com.greensock,
            g = 2 * Math.PI,
            h = Math.PI / 2,
            i = f._class,
            j = function j(b, c) {
                var d = i("easing." + b, function () { }, !0),
                    e = d.prototype = new a(); return e.constructor = d, e.getRatio = c, d;
            },
            k = a.register || function () { },
            l = function l(a, b, c, d, e) {
                var f = i("easing." + a, { easeOut: new b(), easeIn: new c(), easeInOut: new d() }, !0); return k(f, a), f;
            },
            m = function m(a, b, c) {
                this.t = a, this.v = b, c && (this.next = c, c.prev = this, this.c = c.v - b, this.gap = c.t - a);
            },
            n = function n(b, c) {
                var d = i("easing." + b, function (a) {
                    this._p1 = a || 0 === a ? a : 1.70158, this._p2 = 1.525 * this._p1;
                }, !0),
                    e = d.prototype = new a(); return e.constructor = d, e.getRatio = c, e.config = function (a) {
                        return new d(a);
                    }, d;
            },
            o = l("Back", n("BackOut", function (a) {
                return (a -= 1) * a * ((this._p1 + 1) * a + this._p1) + 1;
            }), n("BackIn", function (a) {
                return a * a * ((this._p1 + 1) * a - this._p1);
            }), n("BackInOut", function (a) {
                return (a *= 2) < 1 ? .5 * a * a * ((this._p2 + 1) * a - this._p2) : .5 * ((a -= 2) * a * ((this._p2 + 1) * a + this._p2) + 2);
            })),
            p = i("easing.SlowMo", function (a, b, c) {
                b = b || 0 === b ? b : .7, null == a ? a = .7 : a > 1 && (a = 1), this._p = 1 !== a ? b : 0, this._p1 = (1 - a) / 2, this._p2 = a, this._p3 = this._p1 + this._p2, this._calcEnd = c === !0;
            }, !0),
            q = p.prototype = new a(); return q.constructor = p, q.getRatio = function (a) {
                var b = a + (.5 - a) * this._p; return a < this._p1 ? this._calcEnd ? 1 - (a = 1 - a / this._p1) * a : b - (a = 1 - a / this._p1) * a * a * a * b : a > this._p3 ? this._calcEnd ? 1 - (a = (a - this._p3) / this._p1) * a : b + (a - b) * (a = (a - this._p3) / this._p1) * a * a * a : this._calcEnd ? 1 : b;
            }, p.ease = new p(.7, .7), q.config = p.config = function (a, b, c) {
                return new p(a, b, c);
            }, b = i("easing.SteppedEase", function (a) {
                a = a || 1, this._p1 = 1 / a, this._p2 = a + 1;
            }, !0), q = b.prototype = new a(), q.constructor = b, q.getRatio = function (a) {
                return 0 > a ? a = 0 : a >= 1 && (a = .999999999), (this._p2 * a >> 0) * this._p1;
            }, q.config = b.config = function (a) {
                return new b(a);
            }, c = i("easing.RoughEase", function (b) {
                b = b || {}; for (var c, d, e, f, g, h, i = b.taper || "none", j = [], k = 0, l = 0 | (b.points || 20), n = l, o = b.randomize !== !1, p = b.clamp === !0, q = b.template instanceof a ? b.template : null, r = "number" == typeof b.strength ? .4 * b.strength : .4; --n > -1;) {
                    c = o ? Math.random() : 1 / l * n, d = q ? q.getRatio(c) : c, "none" === i ? e = r : "out" === i ? (f = 1 - c, e = f * f * r) : "in" === i ? e = c * c * r : .5 > c ? (f = 2 * c, e = f * f * .5 * r) : (f = 2 * (1 - c), e = f * f * .5 * r), o ? d += Math.random() * e - .5 * e : n % 2 ? d += .5 * e : d -= .5 * e, p && (d > 1 ? d = 1 : 0 > d && (d = 0)), j[k++] = { x: c, y: d };
                } for (j.sort(function (a, b) {
                  return a.x - b.x;
                }), h = new m(1, 1, null), n = l; --n > -1;) {
                    g = j[n], h = new m(g.x, g.y, h);
                } this._prev = new m(0, 0, 0 !== h.t ? h : h.next);
            }, !0), q = c.prototype = new a(), q.constructor = c, q.getRatio = function (a) {
                var b = this._prev; if (a > b.t) {
                    for (; b.next && a >= b.t;) {
                        b = b.next;
                    } b = b.prev;
                } else for (; b.prev && a <= b.t;) {
                    b = b.prev;
                } return this._prev = b, b.v + (a - b.t) / b.gap * b.c;
            }, q.config = function (a) {
                return new c(a);
            }, c.ease = new c(), l("Bounce", j("BounceOut", function (a) {
                return 1 / 2.75 > a ? 7.5625 * a * a : 2 / 2.75 > a ? 7.5625 * (a -= 1.5 / 2.75) * a + .75 : 2.5 / 2.75 > a ? 7.5625 * (a -= 2.25 / 2.75) * a + .9375 : 7.5625 * (a -= 2.625 / 2.75) * a + .984375;
            }), j("BounceIn", function (a) {
                return (a = 1 - a) < 1 / 2.75 ? 1 - 7.5625 * a * a : 2 / 2.75 > a ? 1 - (7.5625 * (a -= 1.5 / 2.75) * a + .75) : 2.5 / 2.75 > a ? 1 - (7.5625 * (a -= 2.25 / 2.75) * a + .9375) : 1 - (7.5625 * (a -= 2.625 / 2.75) * a + .984375);
            }), j("BounceInOut", function (a) {
                var b = .5 > a; return a = b ? 1 - 2 * a : 2 * a - 1, a = 1 / 2.75 > a ? 7.5625 * a * a : 2 / 2.75 > a ? 7.5625 * (a -= 1.5 / 2.75) * a + .75 : 2.5 / 2.75 > a ? 7.5625 * (a -= 2.25 / 2.75) * a + .9375 : 7.5625 * (a -= 2.625 / 2.75) * a + .984375, b ? .5 * (1 - a) : .5 * a + .5;
            })), l("Circ", j("CircOut", function (a) {
                return Math.sqrt(1 - (a -= 1) * a);
            }), j("CircIn", function (a) {
                return -(Math.sqrt(1 - a * a) - 1);
            }), j("CircInOut", function (a) {
                return (a *= 2) < 1 ? -.5 * (Math.sqrt(1 - a * a) - 1) : .5 * (Math.sqrt(1 - (a -= 2) * a) + 1);
            })), d = function d(b, c, _d) {
                var e = i("easing." + b, function (a, b) {
                    this._p1 = a >= 1 ? a : 1, this._p2 = (b || _d) / (1 > a ? a : 1), this._p3 = this._p2 / g * (Math.asin(1 / this._p1) || 0), this._p2 = g / this._p2;
                }, !0),
                    f = e.prototype = new a(); return f.constructor = e, f.getRatio = c, f.config = function (a, b) {
                        return new e(a, b);
                    }, e;
            }, l("Elastic", d("ElasticOut", function (a) {
                return this._p1 * Math.pow(2, -10 * a) * Math.sin((a - this._p3) * this._p2) + 1;
            }, .3), d("ElasticIn", function (a) {
                return -(this._p1 * Math.pow(2, 10 * (a -= 1)) * Math.sin((a - this._p3) * this._p2));
            }, .3), d("ElasticInOut", function (a) {
                return (a *= 2) < 1 ? -.5 * (this._p1 * Math.pow(2, 10 * (a -= 1)) * Math.sin((a - this._p3) * this._p2)) : this._p1 * Math.pow(2, -10 * (a -= 1)) * Math.sin((a - this._p3) * this._p2) * .5 + 1;
            }, .45)), l("Expo", j("ExpoOut", function (a) {
                return 1 - Math.pow(2, -10 * a);
            }), j("ExpoIn", function (a) {
                return Math.pow(2, 10 * (a - 1)) - .001;
            }), j("ExpoInOut", function (a) {
                return (a *= 2) < 1 ? .5 * Math.pow(2, 10 * (a - 1)) : .5 * (2 - Math.pow(2, -10 * (a - 1)));
            })), l("Sine", j("SineOut", function (a) {
                return Math.sin(a * h);
            }), j("SineIn", function (a) {
                return -Math.cos(a * h) + 1;
            }), j("SineInOut", function (a) {
                return -.5 * (Math.cos(Math.PI * a) - 1);
            })), i("easing.EaseLookup", {
                find: function find(b) {
                    return a.map[b];
                }
            }, !0), k(e.SlowMo, "SlowMo", "ease,"), k(c, "RoughEase", "ease,"), k(b, "SteppedEase", "ease,"), o;
    }, !0);
}), _gsScope._gsDefine && _gsScope._gsQueue.pop()(), function (a, b) {
    "use strict";
    var c = {},
        d = a.document,
        e = a.GreenSockGlobals = a.GreenSockGlobals || a; if (!e.TweenLite) {
            var f,
                g,
                h,
                i,
                j,
                k = function k(a) {
                    var b,
                        c = a.split("."),
                        d = e; for (b = 0; b < c.length; b++) {
                            d[c[b]] = d = d[c[b]] || {};
                        } return d;
                },
                l = k("com.greensock"),
                m = 1e-10,
                n = function n(a) {
                    var b,
                        c = [],
                        d = a.length; for (b = 0; b !== d; c.push(a[b++])) { } return c;
                },
                o = function o() { },
                p = function () {
                    var a = Object.prototype.toString,
                        b = a.call([]); return function (c) {
                            return null != c && (c instanceof Array || "object" == (typeof c === "undefined" ? "undefined" : _typeof(c)) && !!c.push && a.call(c) === b);
                        };
                }(),
                q = {},
                r = function r(d, f, g, h) {
                    this.sc = q[d] ? q[d].sc : [], q[d] = this, this.gsClass = null, this.func = g; var i = []; this.check = function (j) {
                        for (var l, m, n, o, p, s = f.length, t = s; --s > -1;) {
                            (l = q[f[s]] || new r(f[s], [])).gsClass ? (i[s] = l.gsClass, t--) : j && l.sc.push(this);
                        } if (0 === t && g) {
                            if (m = ("com.greensock." + d).split("."), n = m.pop(), o = k(m.join("."))[n] = this.gsClass = g.apply(g, i), h) if (e[n] = c[n] = o, p = "undefined" != typeof module && module.exports, !p && "function" == typeof define && define.amd) define((a.GreenSockAMDPath ? a.GreenSockAMDPath + "/" : "") + d.split(".").pop(), [], function () {
                                return o;
                            }); else if (p) if (d === b) {
                                module.exports = c[b] = o; for (s in c) {
                                    o[s] = c[s];
                                }
                            } else c[b] && (c[b][n] = o); for (s = 0; s < this.sc.length; s++) {
                                this.sc[s].check();
                            }
                        }
                    }, this.check(!0);
                },
                s = a._gsDefine = function (a, b, c, d) {
                    return new r(a, b, c, d);
                },
                t = l._class = function (a, b, c) {
                    return b = b || function () { }, s(a, [], function () {
                        return b;
                    }, c), b;
                }; s.globals = e; var u = [0, 0, 1, 1],
                    v = t("easing.Ease", function (a, b, c, d) {
                        this._func = a, this._type = c || 0, this._power = d || 0, this._params = b ? u.concat(b) : u;
                    }, !0),
                    w = v.map = {},
                    x = v.register = function (a, b, c, d) {
                        for (var e, f, g, h, i = b.split(","), j = i.length, k = (c || "easeIn,easeOut,easeInOut").split(",") ; --j > -1;) {
                            for (f = i[j], e = d ? t("easing." + f, null, !0) : l.easing[f] || {}, g = k.length; --g > -1;) {
                                h = k[g], w[f + "." + h] = w[h + f] = e[h] = a.getRatio ? a : a[h] || new a();
                            }
                        }
                    }; for (h = v.prototype, h._calcEnd = !1, h.getRatio = function (a) {
                      if (this._func) return this._params[0] = a, this._func.apply(null, this._params); var b = this._type,
                          c = this._power,
                          d = 1 === b ? 1 - a : 2 === b ? a : .5 > a ? 2 * a : 2 * (1 - a); return 1 === c ? d *= d : 2 === c ? d *= d * d : 3 === c ? d *= d * d * d : 4 === c && (d *= d * d * d * d), 1 === b ? 1 - d : 2 === b ? d : .5 > a ? d / 2 : 1 - d / 2;
                    }, f = ["Linear", "Quad", "Cubic", "Quart", "Quint,Strong"], g = f.length; --g > -1;) {
                        h = f[g] + ",Power" + g, x(new v(null, null, 1, g), h, "easeOut", !0), x(new v(null, null, 2, g), h, "easeIn" + (0 === g ? ",easeNone" : "")), x(new v(null, null, 3, g), h, "easeInOut");
                    } w.linear = l.easing.Linear.easeIn, w.swing = l.easing.Quad.easeInOut; var y = t("events.EventDispatcher", function (a) {
                        this._listeners = {}, this._eventTarget = a || this;
                    }); h = y.prototype, h.addEventListener = function (a, b, c, d, e) {
                        e = e || 0; var f,
                            g,
                            h = this._listeners[a],
                            k = 0; for (this !== i || j || i.wake(), null == h && (this._listeners[a] = h = []), g = h.length; --g > -1;) {
                                f = h[g], f.c === b && f.s === c ? h.splice(g, 1) : 0 === k && f.pr < e && (k = g + 1);
                            } h.splice(k, 0, { c: b, s: c, up: d, pr: e });
                    }, h.removeEventListener = function (a, b) {
                        var c,
                            d = this._listeners[a]; if (d) for (c = d.length; --c > -1;) {
                                if (d[c].c === b) return void d.splice(c, 1);
                            }
                    }, h.dispatchEvent = function (a) {
                        var b,
                            c,
                            d,
                            e = this._listeners[a]; if (e) for (b = e.length, b > 1 && (e = e.slice(0)), c = this._eventTarget; --b > -1;) {
                                d = e[b], d && (d.up ? d.c.call(d.s || c, { type: a, target: c }) : d.c.call(d.s || c));
                            }
                    }; var z = a.requestAnimationFrame,
                        A = a.cancelAnimationFrame,
                        B = Date.now || function () {
                            return new Date().getTime();
                        },
                        C = B(); for (f = ["ms", "moz", "webkit", "o"], g = f.length; --g > -1 && !z;) {
                            z = a[f[g] + "RequestAnimationFrame"], A = a[f[g] + "CancelAnimationFrame"] || a[f[g] + "CancelRequestAnimationFrame"];
                        } t("Ticker", function (a, b) {
                            var c,
                                e,
                                f,
                                g,
                                h,
                                k = this,
                                l = B(),
                                n = b !== !1 && z ? "auto" : !1,
                                p = 500,
                                q = 33,
                                r = "tick",
                                s = function s(a) {
                                    var b,
                                        d,
                                        i = B() - C; i > p && (l += i - q), C += i, k.time = (C - l) / 1e3, b = k.time - h, (!c || b > 0 || a === !0) && (k.frame++, h += b + (b >= g ? .004 : g - b), d = !0), a !== !0 && (f = e(s)), d && k.dispatchEvent(r);
                                }; y.call(k), k.time = k.frame = 0, k.tick = function () {
                                    s(!0);
                                }, k.lagSmoothing = function (a, b) {
                                    p = a || 1 / m, q = Math.min(b, p, 0);
                                }, k.sleep = function () {
                                    null != f && (n && A ? A(f) : clearTimeout(f), e = o, f = null, k === i && (j = !1));
                                }, k.wake = function (a) {
                                    null !== f ? k.sleep() : a ? l += -C + (C = B()) : k.frame > 10 && (C = B() - p + 5), e = 0 === c ? o : n && z ? z : function (a) {
                                        return setTimeout(a, 1e3 * (h - k.time) + 1 | 0);
                                    }, k === i && (j = !0), s(2);
                                }, k.fps = function (a) {
                                    return arguments.length ? (c = a, g = 1 / (c || 60), h = this.time + g, void k.wake()) : c;
                                }, k.useRAF = function (a) {
                                    return arguments.length ? (k.sleep(), n = a, void k.fps(c)) : n;
                                }, k.fps(a), setTimeout(function () {
                                    "auto" === n && k.frame < 5 && "hidden" !== d.visibilityState && k.useRAF(!1);
                                }, 1500);
                        }), h = l.Ticker.prototype = new l.events.EventDispatcher(), h.constructor = l.Ticker; var D = t("core.Animation", function (a, b) {
                            if (this.vars = b = b || {}, this._duration = this._totalDuration = a || 0, this._delay = Number(b.delay) || 0, this._timeScale = 1, this._active = b.immediateRender === !0, this.data = b.data, this._reversed = b.reversed === !0, W) {
                                j || i.wake(); var c = this.vars.useFrames ? V : W; c.add(this, c._time), this.vars.paused && this.paused(!0);
                            }
                        }); i = D.ticker = new l.Ticker(), h = D.prototype, h._dirty = h._gc = h._initted = h._paused = !1, h._totalTime = h._time = 0, h._rawPrevTime = -1, h._next = h._last = h._onUpdate = h._timeline = h.timeline = null, h._paused = !1; var E = function E() {
                            j && B() - C > 2e3 && i.wake(), setTimeout(E, 2e3);
                        }; E(), h.play = function (a, b) {
                            return null != a && this.seek(a, b), this.reversed(!1).paused(!1);
                        }, h.pause = function (a, b) {
                            return null != a && this.seek(a, b), this.paused(!0);
                        }, h.resume = function (a, b) {
                            return null != a && this.seek(a, b), this.paused(!1);
                        }, h.seek = function (a, b) {
                            return this.totalTime(Number(a), b !== !1);
                        }, h.restart = function (a, b) {
                            return this.reversed(!1).paused(!1).totalTime(a ? -this._delay : 0, b !== !1, !0);
                        }, h.reverse = function (a, b) {
                            return null != a && this.seek(a || this.totalDuration(), b), this.reversed(!0).paused(!1);
                        }, h.render = function (a, b, c) { }, h.invalidate = function () {
                            return this._time = this._totalTime = 0, this._initted = this._gc = !1, this._rawPrevTime = -1, (this._gc || !this.timeline) && this._enabled(!0), this;
                        }, h.isActive = function () {
                            var a,
                                b = this._timeline,
                                c = this._startTime; return !b || !this._gc && !this._paused && b.isActive() && (a = b.rawTime(!0)) >= c && a < c + this.totalDuration() / this._timeScale;
                        }, h._enabled = function (a, b) {
                            return j || i.wake(), this._gc = !a, this._active = this.isActive(), b !== !0 && (a && !this.timeline ? this._timeline.add(this, this._startTime - this._delay) : !a && this.timeline && this._timeline._remove(this, !0)), !1;
                        }, h._kill = function (a, b) {
                            return this._enabled(!1, !1);
                        }, h.kill = function (a, b) {
                            return this._kill(a, b), this;
                        }, h._uncache = function (a) {
                            for (var b = a ? this : this.timeline; b;) {
                                b._dirty = !0, b = b.timeline;
                            } return this;
                        }, h._swapSelfInParams = function (a) {
                            for (var b = a.length, c = a.concat() ; --b > -1;) {
                                "{self}" === a[b] && (c[b] = this);
                            } return c;
                        }, h._callback = function (a) {
                            var b = this.vars,
                                c = b[a],
                                d = b[a + "Params"],
                                e = b[a + "Scope"] || b.callbackScope || this,
                                f = d ? d.length : 0; switch (f) {
                                    case 0:
                                        c.call(e); break; case 1:
                                            c.call(e, d[0]); break; case 2:
                                                c.call(e, d[0], d[1]); break; default:
                                                    c.apply(e, d);
                                }
                        }, h.eventCallback = function (a, b, c, d) {
                            if ("on" === (a || "").substr(0, 2)) {
                                var e = this.vars; if (1 === arguments.length) return e[a]; null == b ? delete e[a] : (e[a] = b, e[a + "Params"] = p(c) && -1 !== c.join("").indexOf("{self}") ? this._swapSelfInParams(c) : c, e[a + "Scope"] = d), "onUpdate" === a && (this._onUpdate = b);
                            } return this;
                        }, h.delay = function (a) {
                            return arguments.length ? (this._timeline.smoothChildTiming && this.startTime(this._startTime + a - this._delay), this._delay = a, this) : this._delay;
                        }, h.duration = function (a) {
                            return arguments.length ? (this._duration = this._totalDuration = a, this._uncache(!0), this._timeline.smoothChildTiming && this._time > 0 && this._time < this._duration && 0 !== a && this.totalTime(this._totalTime * (a / this._duration), !0), this) : (this._dirty = !1, this._duration);
                        }, h.totalDuration = function (a) {
                            return this._dirty = !1, arguments.length ? this.duration(a) : this._totalDuration;
                        }, h.time = function (a, b) {
                            return arguments.length ? (this._dirty && this.totalDuration(), this.totalTime(a > this._duration ? this._duration : a, b)) : this._time;
                        }, h.totalTime = function (a, b, c) {
                            if (j || i.wake(), !arguments.length) return this._totalTime; if (this._timeline) {
                                if (0 > a && !c && (a += this.totalDuration()), this._timeline.smoothChildTiming) {
                                    this._dirty && this.totalDuration(); var d = this._totalDuration,
                                        e = this._timeline; if (a > d && !c && (a = d), this._startTime = (this._paused ? this._pauseTime : e._time) - (this._reversed ? d - a : a) / this._timeScale, e._dirty || this._uncache(!1), e._timeline) for (; e._timeline;) {
                                            e._timeline._time !== (e._startTime + e._totalTime) / e._timeScale && e.totalTime(e._totalTime, !0), e = e._timeline;
                                        }
                                } this._gc && this._enabled(!0, !1), (this._totalTime !== a || 0 === this._duration) && (J.length && Y(), this.render(a, b, !1), J.length && Y());
                            } return this;
                        }, h.progress = h.totalProgress = function (a, b) {
                            var c = this.duration(); return arguments.length ? this.totalTime(c * a, b) : c ? this._time / c : this.ratio;
                        }, h.startTime = function (a) {
                            return arguments.length ? (a !== this._startTime && (this._startTime = a, this.timeline && this.timeline._sortChildren && this.timeline.add(this, a - this._delay)), this) : this._startTime;
                        }, h.endTime = function (a) {
                            return this._startTime + (0 != a ? this.totalDuration() : this.duration()) / this._timeScale;
                        }, h.timeScale = function (a) {
                            if (!arguments.length) return this._timeScale; if (a = a || m, this._timeline && this._timeline.smoothChildTiming) {
                                var b = this._pauseTime,
                                    c = b || 0 === b ? b : this._timeline.totalTime(); this._startTime = c - (c - this._startTime) * this._timeScale / a;
                            } return this._timeScale = a, this._uncache(!1);
                        }, h.reversed = function (a) {
                            return arguments.length ? (a != this._reversed && (this._reversed = a, this.totalTime(this._timeline && !this._timeline.smoothChildTiming ? this.totalDuration() - this._totalTime : this._totalTime, !0)), this) : this._reversed;
                        }, h.paused = function (a) {
                            if (!arguments.length) return this._paused; var b,
                                c,
                                d = this._timeline; return a != this._paused && d && (j || a || i.wake(), b = d.rawTime(), c = b - this._pauseTime, !a && d.smoothChildTiming && (this._startTime += c, this._uncache(!1)), this._pauseTime = a ? b : null, this._paused = a, this._active = this.isActive(), !a && 0 !== c && this._initted && this.duration() && (b = d.smoothChildTiming ? this._totalTime : (b - this._startTime) / this._timeScale, this.render(b, b === this._totalTime, !0))), this._gc && !a && this._enabled(!0, !1), this;
                        }; var F = t("core.SimpleTimeline", function (a) {
                            D.call(this, 0, a), this.autoRemoveChildren = this.smoothChildTiming = !0;
                        }); h = F.prototype = new D(), h.constructor = F, h.kill()._gc = !1, h._first = h._last = h._recent = null, h._sortChildren = !1, h.add = h.insert = function (a, b, c, d) {
                            var e, f; if (a._startTime = Number(b || 0) + a._delay, a._paused && this !== a._timeline && (a._pauseTime = a._startTime + (this.rawTime() - a._startTime) / a._timeScale), a.timeline && a.timeline._remove(a, !0), a.timeline = a._timeline = this, a._gc && a._enabled(!0, !0), e = this._last, this._sortChildren) for (f = a._startTime; e && e._startTime > f;) {
                                e = e._prev;
                            } return e ? (a._next = e._next, e._next = a) : (a._next = this._first, this._first = a), a._next ? a._next._prev = a : this._last = a, a._prev = e, this._recent = a, this._timeline && this._uncache(!0), this;
                        }, h._remove = function (a, b) {
                            return a.timeline === this && (b || a._enabled(!1, !0), a._prev ? a._prev._next = a._next : this._first === a && (this._first = a._next), a._next ? a._next._prev = a._prev : this._last === a && (this._last = a._prev), a._next = a._prev = a.timeline = null, a === this._recent && (this._recent = this._last), this._timeline && this._uncache(!0)), this;
                        }, h.render = function (a, b, c) {
                            var d,
                                e = this._first; for (this._totalTime = this._time = this._rawPrevTime = a; e;) {
                                    d = e._next, (e._active || a >= e._startTime && !e._paused) && (e._reversed ? e.render((e._dirty ? e.totalDuration() : e._totalDuration) - (a - e._startTime) * e._timeScale, b, c) : e.render((a - e._startTime) * e._timeScale, b, c)), e = d;
                                }
                        }, h.rawTime = function () {
                            return j || i.wake(), this._totalTime;
                        }; var G = t("TweenLite", function (b, c, d) {
                            if (D.call(this, c, d), this.render = G.prototype.render, null == b) throw "Cannot tween a null target."; this.target = b = "string" != typeof b ? b : G.selector(b) || b; var e,
                                f,
                                g,
                                h = b.jquery || b.length && b !== a && b[0] && (b[0] === a || b[0].nodeType && b[0].style && !b.nodeType),
                                i = this.vars.overwrite; if (this._overwrite = i = null == i ? U[G.defaultOverwrite] : "number" == typeof i ? i >> 0 : U[i], (h || b instanceof Array || b.push && p(b)) && "number" != typeof b[0]) for (this._targets = g = n(b), this._propLookup = [], this._siblings = [], e = 0; e < g.length; e++) {
                                    f = g[e], f ? "string" != typeof f ? f.length && f !== a && f[0] && (f[0] === a || f[0].nodeType && f[0].style && !f.nodeType) ? (g.splice(e--, 1), this._targets = g = g.concat(n(f))) : (this._siblings[e] = Z(f, this, !1), 1 === i && this._siblings[e].length > 1 && _(f, this, null, 1, this._siblings[e])) : (f = g[e--] = G.selector(f), "string" == typeof f && g.splice(e + 1, 1)) : g.splice(e--, 1);
                                } else this._propLookup = {}, this._siblings = Z(b, this, !1), 1 === i && this._siblings.length > 1 && _(b, this, null, 1, this._siblings); (this.vars.immediateRender || 0 === c && 0 === this._delay && this.vars.immediateRender !== !1) && (this._time = -m, this.render(Math.min(0, -this._delay)));
                        }, !0),
                            H = function H(b) {
                                return b && b.length && b !== a && b[0] && (b[0] === a || b[0].nodeType && b[0].style && !b.nodeType);
                            },
                            I = function I(a, b) {
                                var c,
                                    d = {}; for (c in a) {
                                        T[c] || c in b && "transform" !== c && "x" !== c && "y" !== c && "width" !== c && "height" !== c && "className" !== c && "border" !== c || !(!Q[c] || Q[c] && Q[c]._autoCSS) || (d[c] = a[c], delete a[c]);
                                    } a.css = d;
                            }; h = G.prototype = new D(), h.constructor = G, h.kill()._gc = !1, h.ratio = 0, h._firstPT = h._targets = h._overwrittenProps = h._startAt = null, h._notifyPluginsOfEnabled = h._lazy = !1, G.version = "1.19.1", G.defaultEase = h._ease = new v(null, null, 1, 1), G.defaultOverwrite = "auto", G.ticker = i, G.autoSleep = 120, G.lagSmoothing = function (a, b) {
                                i.lagSmoothing(a, b);
                            }, G.selector = a.$ || a.jQuery || function (b) {
                                var c = a.$ || a.jQuery; return c ? (G.selector = c, c(b)) : "undefined" == typeof d ? b : d.querySelectorAll ? d.querySelectorAll(b) : d.getElementById("#" === b.charAt(0) ? b.substr(1) : b);
                            }; var J = [],
                                K = {},
                                L = /(?:(-|-=|\+=)?\d*\.?\d*(?:e[\-+]?\d+)?)[0-9]/gi,
                                M = function M(a) {
                                    for (var b, c = this._firstPT, d = 1e-6; c;) {
                                        b = c.blob ? 1 === a ? this.end : a ? this.join("") : this.start : c.c * a + c.s, c.m ? b = c.m(b, this._target || c.t) : d > b && b > -d && !c.blob && (b = 0), c.f ? c.fp ? c.t[c.p](c.fp, b) : c.t[c.p](b) : c.t[c.p] = b, c = c._next;
                                    }
                                },
                                N = function N(a, b, c, d) {
                                    var e,
                                        f,
                                        g,
                                        h,
                                        i,
                                        j,
                                        k,
                                        l = [],
                                        m = 0,
                                        n = "",
                                        o = 0; for (l.start = a, l.end = b, a = l[0] = a + "", b = l[1] = b + "", c && (c(l), a = l[0], b = l[1]), l.length = 0, e = a.match(L) || [], f = b.match(L) || [], d && (d._next = null, d.blob = 1, l._firstPT = l._applyPT = d), i = f.length, h = 0; i > h; h++) {
                                            k = f[h], j = b.substr(m, b.indexOf(k, m) - m), n += j || !h ? j : ",", m += j.length, o ? o = (o + 1) % 5 : "rgba(" === j.substr(-5) && (o = 1), k === e[h] || e.length <= h ? n += k : (n && (l.push(n), n = ""), g = parseFloat(e[h]), l.push(g), l._firstPT = { _next: l._firstPT, t: l, p: l.length - 1, s: g, c: ("=" === k.charAt(1) ? parseInt(k.charAt(0) + "1", 10) * parseFloat(k.substr(2)) : parseFloat(k) - g) || 0, f: 0, m: o && 4 > o ? Math.round : 0 }), m += k.length;
                                        } return n += b.substr(m), n && l.push(n), l.setRatio = M, l;
                                },
                                O = function O(a, b, c, d, e, f, g, h, i) {
                                    "function" == typeof d && (d = d(i || 0, a)); var j,
                                        k = _typeof(a[b]),
                                        l = "function" !== k ? "" : b.indexOf("set") || "function" != typeof a["get" + b.substr(3)] ? b : "get" + b.substr(3),
                                        m = "get" !== c ? c : l ? g ? a[l](g) : a[l]() : a[b],
                                        n = "string" == typeof d && "=" === d.charAt(1),
                                        o = { t: a, p: b, s: m, f: "function" === k, pg: 0, n: e || b, m: f ? "function" == typeof f ? f : Math.round : 0, pr: 0, c: n ? parseInt(d.charAt(0) + "1", 10) * parseFloat(d.substr(2)) : parseFloat(d) - m || 0 }; return ("number" != typeof m || "number" != typeof d && !n) && (g || isNaN(m) || !n && isNaN(d) || "boolean" == typeof m || "boolean" == typeof d ? (o.fp = g, j = N(m, n ? o.s + o.c : d, h || G.defaultStringFilter, o), o = { t: j, p: "setRatio", s: 0, c: 1, f: 2, pg: 0, n: e || b, pr: 0, m: 0 }) : (o.s = parseFloat(m), n || (o.c = parseFloat(d) - o.s || 0))), o.c ? ((o._next = this._firstPT) && (o._next._prev = o), this._firstPT = o, o) : void 0;
                                },
                                P = G._internals = { isArray: p, isSelector: H, lazyTweens: J, blobDif: N },
                                Q = G._plugins = {},
                                R = P.tweenLookup = {},
                                S = 0,
                                T = P.reservedProps = { ease: 1, delay: 1, overwrite: 1, onComplete: 1, onCompleteParams: 1, onCompleteScope: 1, useFrames: 1, runBackwards: 1, startAt: 1, onUpdate: 1, onUpdateParams: 1, onUpdateScope: 1, onStart: 1, onStartParams: 1, onStartScope: 1, onReverseComplete: 1, onReverseCompleteParams: 1, onReverseCompleteScope: 1, onRepeat: 1, onRepeatParams: 1, onRepeatScope: 1, easeParams: 1, yoyo: 1, immediateRender: 1, repeat: 1, repeatDelay: 1, data: 1, paused: 1, reversed: 1, autoCSS: 1, lazy: 1, onOverwrite: 1, callbackScope: 1, stringFilter: 1, id: 1 },
                                U = { none: 0, all: 1, auto: 2, concurrent: 3, allOnStart: 4, preexisting: 5, "true": 1, "false": 0 },
                                V = D._rootFramesTimeline = new F(),
                                W = D._rootTimeline = new F(),
                                X = 30,
                                Y = P.lazyRender = function () {
                                    var a,
                                        b = J.length; for (K = {}; --b > -1;) {
                                            a = J[b], a && a._lazy !== !1 && (a.render(a._lazy[0], a._lazy[1], !0), a._lazy = !1);
                                        } J.length = 0;
                                }; W._startTime = i.time, V._startTime = i.frame, W._active = V._active = !0, setTimeout(Y, 1), D._updateRoot = G.render = function () {
                                    var a, b, c; if (J.length && Y(), W.render((i.time - W._startTime) * W._timeScale, !1, !1), V.render((i.frame - V._startTime) * V._timeScale, !1, !1), J.length && Y(), i.frame >= X) {
                                        X = i.frame + (parseInt(G.autoSleep, 10) || 120); for (c in R) {
                                            for (b = R[c].tweens, a = b.length; --a > -1;) {
                                                b[a]._gc && b.splice(a, 1);
                                            } 0 === b.length && delete R[c];
                                        } if (c = W._first, (!c || c._paused) && G.autoSleep && !V._first && 1 === i._listeners.tick.length) {
                                            for (; c && c._paused;) {
                                                c = c._next;
                                            } c || i.sleep();
                                        }
                                    }
                                }, i.addEventListener("tick", D._updateRoot); var Z = function Z(a, b, c) {
                                    var d,
                                        e,
                                        f = a._gsTweenID; if (R[f || (a._gsTweenID = f = "t" + S++)] || (R[f] = { target: a, tweens: [] }), b && (d = R[f].tweens, d[e = d.length] = b, c)) for (; --e > -1;) {
                                            d[e] === b && d.splice(e, 1);
                                        } return R[f].tweens;
                                },
                                    $ = function $(a, b, c, d) {
                                        var e,
                                            f,
                                            g = a.vars.onOverwrite; return g && (e = g(a, b, c, d)), g = G.onOverwrite, g && (f = g(a, b, c, d)), e !== !1 && f !== !1;
                                    },
                                    _ = function _(a, b, c, d, e) {
                                        var f, g, h, i; if (1 === d || d >= 4) {
                                            for (i = e.length, f = 0; i > f; f++) {
                                                if ((h = e[f]) !== b) h._gc || h._kill(null, a, b) && (g = !0); else if (5 === d) break;
                                            } return g;
                                        } var j,
                                            k = b._startTime + m,
                                            l = [],
                                            n = 0,
                                            o = 0 === b._duration; for (f = e.length; --f > -1;) {
                                                (h = e[f]) === b || h._gc || h._paused || (h._timeline !== b._timeline ? (j = j || aa(b, 0, o), 0 === aa(h, j, o) && (l[n++] = h)) : h._startTime <= k && h._startTime + h.totalDuration() / h._timeScale > k && ((o || !h._initted) && k - h._startTime <= 2e-10 || (l[n++] = h)));
                                            } for (f = n; --f > -1;) {
                                                if (h = l[f], 2 === d && h._kill(c, a, b) && (g = !0), 2 !== d || !h._firstPT && h._initted) {
                                                    if (2 !== d && !$(h, b)) continue; h._enabled(!1, !1) && (g = !0);
                                                }
                                            } return g;
                                    },
                                    aa = function aa(a, b, c) {
                                        for (var d = a._timeline, e = d._timeScale, f = a._startTime; d._timeline;) {
                                            if (f += d._startTime, e *= d._timeScale, d._paused) return -100; d = d._timeline;
                                        } return f /= e, f > b ? f - b : c && f === b || !a._initted && 2 * m > f - b ? m : (f += a.totalDuration() / a._timeScale / e) > b + m ? 0 : f - b - m;
                                    }; h._init = function () {
                                        var a,
                                            b,
                                            c,
                                            d,
                                            e,
                                            f,
                                            g = this.vars,
                                            h = this._overwrittenProps,
                                            i = this._duration,
                                            j = !!g.immediateRender,
                                            k = g.ease; if (g.startAt) {
                                                this._startAt && (this._startAt.render(-1, !0), this._startAt.kill()), e = {}; for (d in g.startAt) {
                                                    e[d] = g.startAt[d];
                                                } if (e.overwrite = !1, e.immediateRender = !0, e.lazy = j && g.lazy !== !1, e.startAt = e.delay = null, this._startAt = G.to(this.target, 0, e), j) if (this._time > 0) this._startAt = null; else if (0 !== i) return;
                                            } else if (g.runBackwards && 0 !== i) if (this._startAt) this._startAt.render(-1, !0), this._startAt.kill(), this._startAt = null; else {
                                                0 !== this._time && (j = !1), c = {}; for (d in g) {
                                                    T[d] && "autoCSS" !== d || (c[d] = g[d]);
                                                } if (c.overwrite = 0, c.data = "isFromStart", c.lazy = j && g.lazy !== !1, c.immediateRender = j, this._startAt = G.to(this.target, 0, c), j) {
                                                    if (0 === this._time) return;
                                                } else this._startAt._init(), this._startAt._enabled(!1), this.vars.immediateRender && (this._startAt = null);
                                            } if (this._ease = k = k ? k instanceof v ? k : "function" == typeof k ? new v(k, g.easeParams) : w[k] || G.defaultEase : G.defaultEase, g.easeParams instanceof Array && k.config && (this._ease = k.config.apply(k, g.easeParams)), this._easeType = this._ease._type, this._easePower = this._ease._power, this._firstPT = null, this._targets) for (f = this._targets.length, a = 0; f > a; a++) {
                                                this._initProps(this._targets[a], this._propLookup[a] = {}, this._siblings[a], h ? h[a] : null, a) && (b = !0);
                                            } else b = this._initProps(this.target, this._propLookup, this._siblings, h, 0); if (b && G._onPluginEvent("_onInitAllProps", this), h && (this._firstPT || "function" != typeof this.target && this._enabled(!1, !1)), g.runBackwards) for (c = this._firstPT; c;) {
                                                c.s += c.c, c.c = -c.c, c = c._next;
                                            } this._onUpdate = g.onUpdate, this._initted = !0;
                                    }, h._initProps = function (b, c, d, e, f) {
                                        var g, h, i, j, k, l; if (null == b) return !1; K[b._gsTweenID] && Y(), this.vars.css || b.style && b !== a && b.nodeType && Q.css && this.vars.autoCSS !== !1 && I(this.vars, b); for (g in this.vars) {
                                            if (l = this.vars[g], T[g]) l && (l instanceof Array || l.push && p(l)) && -1 !== l.join("").indexOf("{self}") && (this.vars[g] = l = this._swapSelfInParams(l, this)); else if (Q[g] && (j = new Q[g]())._onInitTween(b, this.vars[g], this, f)) {
                                                for (this._firstPT = k = { _next: this._firstPT, t: j, p: "setRatio", s: 0, c: 1, f: 1, n: g, pg: 1, pr: j._priority, m: 0 }, h = j._overwriteProps.length; --h > -1;) {
                                                    c[j._overwriteProps[h]] = this._firstPT;
                                                } (j._priority || j._onInitAllProps) && (i = !0), (j._onDisable || j._onEnable) && (this._notifyPluginsOfEnabled = !0), k._next && (k._next._prev = k);
                                            } else c[g] = O.call(this, b, g, "get", l, g, 0, null, this.vars.stringFilter, f);
                                        } return e && this._kill(e, b) ? this._initProps(b, c, d, e, f) : this._overwrite > 1 && this._firstPT && d.length > 1 && _(b, this, c, this._overwrite, d) ? (this._kill(c, b), this._initProps(b, c, d, e, f)) : (this._firstPT && (this.vars.lazy !== !1 && this._duration || this.vars.lazy && !this._duration) && (K[b._gsTweenID] = !0), i);
                                    }, h.render = function (a, b, c) {
                                        var d,
                                            e,
                                            f,
                                            g,
                                            h = this._time,
                                            i = this._duration,
                                            j = this._rawPrevTime; if (a >= i - 1e-7 && a >= 0) this._totalTime = this._time = i, this.ratio = this._ease._calcEnd ? this._ease.getRatio(1) : 1, this._reversed || (d = !0, e = "onComplete", c = c || this._timeline.autoRemoveChildren), 0 === i && (this._initted || !this.vars.lazy || c) && (this._startTime === this._timeline._duration && (a = 0), (0 > j || 0 >= a && a >= -1e-7 || j === m && "isPause" !== this.data) && j !== a && (c = !0, j > m && (e = "onReverseComplete")), this._rawPrevTime = g = !b || a || j === a ? a : m); else if (1e-7 > a) this._totalTime = this._time = 0, this.ratio = this._ease._calcEnd ? this._ease.getRatio(0) : 0, (0 !== h || 0 === i && j > 0) && (e = "onReverseComplete", d = this._reversed), 0 > a && (this._active = !1, 0 === i && (this._initted || !this.vars.lazy || c) && (j >= 0 && (j !== m || "isPause" !== this.data) && (c = !0), this._rawPrevTime = g = !b || a || j === a ? a : m)), this._initted || (c = !0); else if (this._totalTime = this._time = a, this._easeType) {
                                                var k = a / i,
                                                    l = this._easeType,
                                                    n = this._easePower; (1 === l || 3 === l && k >= .5) && (k = 1 - k), 3 === l && (k *= 2), 1 === n ? k *= k : 2 === n ? k *= k * k : 3 === n ? k *= k * k * k : 4 === n && (k *= k * k * k * k), 1 === l ? this.ratio = 1 - k : 2 === l ? this.ratio = k : .5 > a / i ? this.ratio = k / 2 : this.ratio = 1 - k / 2;
                                            } else this.ratio = this._ease.getRatio(a / i); if (this._time !== h || c) {
                                                if (!this._initted) {
                                                    if (this._init(), !this._initted || this._gc) return; if (!c && this._firstPT && (this.vars.lazy !== !1 && this._duration || this.vars.lazy && !this._duration)) return this._time = this._totalTime = h, this._rawPrevTime = j, J.push(this), void (this._lazy = [a, b]); this._time && !d ? this.ratio = this._ease.getRatio(this._time / i) : d && this._ease._calcEnd && (this.ratio = this._ease.getRatio(0 === this._time ? 0 : 1));
                                                } for (this._lazy !== !1 && (this._lazy = !1), this._active || !this._paused && this._time !== h && a >= 0 && (this._active = !0), 0 === h && (this._startAt && (a >= 0 ? this._startAt.render(a, b, c) : e || (e = "_dummyGS")), this.vars.onStart && (0 !== this._time || 0 === i) && (b || this._callback("onStart"))), f = this._firstPT; f;) {
                                                    f.f ? f.t[f.p](f.c * this.ratio + f.s) : f.t[f.p] = f.c * this.ratio + f.s, f = f._next;
                                                } this._onUpdate && (0 > a && this._startAt && a !== -1e-4 && this._startAt.render(a, b, c), b || (this._time !== h || d || c) && this._callback("onUpdate")), e && (!this._gc || c) && (0 > a && this._startAt && !this._onUpdate && a !== -1e-4 && this._startAt.render(a, b, c), d && (this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !b && this.vars[e] && this._callback(e), 0 === i && this._rawPrevTime === m && g !== m && (this._rawPrevTime = 0));
                                            }
                                    }, h._kill = function (a, b, c) {
                                        if ("all" === a && (a = null), null == a && (null == b || b === this.target)) return this._lazy = !1, this._enabled(!1, !1); b = "string" != typeof b ? b || this._targets || this.target : G.selector(b) || b; var d,
                                            e,
                                            f,
                                            g,
                                            h,
                                            i,
                                            j,
                                            k,
                                            l,
                                            m = c && this._time && c._startTime === this._startTime && this._timeline === c._timeline; if ((p(b) || H(b)) && "number" != typeof b[0]) for (d = b.length; --d > -1;) {
                                                this._kill(a, b[d], c) && (i = !0);
                                            } else {
                                                if (this._targets) {
                                                    for (d = this._targets.length; --d > -1;) {
                                                        if (b === this._targets[d]) {
                                                            h = this._propLookup[d] || {}, this._overwrittenProps = this._overwrittenProps || [], e = this._overwrittenProps[d] = a ? this._overwrittenProps[d] || {} : "all"; break;
                                                        }
                                                    }
                                                } else {
                                                    if (b !== this.target) return !1; h = this._propLookup, e = this._overwrittenProps = a ? this._overwrittenProps || {} : "all";
                                                } if (h) {
                                                    if (j = a || h, k = a !== e && "all" !== e && a !== h && ("object" != (typeof a === "undefined" ? "undefined" : _typeof(a)) || !a._tempKill), c && (G.onOverwrite || this.vars.onOverwrite)) {
                                                        for (f in j) {
                                                            h[f] && (l || (l = []), l.push(f));
                                                        } if ((l || !a) && !$(this, c, b, l)) return !1;
                                                    } for (f in j) {
                                                        (g = h[f]) && (m && (g.f ? g.t[g.p](g.s) : g.t[g.p] = g.s, i = !0), g.pg && g.t._kill(j) && (i = !0), g.pg && 0 !== g.t._overwriteProps.length || (g._prev ? g._prev._next = g._next : g === this._firstPT && (this._firstPT = g._next), g._next && (g._next._prev = g._prev), g._next = g._prev = null), delete h[f]), k && (e[f] = 1);
                                                    } !this._firstPT && this._initted && this._enabled(!1, !1);
                                                }
                                            } return i;
                                    }, h.invalidate = function () {
                                        return this._notifyPluginsOfEnabled && G._onPluginEvent("_onDisable", this), this._firstPT = this._overwrittenProps = this._startAt = this._onUpdate = null, this._notifyPluginsOfEnabled = this._active = this._lazy = !1, this._propLookup = this._targets ? {} : [], D.prototype.invalidate.call(this), this.vars.immediateRender && (this._time = -m, this.render(Math.min(0, -this._delay))), this;
                                    }, h._enabled = function (a, b) {
                                        if (j || i.wake(), a && this._gc) {
                                            var c,
                                                d = this._targets; if (d) for (c = d.length; --c > -1;) {
                                                    this._siblings[c] = Z(d[c], this, !0);
                                                } else this._siblings = Z(this.target, this, !0);
                                        } return D.prototype._enabled.call(this, a, b), this._notifyPluginsOfEnabled && this._firstPT ? G._onPluginEvent(a ? "_onEnable" : "_onDisable", this) : !1;
                                    }, G.to = function (a, b, c) {
                                        return new G(a, b, c);
                                    }, G.from = function (a, b, c) {
                                        return c.runBackwards = !0, c.immediateRender = 0 != c.immediateRender, new G(a, b, c);
                                    }, G.fromTo = function (a, b, c, d) {
                                        return d.startAt = c, d.immediateRender = 0 != d.immediateRender && 0 != c.immediateRender, new G(a, b, d);
                                    }, G.delayedCall = function (a, b, c, d, e) {
                                        return new G(b, 0, { delay: a, onComplete: b, onCompleteParams: c, callbackScope: d, onReverseComplete: b, onReverseCompleteParams: c, immediateRender: !1, lazy: !1, useFrames: e, overwrite: 0 });
                                    }, G.set = function (a, b) {
                                        return new G(a, 0, b);
                                    }, G.getTweensOf = function (a, b) {
                                        if (null == a) return []; a = "string" != typeof a ? a : G.selector(a) || a; var c, d, e, f; if ((p(a) || H(a)) && "number" != typeof a[0]) {
                                            for (c = a.length, d = []; --c > -1;) {
                                                d = d.concat(G.getTweensOf(a[c], b));
                                            } for (c = d.length; --c > -1;) {
                                                for (f = d[c], e = c; --e > -1;) {
                                                    f === d[e] && d.splice(c, 1);
                                                }
                                            }
                                        } else for (d = Z(a).concat(), c = d.length; --c > -1;) {
                                            (d[c]._gc || b && !d[c].isActive()) && d.splice(c, 1);
                                        } return d;
                                    }, G.killTweensOf = G.killDelayedCallsTo = function (a, b, c) {
                                        "object" == (typeof b === "undefined" ? "undefined" : _typeof(b)) && (c = b, b = !1); for (var d = G.getTweensOf(a, b), e = d.length; --e > -1;) {
                                            d[e]._kill(c, a);
                                        }
                                    }; var ba = t("plugins.TweenPlugin", function (a, b) {
                                        this._overwriteProps = (a || "").split(","), this._propName = this._overwriteProps[0], this._priority = b || 0, this._super = ba.prototype;
                                    }, !0); if (h = ba.prototype, ba.version = "1.19.0", ba.API = 2, h._firstPT = null, h._addTween = O, h.setRatio = M, h._kill = function (a) {
                                      var b,
                                          c = this._overwriteProps,
                                          d = this._firstPT; if (null != a[this._propName]) this._overwriteProps = []; else for (b = c.length; --b > -1;) {
                                        null != a[c[b]] && c.splice(b, 1);
                                    } for (; d;) {
                                        null != a[d.n] && (d._next && (d._next._prev = d._prev), d._prev ? (d._prev._next = d._next, d._prev = null) : this._firstPT === d && (this._firstPT = d._next)), d = d._next;
                                    } return !1;
                                    }, h._mod = h._roundProps = function (a) {
                                      for (var b, c = this._firstPT; c;) {
                                        b = a[this._propName] || null != c.n && a[c.n.split(this._propName + "_").join("")], b && "function" == typeof b && (2 === c.f ? c.t._applyPT.m = b : c.m = b), c = c._next;
                                    }
                                    }, G._onPluginEvent = function (a, b) {
                                      var c,
                                          d,
                                          e,
                                          f,
                                          g,
                                          h = b._firstPT; if ("_onInitAllProps" === a) {
                                        for (; h;) {
                                          for (g = h._next, d = e; d && d.pr > h.pr;) {
                                            d = d._next;
                                    } (h._prev = d ? d._prev : f) ? h._prev._next = h : e = h, (h._next = d) ? d._prev = h : f = h, h = g;
                                    } h = b._firstPT = e;
                                    } for (; h;) {
                                        h.pg && "function" == typeof h.t[a] && h.t[a]() && (c = !0), h = h._next;
                                    } return c;
                                    }, ba.activate = function (a) {
                                      for (var b = a.length; --b > -1;) {
                                        a[b].API === ba.API && (Q[new a[b]()._propName] = a[b]);
                                    } return !0;
                                    }, s.plugin = function (a) {
                                      if (!(a && a.propName && a.init && a.API)) throw "illegal plugin definition."; var b,
                                          c = a.propName,
                                          d = a.priority || 0,
                                          e = a.overwriteProps,
                                          f = { init: "_onInitTween", set: "setRatio", kill: "_kill", round: "_mod", mod: "_mod", initAll: "_onInitAllProps" },
                                          g = t("plugins." + c.charAt(0).toUpperCase() + c.substr(1) + "Plugin", function () {
                                        ba.call(this, c, d), this._overwriteProps = e || [];
                                    }, a.global === !0),
                                          h = g.prototype = new ba(c); h.constructor = g, g.API = a.API; for (b in f) {
                                        "function" == typeof a[b] && (h[f[b]] = a[b]);
                                    } return g.version = a.version, ba.activate([g]), g;
                                    }, f = a._gsQueue) {
                                        for (g = 0; g < f.length; g++) {
                                            f[g]();
                                        } for (h in q) {
                                            q[h].func || a.console.log("GSAP encountered missing dependency: " + h);
                                        }
                                    } j = !1;
        }
}("undefined" != typeof module && module.exports && "undefined" != typeof global ? global : undefined || window, "TweenMax");
"use strict";

var _typeof = typeof Symbol === "function" && typeof Symbol.iterator === "symbol" ? function (obj) { return typeof obj; } : function (obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; };

/*!
 * jQuery JavaScript Library v3.1.1
 * https://jquery.com/
 *
 * Includes Sizzle.js
 * https://sizzlejs.com/
 *
 * Copyright jQuery Foundation and other contributors
 * Released under the MIT license
 * https://jquery.org/license
 *
 * Date: 2016-09-22T22:30Z
 */
(function (global, factory) {

    "use strict";

    if ((typeof module === "undefined" ? "undefined" : _typeof(module)) === "object" && _typeof(module.exports) === "object") {

        // For CommonJS and CommonJS-like environments where a proper `window`
        // is present, execute the factory and get jQuery.
        // For environments that do not have a `window` with a `document`
        // (such as Node.js), expose a factory as module.exports.
        // This accentuates the need for the creation of a real `window`.
        // e.g. var jQuery = require("jquery")(window);
        // See ticket #14549 for more info.
        module.exports = global.document ? factory(global, true) : function (w) {
            if (!w.document) {
                throw new Error("jQuery requires a window with a document");
            }
            return factory(w);
        };
    } else {
        factory(global);
    }

    // Pass this if window is not defined yet
})(typeof window !== "undefined" ? window : undefined, function (window, noGlobal) {

    // Edge <= 12 - 13+, Firefox <=18 - 45+, IE 10 - 11, Safari 5.1 - 9+, iOS 6 - 9.1
    // throw exceptions when non-strict code (e.g., ASP.NET 4.5) accesses strict mode
    // arguments.callee.caller (trac-13335). But as of jQuery 3.0 (2016), strict mode should be common
    // enough that all such attempts are guarded in a try block.
    "use strict";

    var arr = [];

    var document = window.document;

    var getProto = Object.getPrototypeOf;

    var _slice = arr.slice;

    var concat = arr.concat;

    var push = arr.push;

    var indexOf = arr.indexOf;

    var class2type = {};

    var toString = class2type.toString;

    var hasOwn = class2type.hasOwnProperty;

    var fnToString = hasOwn.toString;

    var ObjectFunctionString = fnToString.call(Object);

    var support = {};

    function DOMEval(code, doc) {
        doc = doc || document;

        var script = doc.createElement("script");

        script.text = code;
        doc.head.appendChild(script).parentNode.removeChild(script);
    }
    /* global Symbol */
    // Defining this global in .eslintrc.json would create a danger of using the global
    // unguarded in another place, it seems safer to define global only for this module


    var version = "3.1.1",


	// Define a local copy of jQuery
	jQuery = function jQuery(selector, context) {

	    // The jQuery object is actually just the init constructor 'enhanced'
	    // Need init if jQuery is called (just allow error to be thrown if not included)
	    return new jQuery.fn.init(selector, context);
	},


	// Support: Android <=4.0 only
	// Make sure we trim BOM and NBSP
	rtrim = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g,


	// Matches dashed string for camelizing
	rmsPrefix = /^-ms-/,
	    rdashAlpha = /-([a-z])/g,


	// Used by jQuery.camelCase as callback to replace()
	fcamelCase = function fcamelCase(all, letter) {
	    return letter.toUpperCase();
	};

    jQuery.fn = jQuery.prototype = {

        // The current version of jQuery being used
        jquery: version,

        constructor: jQuery,

        // The default length of a jQuery object is 0
        length: 0,

        toArray: function toArray() {
            return _slice.call(this);
        },

        // Get the Nth element in the matched element set OR
        // Get the whole matched element set as a clean array
        get: function get(num) {

            // Return all the elements in a clean array
            if (num == null) {
                return _slice.call(this);
            }

            // Return just the one element from the set
            return num < 0 ? this[num + this.length] : this[num];
        },

        // Take an array of elements and push it onto the stack
        // (returning the new matched element set)
        pushStack: function pushStack(elems) {

            // Build a new jQuery matched element set
            var ret = jQuery.merge(this.constructor(), elems);

            // Add the old object onto the stack (as a reference)
            ret.prevObject = this;

            // Return the newly-formed element set
            return ret;
        },

        // Execute a callback for every element in the matched set.
        each: function each(callback) {
            return jQuery.each(this, callback);
        },

        map: function map(callback) {
            return this.pushStack(jQuery.map(this, function (elem, i) {
                return callback.call(elem, i, elem);
            }));
        },

        slice: function slice() {
            return this.pushStack(_slice.apply(this, arguments));
        },

        first: function first() {
            return this.eq(0);
        },

        last: function last() {
            return this.eq(-1);
        },

        eq: function eq(i) {
            var len = this.length,
			    j = +i + (i < 0 ? len : 0);
            return this.pushStack(j >= 0 && j < len ? [this[j]] : []);
        },

        end: function end() {
            return this.prevObject || this.constructor();
        },

        // For internal use only.
        // Behaves like an Array's method, not like a jQuery method.
        push: push,
        sort: arr.sort,
        splice: arr.splice
    };

    jQuery.extend = jQuery.fn.extend = function () {
        var options,
		    name,
		    src,
		    copy,
		    copyIsArray,
		    clone,
		    target = arguments[0] || {},
		    i = 1,
		    length = arguments.length,
		    deep = false;

        // Handle a deep copy situation
        if (typeof target === "boolean") {
            deep = target;

            // Skip the boolean and the target
            target = arguments[i] || {};
            i++;
        }

        // Handle case when target is a string or something (possible in deep copy)
        if ((typeof target === "undefined" ? "undefined" : _typeof(target)) !== "object" && !jQuery.isFunction(target)) {
            target = {};
        }

        // Extend jQuery itself if only one argument is passed
        if (i === length) {
            target = this;
            i--;
        }

        for (; i < length; i++) {

            // Only deal with non-null/undefined values
            if ((options = arguments[i]) != null) {

                // Extend the base object
                for (name in options) {
                    src = target[name];
                    copy = options[name];

                    // Prevent never-ending loop
                    if (target === copy) {
                        continue;
                    }

                    // Recurse if we're merging plain objects or arrays
                    if (deep && copy && (jQuery.isPlainObject(copy) || (copyIsArray = jQuery.isArray(copy)))) {

                        if (copyIsArray) {
                            copyIsArray = false;
                            clone = src && jQuery.isArray(src) ? src : [];
                        } else {
                            clone = src && jQuery.isPlainObject(src) ? src : {};
                        }

                        // Never move original objects, clone them
                        target[name] = jQuery.extend(deep, clone, copy);

                        // Don't bring in undefined values
                    } else if (copy !== undefined) {
                        target[name] = copy;
                    }
                }
            }
        }

        // Return the modified object
        return target;
    };

    jQuery.extend({

        // Unique for each copy of jQuery on the page
        expando: "jQuery" + (version + Math.random()).replace(/\D/g, ""),

        // Assume jQuery is ready without the ready module
        isReady: true,

        error: function error(msg) {
            throw new Error(msg);
        },

        noop: function noop() { },

        isFunction: function isFunction(obj) {
            return jQuery.type(obj) === "function";
        },

        isArray: Array.isArray,

        isWindow: function isWindow(obj) {
            return obj != null && obj === obj.window;
        },

        isNumeric: function isNumeric(obj) {

            // As of jQuery 3.0, isNumeric is limited to
            // strings and numbers (primitives or objects)
            // that can be coerced to finite numbers (gh-2662)
            var type = jQuery.type(obj);
            return (type === "number" || type === "string") &&

			// parseFloat NaNs numeric-cast false positives ("")
			// ...but misinterprets leading-number strings, particularly hex literals ("0x...")
			// subtraction forces infinities to NaN
			!isNaN(obj - parseFloat(obj));
        },

        isPlainObject: function isPlainObject(obj) {
            var proto, Ctor;

            // Detect obvious negatives
            // Use toString instead of jQuery.type to catch host objects
            if (!obj || toString.call(obj) !== "[object Object]") {
                return false;
            }

            proto = getProto(obj);

            // Objects with no prototype (e.g., `Object.create( null )`) are plain
            if (!proto) {
                return true;
            }

            // Objects with prototype are plain iff they were constructed by a global Object function
            Ctor = hasOwn.call(proto, "constructor") && proto.constructor;
            return typeof Ctor === "function" && fnToString.call(Ctor) === ObjectFunctionString;
        },

        isEmptyObject: function isEmptyObject(obj) {

            /* eslint-disable no-unused-vars */
            // See https://github.com/eslint/eslint/issues/6125
            var name;

            for (name in obj) {
                return false;
            }
            return true;
        },

        type: function type(obj) {
            if (obj == null) {
                return obj + "";
            }

            // Support: Android <=2.3 only (functionish RegExp)
            return (typeof obj === "undefined" ? "undefined" : _typeof(obj)) === "object" || typeof obj === "function" ? class2type[toString.call(obj)] || "object" : typeof obj === "undefined" ? "undefined" : _typeof(obj);
        },

        // Evaluates a script in a global context
        globalEval: function globalEval(code) {
            DOMEval(code);
        },

        // Convert dashed to camelCase; used by the css and data modules
        // Support: IE <=9 - 11, Edge 12 - 13
        // Microsoft forgot to hump their vendor prefix (#9572)
        camelCase: function camelCase(string) {
            return string.replace(rmsPrefix, "ms-").replace(rdashAlpha, fcamelCase);
        },

        nodeName: function nodeName(elem, name) {
            return elem.nodeName && elem.nodeName.toLowerCase() === name.toLowerCase();
        },

        each: function each(obj, callback) {
            var length,
			    i = 0;

            if (isArrayLike(obj)) {
                length = obj.length;
                for (; i < length; i++) {
                    if (callback.call(obj[i], i, obj[i]) === false) {
                        break;
                    }
                }
            } else {
                for (i in obj) {
                    if (callback.call(obj[i], i, obj[i]) === false) {
                        break;
                    }
                }
            }

            return obj;
        },

        // Support: Android <=4.0 only
        trim: function trim(text) {
            return text == null ? "" : (text + "").replace(rtrim, "");
        },

        // results is for internal usage only
        makeArray: function makeArray(arr, results) {
            var ret = results || [];

            if (arr != null) {
                if (isArrayLike(Object(arr))) {
                    jQuery.merge(ret, typeof arr === "string" ? [arr] : arr);
                } else {
                    push.call(ret, arr);
                }
            }

            return ret;
        },

        inArray: function inArray(elem, arr, i) {
            return arr == null ? -1 : indexOf.call(arr, elem, i);
        },

        // Support: Android <=4.0 only, PhantomJS 1 only
        // push.apply(_, arraylike) throws on ancient WebKit
        merge: function merge(first, second) {
            var len = +second.length,
			    j = 0,
			    i = first.length;

            for (; j < len; j++) {
                first[i++] = second[j];
            }

            first.length = i;

            return first;
        },

        grep: function grep(elems, callback, invert) {
            var callbackInverse,
			    matches = [],
			    i = 0,
			    length = elems.length,
			    callbackExpect = !invert;

            // Go through the array, only saving the items
            // that pass the validator function
            for (; i < length; i++) {
                callbackInverse = !callback(elems[i], i);
                if (callbackInverse !== callbackExpect) {
                    matches.push(elems[i]);
                }
            }

            return matches;
        },

        // arg is for internal usage only
        map: function map(elems, callback, arg) {
            var length,
			    value,
			    i = 0,
			    ret = [];

            // Go through the array, translating each of the items to their new values
            if (isArrayLike(elems)) {
                length = elems.length;
                for (; i < length; i++) {
                    value = callback(elems[i], i, arg);

                    if (value != null) {
                        ret.push(value);
                    }
                }

                // Go through every key on the object,
            } else {
                for (i in elems) {
                    value = callback(elems[i], i, arg);

                    if (value != null) {
                        ret.push(value);
                    }
                }
            }

            // Flatten any nested arrays
            return concat.apply([], ret);
        },

        // A global GUID counter for objects
        guid: 1,

        // Bind a function to a context, optionally partially applying any
        // arguments.
        proxy: function proxy(fn, context) {
            var tmp, args, proxy;

            if (typeof context === "string") {
                tmp = fn[context];
                context = fn;
                fn = tmp;
            }

            // Quick check to determine if target is callable, in the spec
            // this throws a TypeError, but we will just return undefined.
            if (!jQuery.isFunction(fn)) {
                return undefined;
            }

            // Simulated bind
            args = _slice.call(arguments, 2);
            proxy = function proxy() {
                return fn.apply(context || this, args.concat(_slice.call(arguments)));
            };

            // Set the guid of unique handler to the same of original handler, so it can be removed
            proxy.guid = fn.guid = fn.guid || jQuery.guid++;

            return proxy;
        },

        now: Date.now,

        // jQuery.support is not used in Core but other projects attach their
        // properties to it so it needs to exist.
        support: support
    });

    if (typeof Symbol === "function") {
        jQuery.fn[Symbol.iterator] = arr[Symbol.iterator];
    }

    // Populate the class2type map
    jQuery.each("Boolean Number String Function Array Date RegExp Object Error Symbol".split(" "), function (i, name) {
        class2type["[object " + name + "]"] = name.toLowerCase();
    });

    function isArrayLike(obj) {

        // Support: real iOS 8.2 only (not reproducible in simulator)
        // `in` check used to prevent JIT error (gh-2145)
        // hasOwn isn't used here due to false negatives
        // regarding Nodelist length in IE
        var length = !!obj && "length" in obj && obj.length,
		    type = jQuery.type(obj);

        if (type === "function" || jQuery.isWindow(obj)) {
            return false;
        }

        return type === "array" || length === 0 || typeof length === "number" && length > 0 && length - 1 in obj;
    }
    var Sizzle =
	/*!
  * Sizzle CSS Selector Engine v2.3.3
  * https://sizzlejs.com/
  *
  * Copyright jQuery Foundation and other contributors
  * Released under the MIT license
  * http://jquery.org/license
  *
  * Date: 2016-08-08
  */
	function (window) {

	    var i,
		    support,
		    Expr,
		    getText,
		    isXML,
		    tokenize,
		    compile,
		    select,
		    outermostContext,
		    sortInput,
		    hasDuplicate,


		// Local document vars
		setDocument,
		    document,
		    docElem,
		    documentIsHTML,
		    rbuggyQSA,
		    rbuggyMatches,
		    matches,
		    contains,


		// Instance-specific data
		expando = "sizzle" + 1 * new Date(),
		    preferredDoc = window.document,
		    dirruns = 0,
		    done = 0,
		    classCache = createCache(),
		    tokenCache = createCache(),
		    compilerCache = createCache(),
		    sortOrder = function sortOrder(a, b) {
		        if (a === b) {
		            hasDuplicate = true;
		        }
		        return 0;
		    },


		// Instance methods
		hasOwn = {}.hasOwnProperty,
		    arr = [],
		    pop = arr.pop,
		    push_native = arr.push,
		    push = arr.push,
		    slice = arr.slice,

		// Use a stripped-down indexOf as it's faster than native
		// https://jsperf.com/thor-indexof-vs-for/5
		indexOf = function indexOf(list, elem) {
		    var i = 0,
			    len = list.length;
		    for (; i < len; i++) {
		        if (list[i] === elem) {
		            return i;
		        }
		    }
		    return -1;
		},
		    booleans = "checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped",


		// Regular expressions

		// http://www.w3.org/TR/css3-selectors/#whitespace
		whitespace = "[\\x20\\t\\r\\n\\f]",


		// http://www.w3.org/TR/CSS21/syndata.html#value-def-identifier
		identifier = "(?:\\\\.|[\\w-]|[^\0-\\xa0])+",


		// Attribute selectors: http://www.w3.org/TR/selectors/#attribute-selectors
		attributes = "\\[" + whitespace + "*(" + identifier + ")(?:" + whitespace +
		// Operator (capture 2)
		"*([*^$|!~]?=)" + whitespace +
		// "Attribute values must be CSS identifiers [capture 5] or strings [capture 3 or capture 4]"
		"*(?:'((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\"|(" + identifier + "))|)" + whitespace + "*\\]",
		    pseudos = ":(" + identifier + ")(?:\\((" +
		// To reduce the number of selectors needing tokenize in the preFilter, prefer arguments:
		// 1. quoted (capture 3; capture 4 or capture 5)
		"('((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\")|" +
		// 2. simple (capture 6)
		"((?:\\\\.|[^\\\\()[\\]]|" + attributes + ")*)|" +
		// 3. anything else (capture 2)
		".*" + ")\\)|)",


		// Leading and non-escaped trailing whitespace, capturing some non-whitespace characters preceding the latter
		rwhitespace = new RegExp(whitespace + "+", "g"),
		    rtrim = new RegExp("^" + whitespace + "+|((?:^|[^\\\\])(?:\\\\.)*)" + whitespace + "+$", "g"),
		    rcomma = new RegExp("^" + whitespace + "*," + whitespace + "*"),
		    rcombinators = new RegExp("^" + whitespace + "*([>+~]|" + whitespace + ")" + whitespace + "*"),
		    rattributeQuotes = new RegExp("=" + whitespace + "*([^\\]'\"]*?)" + whitespace + "*\\]", "g"),
		    rpseudo = new RegExp(pseudos),
		    ridentifier = new RegExp("^" + identifier + "$"),
		    matchExpr = {
		        "ID": new RegExp("^#(" + identifier + ")"),
		        "CLASS": new RegExp("^\\.(" + identifier + ")"),
		        "TAG": new RegExp("^(" + identifier + "|[*])"),
		        "ATTR": new RegExp("^" + attributes),
		        "PSEUDO": new RegExp("^" + pseudos),
		        "CHILD": new RegExp("^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\(" + whitespace + "*(even|odd|(([+-]|)(\\d*)n|)" + whitespace + "*(?:([+-]|)" + whitespace + "*(\\d+)|))" + whitespace + "*\\)|)", "i"),
		        "bool": new RegExp("^(?:" + booleans + ")$", "i"),
		        // For use in libraries implementing .is()
		        // We use this for POS matching in `select`
		        "needsContext": new RegExp("^" + whitespace + "*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\(" + whitespace + "*((?:-\\d)?\\d*)" + whitespace + "*\\)|)(?=[^-]|$)", "i")
		    },
		    rinputs = /^(?:input|select|textarea|button)$/i,
		    rheader = /^h\d$/i,
		    rnative = /^[^{]+\{\s*\[native \w/,


		// Easily-parseable/retrievable ID or TAG or CLASS selectors
		rquickExpr = /^(?:#([\w-]+)|(\w+)|\.([\w-]+))$/,
		    rsibling = /[+~]/,


		// CSS escapes
		// http://www.w3.org/TR/CSS21/syndata.html#escaped-characters
		runescape = new RegExp("\\\\([\\da-f]{1,6}" + whitespace + "?|(" + whitespace + ")|.)", "ig"),
		    funescape = function funescape(_, escaped, escapedWhitespace) {
		        var high = "0x" + escaped - 0x10000;
		        // NaN means non-codepoint
		        // Support: Firefox<24
		        // Workaround erroneous numeric interpretation of +"0x"
		        return high !== high || escapedWhitespace ? escaped : high < 0 ?
                // BMP codepoint
                String.fromCharCode(high + 0x10000) :
                // Supplemental Plane codepoint (surrogate pair)
                String.fromCharCode(high >> 10 | 0xD800, high & 0x3FF | 0xDC00);
		    },


		// CSS string/identifier serialization
		// https://drafts.csswg.org/cssom/#common-serializing-idioms
		rcssescape = /([\0-\x1f\x7f]|^-?\d)|^-$|[^\0-\x1f\x7f-\uFFFF\w-]/g,
		    fcssescape = function fcssescape(ch, asCodePoint) {
		        if (asCodePoint) {

		            // U+0000 NULL becomes U+FFFD REPLACEMENT CHARACTER
		            if (ch === "\0") {
		                return "\uFFFD";
		            }

		            // Control characters and (dependent upon position) numbers get escaped as code points
		            return ch.slice(0, -1) + "\\" + ch.charCodeAt(ch.length - 1).toString(16) + " ";
		        }

		        // Other potentially-special ASCII characters get backslash-escaped
		        return "\\" + ch;
		    },


		// Used for iframes
		// See setDocument()
		// Removing the function wrapper causes a "Permission Denied"
		// error in IE
		unloadHandler = function unloadHandler() {
		    setDocument();
		},
		    disabledAncestor = addCombinator(function (elem) {
		        return elem.disabled === true && ("form" in elem || "label" in elem);
		    }, { dir: "parentNode", next: "legend" });

	    // Optimize for push.apply( _, NodeList )
	    try {
	        push.apply(arr = slice.call(preferredDoc.childNodes), preferredDoc.childNodes);
	        // Support: Android<4.0
	        // Detect silently failing push.apply
	        arr[preferredDoc.childNodes.length].nodeType;
	    } catch (e) {
	        push = {
	            apply: arr.length ?

                    // Leverage slice if possible
                    function (target, els) {
                        push_native.apply(target, slice.call(els));
                    } :

                    // Support: IE<9
                    // Otherwise append directly
                    function (target, els) {
                        var j = target.length,
                            i = 0;
                        // Can't trust NodeList.length
                        while (target[j++] = els[i++]) { }
                        target.length = j - 1;
                    }
	        };
	    }

	    function Sizzle(selector, context, results, seed) {
	        var m,
			    i,
			    elem,
			    nid,
			    match,
			    groups,
			    newSelector,
			    newContext = context && context.ownerDocument,


			// nodeType defaults to 9, since context defaults to document
			nodeType = context ? context.nodeType : 9;

	        results = results || [];

	        // Return early from calls with invalid selector or context
	        if (typeof selector !== "string" || !selector || nodeType !== 1 && nodeType !== 9 && nodeType !== 11) {

	            return results;
	        }

	        // Try to shortcut find operations (as opposed to filters) in HTML documents
	        if (!seed) {

	            if ((context ? context.ownerDocument || context : preferredDoc) !== document) {
	                setDocument(context);
	            }
	            context = context || document;

	            if (documentIsHTML) {

	                // If the selector is sufficiently simple, try using a "get*By*" DOM method
	                // (excepting DocumentFragment context, where the methods don't exist)
	                if (nodeType !== 11 && (match = rquickExpr.exec(selector))) {

	                    // ID selector
	                    if (m = match[1]) {

	                        // Document context
	                        if (nodeType === 9) {
	                            if (elem = context.getElementById(m)) {

	                                // Support: IE, Opera, Webkit
	                                // TODO: identify versions
	                                // getElementById can match elements by name instead of ID
	                                if (elem.id === m) {
	                                    results.push(elem);
	                                    return results;
	                                }
	                            } else {
	                                return results;
	                            }

	                            // Element context
	                        } else {

	                            // Support: IE, Opera, Webkit
	                            // TODO: identify versions
	                            // getElementById can match elements by name instead of ID
	                            if (newContext && (elem = newContext.getElementById(m)) && contains(context, elem) && elem.id === m) {

	                                results.push(elem);
	                                return results;
	                            }
	                        }

	                        // Type selector
	                    } else if (match[2]) {
	                        push.apply(results, context.getElementsByTagName(selector));
	                        return results;

	                        // Class selector
	                    } else if ((m = match[3]) && support.getElementsByClassName && context.getElementsByClassName) {

	                        push.apply(results, context.getElementsByClassName(m));
	                        return results;
	                    }
	                }

	                // Take advantage of querySelectorAll
	                if (support.qsa && !compilerCache[selector + " "] && (!rbuggyQSA || !rbuggyQSA.test(selector))) {

	                    if (nodeType !== 1) {
	                        newContext = context;
	                        newSelector = selector;

	                        // qSA looks outside Element context, which is not what we want
	                        // Thanks to Andrew Dupont for this workaround technique
	                        // Support: IE <=8
	                        // Exclude object elements
	                    } else if (context.nodeName.toLowerCase() !== "object") {

	                        // Capture the context ID, setting it first if necessary
	                        if (nid = context.getAttribute("id")) {
	                            nid = nid.replace(rcssescape, fcssescape);
	                        } else {
	                            context.setAttribute("id", nid = expando);
	                        }

	                        // Prefix every selector in the list
	                        groups = tokenize(selector);
	                        i = groups.length;
	                        while (i--) {
	                            groups[i] = "#" + nid + " " + toSelector(groups[i]);
	                        }
	                        newSelector = groups.join(",");

	                        // Expand context for sibling selectors
	                        newContext = rsibling.test(selector) && testContext(context.parentNode) || context;
	                    }

	                    if (newSelector) {
	                        try {
	                            push.apply(results, newContext.querySelectorAll(newSelector));
	                            return results;
	                        } catch (qsaError) { } finally {
	                            if (nid === expando) {
	                                context.removeAttribute("id");
	                            }
	                        }
	                    }
	                }
	            }
	        }

	        // All others
	        return select(selector.replace(rtrim, "$1"), context, results, seed);
	    }

	    /**
   * Create key-value caches of limited size
   * @returns {function(string, object)} Returns the Object data after storing it on itself with
   *	property name the (space-suffixed) string and (if the cache is larger than Expr.cacheLength)
   *	deleting the oldest entry
   */
	    function createCache() {
	        var keys = [];

	        function cache(key, value) {
	            // Use (key + " ") to avoid collision with native prototype properties (see Issue #157)
	            if (keys.push(key + " ") > Expr.cacheLength) {
	                // Only keep the most recent entries
	                delete cache[keys.shift()];
	            }
	            return cache[key + " "] = value;
	        }
	        return cache;
	    }

	    /**
   * Mark a function for special use by Sizzle
   * @param {Function} fn The function to mark
   */
	    function markFunction(fn) {
	        fn[expando] = true;
	        return fn;
	    }

	    /**
   * Support testing using an element
   * @param {Function} fn Passed the created element and returns a boolean result
   */
	    function assert(fn) {
	        var el = document.createElement("fieldset");

	        try {
	            return !!fn(el);
	        } catch (e) {
	            return false;
	        } finally {
	            // Remove from its parent by default
	            if (el.parentNode) {
	                el.parentNode.removeChild(el);
	            }
	            // release memory in IE
	            el = null;
	        }
	    }

	    /**
   * Adds the same handler for all of the specified attrs
   * @param {String} attrs Pipe-separated list of attributes
   * @param {Function} handler The method that will be applied
   */
	    function addHandle(attrs, handler) {
	        var arr = attrs.split("|"),
			    i = arr.length;

	        while (i--) {
	            Expr.attrHandle[arr[i]] = handler;
	        }
	    }

	    /**
   * Checks document order of two siblings
   * @param {Element} a
   * @param {Element} b
   * @returns {Number} Returns less than 0 if a precedes b, greater than 0 if a follows b
   */
	    function siblingCheck(a, b) {
	        var cur = b && a,
			    diff = cur && a.nodeType === 1 && b.nodeType === 1 && a.sourceIndex - b.sourceIndex;

	        // Use IE sourceIndex if available on both nodes
	        if (diff) {
	            return diff;
	        }

	        // Check if b follows a
	        if (cur) {
	            while (cur = cur.nextSibling) {
	                if (cur === b) {
	                    return -1;
	                }
	            }
	        }

	        return a ? 1 : -1;
	    }

	    /**
   * Returns a function to use in pseudos for input types
   * @param {String} type
   */
	    function createInputPseudo(type) {
	        return function (elem) {
	            var name = elem.nodeName.toLowerCase();
	            return name === "input" && elem.type === type;
	        };
	    }

	    /**
   * Returns a function to use in pseudos for buttons
   * @param {String} type
   */
	    function createButtonPseudo(type) {
	        return function (elem) {
	            var name = elem.nodeName.toLowerCase();
	            return (name === "input" || name === "button") && elem.type === type;
	        };
	    }

	    /**
   * Returns a function to use in pseudos for :enabled/:disabled
   * @param {Boolean} disabled true for :disabled; false for :enabled
   */
	    function createDisabledPseudo(disabled) {

	        // Known :disabled false positives: fieldset[disabled] > legend:nth-of-type(n+2) :can-disable
	        return function (elem) {

	            // Only certain elements can match :enabled or :disabled
	            // https://html.spec.whatwg.org/multipage/scripting.html#selector-enabled
	            // https://html.spec.whatwg.org/multipage/scripting.html#selector-disabled
	            if ("form" in elem) {

	                // Check for inherited disabledness on relevant non-disabled elements:
	                // * listed form-associated elements in a disabled fieldset
	                //   https://html.spec.whatwg.org/multipage/forms.html#category-listed
	                //   https://html.spec.whatwg.org/multipage/forms.html#concept-fe-disabled
	                // * option elements in a disabled optgroup
	                //   https://html.spec.whatwg.org/multipage/forms.html#concept-option-disabled
	                // All such elements have a "form" property.
	                if (elem.parentNode && elem.disabled === false) {

	                    // Option elements defer to a parent optgroup if present
	                    if ("label" in elem) {
	                        if ("label" in elem.parentNode) {
	                            return elem.parentNode.disabled === disabled;
	                        } else {
	                            return elem.disabled === disabled;
	                        }
	                    }

	                    // Support: IE 6 - 11
	                    // Use the isDisabled shortcut property to check for disabled fieldset ancestors
	                    return elem.isDisabled === disabled ||

						// Where there is no isDisabled, check manually
						/* jshint -W018 */
						elem.isDisabled !== !disabled && disabledAncestor(elem) === disabled;
	                }

	                return elem.disabled === disabled;

	                // Try to winnow out elements that can't be disabled before trusting the disabled property.
	                // Some victims get caught in our net (label, legend, menu, track), but it shouldn't
	                // even exist on them, let alone have a boolean value.
	            } else if ("label" in elem) {
	                return elem.disabled === disabled;
	            }

	            // Remaining elements are neither :enabled nor :disabled
	            return false;
	        };
	    }

	    /**
   * Returns a function to use in pseudos for positionals
   * @param {Function} fn
   */
	    function createPositionalPseudo(fn) {
	        return markFunction(function (argument) {
	            argument = +argument;
	            return markFunction(function (seed, matches) {
	                var j,
					    matchIndexes = fn([], seed.length, argument),
					    i = matchIndexes.length;

	                // Match elements found at the specified indexes
	                while (i--) {
	                    if (seed[j = matchIndexes[i]]) {
	                        seed[j] = !(matches[j] = seed[j]);
	                    }
	                }
	            });
	        });
	    }

	    /**
   * Checks a node for validity as a Sizzle context
   * @param {Element|Object=} context
   * @returns {Element|Object|Boolean} The input node if acceptable, otherwise a falsy value
   */
	    function testContext(context) {
	        return context && typeof context.getElementsByTagName !== "undefined" && context;
	    }

	    // Expose support vars for convenience
	    support = Sizzle.support = {};

	    /**
   * Detects XML nodes
   * @param {Element|Object} elem An element or a document
   * @returns {Boolean} True iff elem is a non-HTML XML node
   */
	    isXML = Sizzle.isXML = function (elem) {
	        // documentElement is verified for cases where it doesn't yet exist
	        // (such as loading iframes in IE - #4833)
	        var documentElement = elem && (elem.ownerDocument || elem).documentElement;
	        return documentElement ? documentElement.nodeName !== "HTML" : false;
	    };

	    /**
   * Sets document-related variables once based on the current document
   * @param {Element|Object} [doc] An element or document object to use to set the document
   * @returns {Object} Returns the current document
   */
	    setDocument = Sizzle.setDocument = function (node) {
	        var hasCompare,
			    subWindow,
			    doc = node ? node.ownerDocument || node : preferredDoc;

	        // Return early if doc is invalid or already selected
	        if (doc === document || doc.nodeType !== 9 || !doc.documentElement) {
	            return document;
	        }

	        // Update global variables
	        document = doc;
	        docElem = document.documentElement;
	        documentIsHTML = !isXML(document);

	        // Support: IE 9-11, Edge
	        // Accessing iframe documents after unload throws "permission denied" errors (jQuery #13936)
	        if (preferredDoc !== document && (subWindow = document.defaultView) && subWindow.top !== subWindow) {

	            // Support: IE 11, Edge
	            if (subWindow.addEventListener) {
	                subWindow.addEventListener("unload", unloadHandler, false);

	                // Support: IE 9 - 10 only
	            } else if (subWindow.attachEvent) {
	                subWindow.attachEvent("onunload", unloadHandler);
	            }
	        }

	        /* Attributes
   ---------------------------------------------------------------------- */

	        // Support: IE<8
	        // Verify that getAttribute really returns attributes and not properties
	        // (excepting IE8 booleans)
	        support.attributes = assert(function (el) {
	            el.className = "i";
	            return !el.getAttribute("className");
	        });

	        /* getElement(s)By*
   ---------------------------------------------------------------------- */

	        // Check if getElementsByTagName("*") returns only elements
	        support.getElementsByTagName = assert(function (el) {
	            el.appendChild(document.createComment(""));
	            return !el.getElementsByTagName("*").length;
	        });

	        // Support: IE<9
	        support.getElementsByClassName = rnative.test(document.getElementsByClassName);

	        // Support: IE<10
	        // Check if getElementById returns elements by name
	        // The broken getElementById methods don't pick up programmatically-set names,
	        // so use a roundabout getElementsByName test
	        support.getById = assert(function (el) {
	            docElem.appendChild(el).id = expando;
	            return !document.getElementsByName || !document.getElementsByName(expando).length;
	        });

	        // ID filter and find
	        if (support.getById) {
	            Expr.filter["ID"] = function (id) {
	                var attrId = id.replace(runescape, funescape);
	                return function (elem) {
	                    return elem.getAttribute("id") === attrId;
	                };
	            };
	            Expr.find["ID"] = function (id, context) {
	                if (typeof context.getElementById !== "undefined" && documentIsHTML) {
	                    var elem = context.getElementById(id);
	                    return elem ? [elem] : [];
	                }
	            };
	        } else {
	            Expr.filter["ID"] = function (id) {
	                var attrId = id.replace(runescape, funescape);
	                return function (elem) {
	                    var node = typeof elem.getAttributeNode !== "undefined" && elem.getAttributeNode("id");
	                    return node && node.value === attrId;
	                };
	            };

	            // Support: IE 6 - 7 only
	            // getElementById is not reliable as a find shortcut
	            Expr.find["ID"] = function (id, context) {
	                if (typeof context.getElementById !== "undefined" && documentIsHTML) {
	                    var node,
						    i,
						    elems,
						    elem = context.getElementById(id);

	                    if (elem) {

	                        // Verify the id attribute
	                        node = elem.getAttributeNode("id");
	                        if (node && node.value === id) {
	                            return [elem];
	                        }

	                        // Fall back on getElementsByName
	                        elems = context.getElementsByName(id);
	                        i = 0;
	                        while (elem = elems[i++]) {
	                            node = elem.getAttributeNode("id");
	                            if (node && node.value === id) {
	                                return [elem];
	                            }
	                        }
	                    }

	                    return [];
	                }
	            };
	        }

	        // Tag
	        Expr.find["TAG"] = support.getElementsByTagName ? function (tag, context) {
	            if (typeof context.getElementsByTagName !== "undefined") {
	                return context.getElementsByTagName(tag);

	                // DocumentFragment nodes don't have gEBTN
	            } else if (support.qsa) {
	                return context.querySelectorAll(tag);
	            }
	        } : function (tag, context) {
	            var elem,
				    tmp = [],
				    i = 0,

				// By happy coincidence, a (broken) gEBTN appears on DocumentFragment nodes too
				results = context.getElementsByTagName(tag);

	            // Filter out possible comments
	            if (tag === "*") {
	                while (elem = results[i++]) {
	                    if (elem.nodeType === 1) {
	                        tmp.push(elem);
	                    }
	                }

	                return tmp;
	            }
	            return results;
	        };

	        // Class
	        Expr.find["CLASS"] = support.getElementsByClassName && function (className, context) {
	            if (typeof context.getElementsByClassName !== "undefined" && documentIsHTML) {
	                return context.getElementsByClassName(className);
	            }
	        };

	        /* QSA/matchesSelector
   ---------------------------------------------------------------------- */

	        // QSA and matchesSelector support

	        // matchesSelector(:active) reports false when true (IE9/Opera 11.5)
	        rbuggyMatches = [];

	        // qSa(:focus) reports false when true (Chrome 21)
	        // We allow this because of a bug in IE8/9 that throws an error
	        // whenever `document.activeElement` is accessed on an iframe
	        // So, we allow :focus to pass through QSA all the time to avoid the IE error
	        // See https://bugs.jquery.com/ticket/13378
	        rbuggyQSA = [];

	        if (support.qsa = rnative.test(document.querySelectorAll)) {
	            // Build QSA regex
	            // Regex strategy adopted from Diego Perini
	            assert(function (el) {
	                // Select is set to empty string on purpose
	                // This is to test IE's treatment of not explicitly
	                // setting a boolean content attribute,
	                // since its presence should be enough
	                // https://bugs.jquery.com/ticket/12359
	                docElem.appendChild(el).innerHTML = "<a id='" + expando + "'></a>" + "<select id='" + expando + "-\r\\' msallowcapture=''>" + "<option selected=''></option></select>";

	                // Support: IE8, Opera 11-12.16
	                // Nothing should be selected when empty strings follow ^= or $= or *=
	                // The test attribute must be unknown in Opera but "safe" for WinRT
	                // https://msdn.microsoft.com/en-us/library/ie/hh465388.aspx#attribute_section
	                if (el.querySelectorAll("[msallowcapture^='']").length) {
	                    rbuggyQSA.push("[*^$]=" + whitespace + "*(?:''|\"\")");
	                }

	                // Support: IE8
	                // Boolean attributes and "value" are not treated correctly
	                if (!el.querySelectorAll("[selected]").length) {
	                    rbuggyQSA.push("\\[" + whitespace + "*(?:value|" + booleans + ")");
	                }

	                // Support: Chrome<29, Android<4.4, Safari<7.0+, iOS<7.0+, PhantomJS<1.9.8+
	                if (!el.querySelectorAll("[id~=" + expando + "-]").length) {
	                    rbuggyQSA.push("~=");
	                }

	                // Webkit/Opera - :checked should return selected option elements
	                // http://www.w3.org/TR/2011/REC-css3-selectors-20110929/#checked
	                // IE8 throws error here and will not see later tests
	                if (!el.querySelectorAll(":checked").length) {
	                    rbuggyQSA.push(":checked");
	                }

	                // Support: Safari 8+, iOS 8+
	                // https://bugs.webkit.org/show_bug.cgi?id=136851
	                // In-page `selector#id sibling-combinator selector` fails
	                if (!el.querySelectorAll("a#" + expando + "+*").length) {
	                    rbuggyQSA.push(".#.+[+~]");
	                }
	            });

	            assert(function (el) {
	                el.innerHTML = "<a href='' disabled='disabled'></a>" + "<select disabled='disabled'><option/></select>";

	                // Support: Windows 8 Native Apps
	                // The type and name attributes are restricted during .innerHTML assignment
	                var input = document.createElement("input");
	                input.setAttribute("type", "hidden");
	                el.appendChild(input).setAttribute("name", "D");

	                // Support: IE8
	                // Enforce case-sensitivity of name attribute
	                if (el.querySelectorAll("[name=d]").length) {
	                    rbuggyQSA.push("name" + whitespace + "*[*^$|!~]?=");
	                }

	                // FF 3.5 - :enabled/:disabled and hidden elements (hidden elements are still enabled)
	                // IE8 throws error here and will not see later tests
	                if (el.querySelectorAll(":enabled").length !== 2) {
	                    rbuggyQSA.push(":enabled", ":disabled");
	                }

	                // Support: IE9-11+
	                // IE's :disabled selector does not pick up the children of disabled fieldsets
	                docElem.appendChild(el).disabled = true;
	                if (el.querySelectorAll(":disabled").length !== 2) {
	                    rbuggyQSA.push(":enabled", ":disabled");
	                }

	                // Opera 10-11 does not throw on post-comma invalid pseudos
	                el.querySelectorAll("*,:x");
	                rbuggyQSA.push(",.*:");
	            });
	        }

	        if (support.matchesSelector = rnative.test(matches = docElem.matches || docElem.webkitMatchesSelector || docElem.mozMatchesSelector || docElem.oMatchesSelector || docElem.msMatchesSelector)) {

	            assert(function (el) {
	                // Check to see if it's possible to do matchesSelector
	                // on a disconnected node (IE 9)
	                support.disconnectedMatch = matches.call(el, "*");

	                // This should fail with an exception
	                // Gecko does not error, returns false instead
	                matches.call(el, "[s!='']:x");
	                rbuggyMatches.push("!=", pseudos);
	            });
	        }

	        rbuggyQSA = rbuggyQSA.length && new RegExp(rbuggyQSA.join("|"));
	        rbuggyMatches = rbuggyMatches.length && new RegExp(rbuggyMatches.join("|"));

	        /* Contains
   ---------------------------------------------------------------------- */
	        hasCompare = rnative.test(docElem.compareDocumentPosition);

	        // Element contains another
	        // Purposefully self-exclusive
	        // As in, an element does not contain itself
	        contains = hasCompare || rnative.test(docElem.contains) ? function (a, b) {
	            var adown = a.nodeType === 9 ? a.documentElement : a,
				    bup = b && b.parentNode;
	            return a === bup || !!(bup && bup.nodeType === 1 && (adown.contains ? adown.contains(bup) : a.compareDocumentPosition && a.compareDocumentPosition(bup) & 16));
	        } : function (a, b) {
	            if (b) {
	                while (b = b.parentNode) {
	                    if (b === a) {
	                        return true;
	                    }
	                }
	            }
	            return false;
	        };

	        /* Sorting
   ---------------------------------------------------------------------- */

	        // Document order sorting
	        sortOrder = hasCompare ? function (a, b) {

	            // Flag for duplicate removal
	            if (a === b) {
	                hasDuplicate = true;
	                return 0;
	            }

	            // Sort on method existence if only one input has compareDocumentPosition
	            var compare = !a.compareDocumentPosition - !b.compareDocumentPosition;
	            if (compare) {
	                return compare;
	            }

	            // Calculate position if both inputs belong to the same document
	            compare = (a.ownerDocument || a) === (b.ownerDocument || b) ? a.compareDocumentPosition(b) :

				// Otherwise we know they are disconnected
				1;

	            // Disconnected nodes
	            if (compare & 1 || !support.sortDetached && b.compareDocumentPosition(a) === compare) {

	                // Choose the first element that is related to our preferred document
	                if (a === document || a.ownerDocument === preferredDoc && contains(preferredDoc, a)) {
	                    return -1;
	                }
	                if (b === document || b.ownerDocument === preferredDoc && contains(preferredDoc, b)) {
	                    return 1;
	                }

	                // Maintain original order
	                return sortInput ? indexOf(sortInput, a) - indexOf(sortInput, b) : 0;
	            }

	            return compare & 4 ? -1 : 1;
	        } : function (a, b) {
	            // Exit early if the nodes are identical
	            if (a === b) {
	                hasDuplicate = true;
	                return 0;
	            }

	            var cur,
				    i = 0,
				    aup = a.parentNode,
				    bup = b.parentNode,
				    ap = [a],
				    bp = [b];

	            // Parentless nodes are either documents or disconnected
	            if (!aup || !bup) {
	                return a === document ? -1 : b === document ? 1 : aup ? -1 : bup ? 1 : sortInput ? indexOf(sortInput, a) - indexOf(sortInput, b) : 0;

	                // If the nodes are siblings, we can do a quick check
	            } else if (aup === bup) {
	                return siblingCheck(a, b);
	            }

	            // Otherwise we need full lists of their ancestors for comparison
	            cur = a;
	            while (cur = cur.parentNode) {
	                ap.unshift(cur);
	            }
	            cur = b;
	            while (cur = cur.parentNode) {
	                bp.unshift(cur);
	            }

	            // Walk down the tree looking for a discrepancy
	            while (ap[i] === bp[i]) {
	                i++;
	            }

	            return i ?
				// Do a sibling check if the nodes have a common ancestor
				siblingCheck(ap[i], bp[i]) :

				// Otherwise nodes in our document sort first
				ap[i] === preferredDoc ? -1 : bp[i] === preferredDoc ? 1 : 0;
	        };

	        return document;
	    };

	    Sizzle.matches = function (expr, elements) {
	        return Sizzle(expr, null, null, elements);
	    };

	    Sizzle.matchesSelector = function (elem, expr) {
	        // Set document vars if needed
	        if ((elem.ownerDocument || elem) !== document) {
	            setDocument(elem);
	        }

	        // Make sure that attribute selectors are quoted
	        expr = expr.replace(rattributeQuotes, "='$1']");

	        if (support.matchesSelector && documentIsHTML && !compilerCache[expr + " "] && (!rbuggyMatches || !rbuggyMatches.test(expr)) && (!rbuggyQSA || !rbuggyQSA.test(expr))) {

	            try {
	                var ret = matches.call(elem, expr);

	                // IE 9's matchesSelector returns false on disconnected nodes
	                if (ret || support.disconnectedMatch ||
	                    // As well, disconnected nodes are said to be in a document
	                    // fragment in IE 9
					elem.document && elem.document.nodeType !== 11) {
	                    return ret;
	                }
	            } catch (e) { }
	        }

	        return Sizzle(expr, document, null, [elem]).length > 0;
	    };

	    Sizzle.contains = function (context, elem) {
	        // Set document vars if needed
	        if ((context.ownerDocument || context) !== document) {
	            setDocument(context);
	        }
	        return contains(context, elem);
	    };

	    Sizzle.attr = function (elem, name) {
	        // Set document vars if needed
	        if ((elem.ownerDocument || elem) !== document) {
	            setDocument(elem);
	        }

	        var fn = Expr.attrHandle[name.toLowerCase()],

			// Don't get fooled by Object.prototype properties (jQuery #13807)
			val = fn && hasOwn.call(Expr.attrHandle, name.toLowerCase()) ? fn(elem, name, !documentIsHTML) : undefined;

	        return val !== undefined ? val : support.attributes || !documentIsHTML ? elem.getAttribute(name) : (val = elem.getAttributeNode(name)) && val.specified ? val.value : null;
	    };

	    Sizzle.escape = function (sel) {
	        return (sel + "").replace(rcssescape, fcssescape);
	    };

	    Sizzle.error = function (msg) {
	        throw new Error("Syntax error, unrecognized expression: " + msg);
	    };

	    /**
   * Document sorting and removing duplicates
   * @param {ArrayLike} results
   */
	    Sizzle.uniqueSort = function (results) {
	        var elem,
			    duplicates = [],
			    j = 0,
			    i = 0;

	        // Unless we *know* we can detect duplicates, assume their presence
	        hasDuplicate = !support.detectDuplicates;
	        sortInput = !support.sortStable && results.slice(0);
	        results.sort(sortOrder);

	        if (hasDuplicate) {
	            while (elem = results[i++]) {
	                if (elem === results[i]) {
	                    j = duplicates.push(i);
	                }
	            }
	            while (j--) {
	                results.splice(duplicates[j], 1);
	            }
	        }

	        // Clear input after sorting to release objects
	        // See https://github.com/jquery/sizzle/pull/225
	        sortInput = null;

	        return results;
	    };

	    /**
   * Utility function for retrieving the text value of an array of DOM nodes
   * @param {Array|Element} elem
   */
	    getText = Sizzle.getText = function (elem) {
	        var node,
			    ret = "",
			    i = 0,
			    nodeType = elem.nodeType;

	        if (!nodeType) {
	            // If no nodeType, this is expected to be an array
	            while (node = elem[i++]) {
	                // Do not traverse comment nodes
	                ret += getText(node);
	            }
	        } else if (nodeType === 1 || nodeType === 9 || nodeType === 11) {
	            // Use textContent for elements
	            // innerText usage removed for consistency of new lines (jQuery #11153)
	            if (typeof elem.textContent === "string") {
	                return elem.textContent;
	            } else {
	                // Traverse its children
	                for (elem = elem.firstChild; elem; elem = elem.nextSibling) {
	                    ret += getText(elem);
	                }
	            }
	        } else if (nodeType === 3 || nodeType === 4) {
	            return elem.nodeValue;
	        }
	        // Do not include comment or processing instruction nodes

	        return ret;
	    };

	    Expr = Sizzle.selectors = {

	        // Can be adjusted by the user
	        cacheLength: 50,

	        createPseudo: markFunction,

	        match: matchExpr,

	        attrHandle: {},

	        find: {},

	        relative: {
	            ">": { dir: "parentNode", first: true },
	            " ": { dir: "parentNode" },
	            "+": { dir: "previousSibling", first: true },
	            "~": { dir: "previousSibling" }
	        },

	        preFilter: {
	            "ATTR": function ATTR(match) {
	                match[1] = match[1].replace(runescape, funescape);

	                // Move the given value to match[3] whether quoted or unquoted
	                match[3] = (match[3] || match[4] || match[5] || "").replace(runescape, funescape);

	                if (match[2] === "~=") {
	                    match[3] = " " + match[3] + " ";
	                }

	                return match.slice(0, 4);
	            },

	            "CHILD": function CHILD(match) {
	                /* matches from matchExpr["CHILD"]
     	1 type (only|nth|...)
     	2 what (child|of-type)
     	3 argument (even|odd|\d*|\d*n([+-]\d+)?|...)
     	4 xn-component of xn+y argument ([+-]?\d*n|)
     	5 sign of xn-component
     	6 x of xn-component
     	7 sign of y-component
     	8 y of y-component
     */
	                match[1] = match[1].toLowerCase();

	                if (match[1].slice(0, 3) === "nth") {
	                    // nth-* requires argument
	                    if (!match[3]) {
	                        Sizzle.error(match[0]);
	                    }

	                    // numeric x and y parameters for Expr.filter.CHILD
	                    // remember that false/true cast respectively to 0/1
	                    match[4] = +(match[4] ? match[5] + (match[6] || 1) : 2 * (match[3] === "even" || match[3] === "odd"));
	                    match[5] = +(match[7] + match[8] || match[3] === "odd");

	                    // other types prohibit arguments
	                } else if (match[3]) {
	                    Sizzle.error(match[0]);
	                }

	                return match;
	            },

	            "PSEUDO": function PSEUDO(match) {
	                var excess,
					    unquoted = !match[6] && match[2];

	                if (matchExpr["CHILD"].test(match[0])) {
	                    return null;
	                }

	                // Accept quoted arguments as-is
	                if (match[3]) {
	                    match[2] = match[4] || match[5] || "";

	                    // Strip excess characters from unquoted arguments
	                } else if (unquoted && rpseudo.test(unquoted) && (
	                    // Get excess from tokenize (recursively)
					excess = tokenize(unquoted, true)) && (
	                    // advance to the next closing parenthesis
					excess = unquoted.indexOf(")", unquoted.length - excess) - unquoted.length)) {

	                    // excess is a negative index
	                    match[0] = match[0].slice(0, excess);
	                    match[2] = unquoted.slice(0, excess);
	                }

	                // Return only captures needed by the pseudo filter method (type and argument)
	                return match.slice(0, 3);
	            }
	        },

	        filter: {

	            "TAG": function TAG(nodeNameSelector) {
	                var nodeName = nodeNameSelector.replace(runescape, funescape).toLowerCase();
	                return nodeNameSelector === "*" ? function () {
	                    return true;
	                } : function (elem) {
	                    return elem.nodeName && elem.nodeName.toLowerCase() === nodeName;
	                };
	            },

	            "CLASS": function CLASS(className) {
	                var pattern = classCache[className + " "];

	                return pattern || (pattern = new RegExp("(^|" + whitespace + ")" + className + "(" + whitespace + "|$)")) && classCache(className, function (elem) {
	                    return pattern.test(typeof elem.className === "string" && elem.className || typeof elem.getAttribute !== "undefined" && elem.getAttribute("class") || "");
	                });
	            },

	            "ATTR": function ATTR(name, operator, check) {
	                return function (elem) {
	                    var result = Sizzle.attr(elem, name);

	                    if (result == null) {
	                        return operator === "!=";
	                    }
	                    if (!operator) {
	                        return true;
	                    }

	                    result += "";

	                    return operator === "=" ? result === check : operator === "!=" ? result !== check : operator === "^=" ? check && result.indexOf(check) === 0 : operator === "*=" ? check && result.indexOf(check) > -1 : operator === "$=" ? check && result.slice(-check.length) === check : operator === "~=" ? (" " + result.replace(rwhitespace, " ") + " ").indexOf(check) > -1 : operator === "|=" ? result === check || result.slice(0, check.length + 1) === check + "-" : false;
	                };
	            },

	            "CHILD": function CHILD(type, what, argument, first, last) {
	                var simple = type.slice(0, 3) !== "nth",
					    forward = type.slice(-4) !== "last",
					    ofType = what === "of-type";

	                return first === 1 && last === 0 ?

					// Shortcut for :nth-*(n)
					function (elem) {
					    return !!elem.parentNode;
					} : function (elem, context, xml) {
					    var cache,
						    uniqueCache,
						    outerCache,
						    node,
						    nodeIndex,
						    start,
						    dir = simple !== forward ? "nextSibling" : "previousSibling",
						    parent = elem.parentNode,
						    name = ofType && elem.nodeName.toLowerCase(),
						    useCache = !xml && !ofType,
						    diff = false;

					    if (parent) {

					        // :(first|last|only)-(child|of-type)
					        if (simple) {
					            while (dir) {
					                node = elem;
					                while (node = node[dir]) {
					                    if (ofType ? node.nodeName.toLowerCase() === name : node.nodeType === 1) {

					                        return false;
					                    }
					                }
					                // Reverse direction for :only-* (if we haven't yet done so)
					                start = dir = type === "only" && !start && "nextSibling";
					            }
					            return true;
					        }

					        start = [forward ? parent.firstChild : parent.lastChild];

					        // non-xml :nth-child(...) stores cache data on `parent`
					        if (forward && useCache) {

					            // Seek `elem` from a previously-cached index

					            // ...in a gzip-friendly way
					            node = parent;
					            outerCache = node[expando] || (node[expando] = {});

					            // Support: IE <9 only
					            // Defend against cloned attroperties (jQuery gh-1709)
					            uniqueCache = outerCache[node.uniqueID] || (outerCache[node.uniqueID] = {});

					            cache = uniqueCache[type] || [];
					            nodeIndex = cache[0] === dirruns && cache[1];
					            diff = nodeIndex && cache[2];
					            node = nodeIndex && parent.childNodes[nodeIndex];

					            while (node = ++nodeIndex && node && node[dir] || (

					                // Fallback to seeking `elem` from the start
								diff = nodeIndex = 0) || start.pop()) {

					                // When found, cache indexes on `parent` and break
					                if (node.nodeType === 1 && ++diff && node === elem) {
					                    uniqueCache[type] = [dirruns, nodeIndex, diff];
					                    break;
					                }
					            }
					        } else {
					            // Use previously-cached element index if available
					            if (useCache) {
					                // ...in a gzip-friendly way
					                node = elem;
					                outerCache = node[expando] || (node[expando] = {});

					                // Support: IE <9 only
					                // Defend against cloned attroperties (jQuery gh-1709)
					                uniqueCache = outerCache[node.uniqueID] || (outerCache[node.uniqueID] = {});

					                cache = uniqueCache[type] || [];
					                nodeIndex = cache[0] === dirruns && cache[1];
					                diff = nodeIndex;
					            }

					            // xml :nth-child(...)
					            // or :nth-last-child(...) or :nth(-last)?-of-type(...)
					            if (diff === false) {
					                // Use the same loop as above to seek `elem` from the start
					                while (node = ++nodeIndex && node && node[dir] || (diff = nodeIndex = 0) || start.pop()) {

					                    if ((ofType ? node.nodeName.toLowerCase() === name : node.nodeType === 1) && ++diff) {

					                        // Cache the index of each encountered element
					                        if (useCache) {
					                            outerCache = node[expando] || (node[expando] = {});

					                            // Support: IE <9 only
					                            // Defend against cloned attroperties (jQuery gh-1709)
					                            uniqueCache = outerCache[node.uniqueID] || (outerCache[node.uniqueID] = {});

					                            uniqueCache[type] = [dirruns, diff];
					                        }

					                        if (node === elem) {
					                            break;
					                        }
					                    }
					                }
					            }
					        }

					        // Incorporate the offset, then check against cycle size
					        diff -= last;
					        return diff === first || diff % first === 0 && diff / first >= 0;
					    }
					};
	            },

	            "PSEUDO": function PSEUDO(pseudo, argument) {
	                // pseudo-class names are case-insensitive
	                // http://www.w3.org/TR/selectors/#pseudo-classes
	                // Prioritize by case sensitivity in case custom pseudos are added with uppercase letters
	                // Remember that setFilters inherits from pseudos
	                var args,
					    fn = Expr.pseudos[pseudo] || Expr.setFilters[pseudo.toLowerCase()] || Sizzle.error("unsupported pseudo: " + pseudo);

	                // The user may use createPseudo to indicate that
	                // arguments are needed to create the filter function
	                // just as Sizzle does
	                if (fn[expando]) {
	                    return fn(argument);
	                }

	                // But maintain support for old signatures
	                if (fn.length > 1) {
	                    args = [pseudo, pseudo, "", argument];
	                    return Expr.setFilters.hasOwnProperty(pseudo.toLowerCase()) ? markFunction(function (seed, matches) {
	                        var idx,
							    matched = fn(seed, argument),
							    i = matched.length;
	                        while (i--) {
	                            idx = indexOf(seed, matched[i]);
	                            seed[idx] = !(matches[idx] = matched[i]);
	                        }
	                    }) : function (elem) {
	                        return fn(elem, 0, args);
	                    };
	                }

	                return fn;
	            }
	        },

	        pseudos: {
	            // Potentially complex pseudos
	            "not": markFunction(function (selector) {
	                // Trim the selector passed to compile
	                // to avoid treating leading and trailing
	                // spaces as combinators
	                var input = [],
					    results = [],
					    matcher = compile(selector.replace(rtrim, "$1"));

	                return matcher[expando] ? markFunction(function (seed, matches, context, xml) {
	                    var elem,
						    unmatched = matcher(seed, null, xml, []),
						    i = seed.length;

	                    // Match elements unmatched by `matcher`
	                    while (i--) {
	                        if (elem = unmatched[i]) {
	                            seed[i] = !(matches[i] = elem);
	                        }
	                    }
	                }) : function (elem, context, xml) {
	                    input[0] = elem;
	                    matcher(input, null, xml, results);
	                    // Don't keep the element (issue #299)
	                    input[0] = null;
	                    return !results.pop();
	                };
	            }),

	            "has": markFunction(function (selector) {
	                return function (elem) {
	                    return Sizzle(selector, elem).length > 0;
	                };
	            }),

	            "contains": markFunction(function (text) {
	                text = text.replace(runescape, funescape);
	                return function (elem) {
	                    return (elem.textContent || elem.innerText || getText(elem)).indexOf(text) > -1;
	                };
	            }),

	            // "Whether an element is represented by a :lang() selector
	            // is based solely on the element's language value
	            // being equal to the identifier C,
	            // or beginning with the identifier C immediately followed by "-".
	            // The matching of C against the element's language value is performed case-insensitively.
	            // The identifier C does not have to be a valid language name."
	            // http://www.w3.org/TR/selectors/#lang-pseudo
	            "lang": markFunction(function (lang) {
	                // lang value must be a valid identifier
	                if (!ridentifier.test(lang || "")) {
	                    Sizzle.error("unsupported lang: " + lang);
	                }
	                lang = lang.replace(runescape, funescape).toLowerCase();
	                return function (elem) {
	                    var elemLang;
	                    do {
	                        if (elemLang = documentIsHTML ? elem.lang : elem.getAttribute("xml:lang") || elem.getAttribute("lang")) {

	                            elemLang = elemLang.toLowerCase();
	                            return elemLang === lang || elemLang.indexOf(lang + "-") === 0;
	                        }
	                    } while ((elem = elem.parentNode) && elem.nodeType === 1);
	                    return false;
	                };
	            }),

	            // Miscellaneous
	            "target": function target(elem) {
	                var hash = window.location && window.location.hash;
	                return hash && hash.slice(1) === elem.id;
	            },

	            "root": function root(elem) {
	                return elem === docElem;
	            },

	            "focus": function focus(elem) {
	                return elem === document.activeElement && (!document.hasFocus || document.hasFocus()) && !!(elem.type || elem.href || ~elem.tabIndex);
	            },

	            // Boolean properties
	            "enabled": createDisabledPseudo(false),
	            "disabled": createDisabledPseudo(true),

	            "checked": function checked(elem) {
	                // In CSS3, :checked should return both checked and selected elements
	                // http://www.w3.org/TR/2011/REC-css3-selectors-20110929/#checked
	                var nodeName = elem.nodeName.toLowerCase();
	                return nodeName === "input" && !!elem.checked || nodeName === "option" && !!elem.selected;
	            },

	            "selected": function selected(elem) {
	                // Accessing this property makes selected-by-default
	                // options in Safari work properly
	                if (elem.parentNode) {
	                    elem.parentNode.selectedIndex;
	                }

	                return elem.selected === true;
	            },

	            // Contents
	            "empty": function empty(elem) {
	                // http://www.w3.org/TR/selectors/#empty-pseudo
	                // :empty is negated by element (1) or content nodes (text: 3; cdata: 4; entity ref: 5),
	                //   but not by others (comment: 8; processing instruction: 7; etc.)
	                // nodeType < 6 works because attributes (2) do not appear as children
	                for (elem = elem.firstChild; elem; elem = elem.nextSibling) {
	                    if (elem.nodeType < 6) {
	                        return false;
	                    }
	                }
	                return true;
	            },

	            "parent": function parent(elem) {
	                return !Expr.pseudos["empty"](elem);
	            },

	            // Element/input types
	            "header": function header(elem) {
	                return rheader.test(elem.nodeName);
	            },

	            "input": function input(elem) {
	                return rinputs.test(elem.nodeName);
	            },

	            "button": function button(elem) {
	                var name = elem.nodeName.toLowerCase();
	                return name === "input" && elem.type === "button" || name === "button";
	            },

	            "text": function text(elem) {
	                var attr;
	                return elem.nodeName.toLowerCase() === "input" && elem.type === "text" && (

					// Support: IE<8
					// New HTML5 attribute values (e.g., "search") appear with elem.type === "text"
					(attr = elem.getAttribute("type")) == null || attr.toLowerCase() === "text");
	            },

	            // Position-in-collection
	            "first": createPositionalPseudo(function () {
	                return [0];
	            }),

	            "last": createPositionalPseudo(function (matchIndexes, length) {
	                return [length - 1];
	            }),

	            "eq": createPositionalPseudo(function (matchIndexes, length, argument) {
	                return [argument < 0 ? argument + length : argument];
	            }),

	            "even": createPositionalPseudo(function (matchIndexes, length) {
	                var i = 0;
	                for (; i < length; i += 2) {
	                    matchIndexes.push(i);
	                }
	                return matchIndexes;
	            }),

	            "odd": createPositionalPseudo(function (matchIndexes, length) {
	                var i = 1;
	                for (; i < length; i += 2) {
	                    matchIndexes.push(i);
	                }
	                return matchIndexes;
	            }),

	            "lt": createPositionalPseudo(function (matchIndexes, length, argument) {
	                var i = argument < 0 ? argument + length : argument;
	                for (; --i >= 0;) {
	                    matchIndexes.push(i);
	                }
	                return matchIndexes;
	            }),

	            "gt": createPositionalPseudo(function (matchIndexes, length, argument) {
	                var i = argument < 0 ? argument + length : argument;
	                for (; ++i < length;) {
	                    matchIndexes.push(i);
	                }
	                return matchIndexes;
	            })
	        }
	    };

	    Expr.pseudos["nth"] = Expr.pseudos["eq"];

	    // Add button/input type pseudos
	    for (i in { radio: true, checkbox: true, file: true, password: true, image: true }) {
	        Expr.pseudos[i] = createInputPseudo(i);
	    }
	    for (i in { submit: true, reset: true }) {
	        Expr.pseudos[i] = createButtonPseudo(i);
	    }

	    // Easy API for creating new setFilters
	    function setFilters() { }
	    setFilters.prototype = Expr.filters = Expr.pseudos;
	    Expr.setFilters = new setFilters();

	    tokenize = Sizzle.tokenize = function (selector, parseOnly) {
	        var matched,
			    match,
			    tokens,
			    type,
			    soFar,
			    groups,
			    preFilters,
			    cached = tokenCache[selector + " "];

	        if (cached) {
	            return parseOnly ? 0 : cached.slice(0);
	        }

	        soFar = selector;
	        groups = [];
	        preFilters = Expr.preFilter;

	        while (soFar) {

	            // Comma and first run
	            if (!matched || (match = rcomma.exec(soFar))) {
	                if (match) {
	                    // Don't consume trailing commas as valid
	                    soFar = soFar.slice(match[0].length) || soFar;
	                }
	                groups.push(tokens = []);
	            }

	            matched = false;

	            // Combinators
	            if (match = rcombinators.exec(soFar)) {
	                matched = match.shift();
	                tokens.push({
	                    value: matched,
	                    // Cast descendant combinators to space
	                    type: match[0].replace(rtrim, " ")
	                });
	                soFar = soFar.slice(matched.length);
	            }

	            // Filters
	            for (type in Expr.filter) {
	                if ((match = matchExpr[type].exec(soFar)) && (!preFilters[type] || (match = preFilters[type](match)))) {
	                    matched = match.shift();
	                    tokens.push({
	                        value: matched,
	                        type: type,
	                        matches: match
	                    });
	                    soFar = soFar.slice(matched.length);
	                }
	            }

	            if (!matched) {
	                break;
	            }
	        }

	        // Return the length of the invalid excess
	        // if we're just parsing
	        // Otherwise, throw an error or return tokens
	        return parseOnly ? soFar.length : soFar ? Sizzle.error(selector) :
			// Cache the tokens
			tokenCache(selector, groups).slice(0);
	    };

	    function toSelector(tokens) {
	        var i = 0,
			    len = tokens.length,
			    selector = "";
	        for (; i < len; i++) {
	            selector += tokens[i].value;
	        }
	        return selector;
	    }

	    function addCombinator(matcher, combinator, base) {
	        var dir = combinator.dir,
			    skip = combinator.next,
			    key = skip || dir,
			    checkNonElements = base && key === "parentNode",
			    doneName = done++;

	        return combinator.first ?
			// Check against closest ancestor/preceding element
			function (elem, context, xml) {
			    while (elem = elem[dir]) {
			        if (elem.nodeType === 1 || checkNonElements) {
			            return matcher(elem, context, xml);
			        }
			    }
			    return false;
			} :

			// Check against all ancestor/preceding elements
			function (elem, context, xml) {
			    var oldCache,
				    uniqueCache,
				    outerCache,
				    newCache = [dirruns, doneName];

			    // We can't set arbitrary data on XML nodes, so they don't benefit from combinator caching
			    if (xml) {
			        while (elem = elem[dir]) {
			            if (elem.nodeType === 1 || checkNonElements) {
			                if (matcher(elem, context, xml)) {
			                    return true;
			                }
			            }
			        }
			    } else {
			        while (elem = elem[dir]) {
			            if (elem.nodeType === 1 || checkNonElements) {
			                outerCache = elem[expando] || (elem[expando] = {});

			                // Support: IE <9 only
			                // Defend against cloned attroperties (jQuery gh-1709)
			                uniqueCache = outerCache[elem.uniqueID] || (outerCache[elem.uniqueID] = {});

			                if (skip && skip === elem.nodeName.toLowerCase()) {
			                    elem = elem[dir] || elem;
			                } else if ((oldCache = uniqueCache[key]) && oldCache[0] === dirruns && oldCache[1] === doneName) {

			                    // Assign to newCache so results back-propagate to previous elements
			                    return newCache[2] = oldCache[2];
			                } else {
			                    // Reuse newcache so results back-propagate to previous elements
			                    uniqueCache[key] = newCache;

			                    // A match means we're done; a fail means we have to keep checking
			                    if (newCache[2] = matcher(elem, context, xml)) {
			                        return true;
			                    }
			                }
			            }
			        }
			    }
			    return false;
			};
	    }

	    function elementMatcher(matchers) {
	        return matchers.length > 1 ? function (elem, context, xml) {
	            var i = matchers.length;
	            while (i--) {
	                if (!matchers[i](elem, context, xml)) {
	                    return false;
	                }
	            }
	            return true;
	        } : matchers[0];
	    }

	    function multipleContexts(selector, contexts, results) {
	        var i = 0,
			    len = contexts.length;
	        for (; i < len; i++) {
	            Sizzle(selector, contexts[i], results);
	        }
	        return results;
	    }

	    function condense(unmatched, map, filter, context, xml) {
	        var elem,
			    newUnmatched = [],
			    i = 0,
			    len = unmatched.length,
			    mapped = map != null;

	        for (; i < len; i++) {
	            if (elem = unmatched[i]) {
	                if (!filter || filter(elem, context, xml)) {
	                    newUnmatched.push(elem);
	                    if (mapped) {
	                        map.push(i);
	                    }
	                }
	            }
	        }

	        return newUnmatched;
	    }

	    function setMatcher(preFilter, selector, matcher, postFilter, postFinder, postSelector) {
	        if (postFilter && !postFilter[expando]) {
	            postFilter = setMatcher(postFilter);
	        }
	        if (postFinder && !postFinder[expando]) {
	            postFinder = setMatcher(postFinder, postSelector);
	        }
	        return markFunction(function (seed, results, context, xml) {
	            var temp,
				    i,
				    elem,
				    preMap = [],
				    postMap = [],
				    preexisting = results.length,


				// Get initial elements from seed or context
				elems = seed || multipleContexts(selector || "*", context.nodeType ? [context] : context, []),


				// Prefilter to get matcher input, preserving a map for seed-results synchronization
				matcherIn = preFilter && (seed || !selector) ? condense(elems, preMap, preFilter, context, xml) : elems,
				    matcherOut = matcher ?
				// If we have a postFinder, or filtered seed, or non-seed postFilter or preexisting results,
				postFinder || (seed ? preFilter : preexisting || postFilter) ?

				// ...intermediate processing is necessary
				[] :

				// ...otherwise use results directly
	                results : matcherIn;

	            // Find primary matches
	            if (matcher) {
	                matcher(matcherIn, matcherOut, context, xml);
	            }

	            // Apply postFilter
	            if (postFilter) {
	                temp = condense(matcherOut, postMap);
	                postFilter(temp, [], context, xml);

	                // Un-match failing elements by moving them back to matcherIn
	                i = temp.length;
	                while (i--) {
	                    if (elem = temp[i]) {
	                        matcherOut[postMap[i]] = !(matcherIn[postMap[i]] = elem);
	                    }
	                }
	            }

	            if (seed) {
	                if (postFinder || preFilter) {
	                    if (postFinder) {
	                        // Get the final matcherOut by condensing this intermediate into postFinder contexts
	                        temp = [];
	                        i = matcherOut.length;
	                        while (i--) {
	                            if (elem = matcherOut[i]) {
	                                // Restore matcherIn since elem is not yet a final match
	                                temp.push(matcherIn[i] = elem);
	                            }
	                        }
	                        postFinder(null, matcherOut = [], temp, xml);
	                    }

	                    // Move matched elements from seed to results to keep them synchronized
	                    i = matcherOut.length;
	                    while (i--) {
	                        if ((elem = matcherOut[i]) && (temp = postFinder ? indexOf(seed, elem) : preMap[i]) > -1) {

	                            seed[temp] = !(results[temp] = elem);
	                        }
	                    }
	                }

	                // Add elements to results, through postFinder if defined
	            } else {
	                matcherOut = condense(matcherOut === results ? matcherOut.splice(preexisting, matcherOut.length) : matcherOut);
	                if (postFinder) {
	                    postFinder(null, results, matcherOut, xml);
	                } else {
	                    push.apply(results, matcherOut);
	                }
	            }
	        });
	    }

	    function matcherFromTokens(tokens) {
	        var checkContext,
			    matcher,
			    j,
			    len = tokens.length,
			    leadingRelative = Expr.relative[tokens[0].type],
			    implicitRelative = leadingRelative || Expr.relative[" "],
			    i = leadingRelative ? 1 : 0,


			// The foundational matcher ensures that elements are reachable from top-level context(s)
			matchContext = addCombinator(function (elem) {
			    return elem === checkContext;
			}, implicitRelative, true),
			    matchAnyContext = addCombinator(function (elem) {
			        return indexOf(checkContext, elem) > -1;
			    }, implicitRelative, true),
			    matchers = [function (elem, context, xml) {
			        var ret = !leadingRelative && (xml || context !== outermostContext) || ((checkContext = context).nodeType ? matchContext(elem, context, xml) : matchAnyContext(elem, context, xml));
			        // Avoid hanging onto element (issue #299)
			        checkContext = null;
			        return ret;
			    }];

	        for (; i < len; i++) {
	            if (matcher = Expr.relative[tokens[i].type]) {
	                matchers = [addCombinator(elementMatcher(matchers), matcher)];
	            } else {
	                matcher = Expr.filter[tokens[i].type].apply(null, tokens[i].matches);

	                // Return special upon seeing a positional matcher
	                if (matcher[expando]) {
	                    // Find the next relative operator (if any) for proper handling
	                    j = ++i;
	                    for (; j < len; j++) {
	                        if (Expr.relative[tokens[j].type]) {
	                            break;
	                        }
	                    }
	                    return setMatcher(i > 1 && elementMatcher(matchers), i > 1 && toSelector(
						// If the preceding token was a descendant combinator, insert an implicit any-element `*`
						tokens.slice(0, i - 1).concat({ value: tokens[i - 2].type === " " ? "*" : "" })).replace(rtrim, "$1"), matcher, i < j && matcherFromTokens(tokens.slice(i, j)), j < len && matcherFromTokens(tokens = tokens.slice(j)), j < len && toSelector(tokens));
	                }
	                matchers.push(matcher);
	            }
	        }

	        return elementMatcher(matchers);
	    }

	    function matcherFromGroupMatchers(elementMatchers, setMatchers) {
	        var bySet = setMatchers.length > 0,
			    byElement = elementMatchers.length > 0,
			    superMatcher = function superMatcher(seed, context, xml, results, outermost) {
			        var elem,
                        j,
                        matcher,
                        matchedCount = 0,
                        i = "0",
                        unmatched = seed && [],
                        setMatched = [],
                        contextBackup = outermostContext,

                    // We must always have either seed elements or outermost context
                    elems = seed || byElement && Expr.find["TAG"]("*", outermost),

                    // Use integer dirruns iff this is the outermost matcher
                    dirrunsUnique = dirruns += contextBackup == null ? 1 : Math.random() || 0.1,
                        len = elems.length;

			        if (outermost) {
			            outermostContext = context === document || context || outermost;
			        }

			        // Add elements passing elementMatchers directly to results
			        // Support: IE<9, Safari
			        // Tolerate NodeList properties (IE: "length"; Safari: <number>) matching elements by id
			        for (; i !== len && (elem = elems[i]) != null; i++) {
			            if (byElement && elem) {
			                j = 0;
			                if (!context && elem.ownerDocument !== document) {
			                    setDocument(elem);
			                    xml = !documentIsHTML;
			                }
			                while (matcher = elementMatchers[j++]) {
			                    if (matcher(elem, context || document, xml)) {
			                        results.push(elem);
			                        break;
			                    }
			                }
			                if (outermost) {
			                    dirruns = dirrunsUnique;
			                }
			            }

			            // Track unmatched elements for set filters
			            if (bySet) {
			                // They will have gone through all possible matchers
			                if (elem = !matcher && elem) {
			                    matchedCount--;
			                }

			                // Lengthen the array for every element, matched or not
			                if (seed) {
			                    unmatched.push(elem);
			                }
			            }
			        }

			        // `i` is now the count of elements visited above, and adding it to `matchedCount`
			        // makes the latter nonnegative.
			        matchedCount += i;

			        // Apply set filters to unmatched elements
			        // NOTE: This can be skipped if there are no unmatched elements (i.e., `matchedCount`
			        // equals `i`), unless we didn't visit _any_ elements in the above loop because we have
			        // no element matchers and no seed.
			        // Incrementing an initially-string "0" `i` allows `i` to remain a string only in that
			        // case, which will result in a "00" `matchedCount` that differs from `i` but is also
			        // numerically zero.
			        if (bySet && i !== matchedCount) {
			            j = 0;
			            while (matcher = setMatchers[j++]) {
			                matcher(unmatched, setMatched, context, xml);
			            }

			            if (seed) {
			                // Reintegrate element matches to eliminate the need for sorting
			                if (matchedCount > 0) {
			                    while (i--) {
			                        if (!(unmatched[i] || setMatched[i])) {
			                            setMatched[i] = pop.call(results);
			                        }
			                    }
			                }

			                // Discard index placeholder values to get only actual matches
			                setMatched = condense(setMatched);
			            }

			            // Add matches to results
			            push.apply(results, setMatched);

			            // Seedless set matches succeeding multiple successful matchers stipulate sorting
			            if (outermost && !seed && setMatched.length > 0 && matchedCount + setMatchers.length > 1) {

			                Sizzle.uniqueSort(results);
			            }
			        }

			        // Override manipulation of globals by nested matchers
			        if (outermost) {
			            dirruns = dirrunsUnique;
			            outermostContext = contextBackup;
			        }

			        return unmatched;
			    };

	        return bySet ? markFunction(superMatcher) : superMatcher;
	    }

	    compile = Sizzle.compile = function (selector, match /* Internal Use Only */) {
	        var i,
			    setMatchers = [],
			    elementMatchers = [],
			    cached = compilerCache[selector + " "];

	        if (!cached) {
	            // Generate a function of recursive functions that can be used to check each element
	            if (!match) {
	                match = tokenize(selector);
	            }
	            i = match.length;
	            while (i--) {
	                cached = matcherFromTokens(match[i]);
	                if (cached[expando]) {
	                    setMatchers.push(cached);
	                } else {
	                    elementMatchers.push(cached);
	                }
	            }

	            // Cache the compiled function
	            cached = compilerCache(selector, matcherFromGroupMatchers(elementMatchers, setMatchers));

	            // Save selector and tokenization
	            cached.selector = selector;
	        }
	        return cached;
	    };

	    /**
   * A low-level selection function that works with Sizzle's compiled
   *  selector functions
   * @param {String|Function} selector A selector or a pre-compiled
   *  selector function built with Sizzle.compile
   * @param {Element} context
   * @param {Array} [results]
   * @param {Array} [seed] A set of elements to match against
   */
	    select = Sizzle.select = function (selector, context, results, seed) {
	        var i,
			    tokens,
			    token,
			    type,
			    find,
			    compiled = typeof selector === "function" && selector,
			    match = !seed && tokenize(selector = compiled.selector || selector);

	        results = results || [];

	        // Try to minimize operations if there is only one selector in the list and no seed
	        // (the latter of which guarantees us context)
	        if (match.length === 1) {

	            // Reduce context if the leading compound selector is an ID
	            tokens = match[0] = match[0].slice(0);
	            if (tokens.length > 2 && (token = tokens[0]).type === "ID" && context.nodeType === 9 && documentIsHTML && Expr.relative[tokens[1].type]) {

	                context = (Expr.find["ID"](token.matches[0].replace(runescape, funescape), context) || [])[0];
	                if (!context) {
	                    return results;

	                    // Precompiled matchers will still verify ancestry, so step up a level
	                } else if (compiled) {
	                    context = context.parentNode;
	                }

	                selector = selector.slice(tokens.shift().value.length);
	            }

	            // Fetch a seed set for right-to-left matching
	            i = matchExpr["needsContext"].test(selector) ? 0 : tokens.length;
	            while (i--) {
	                token = tokens[i];

	                // Abort if we hit a combinator
	                if (Expr.relative[type = token.type]) {
	                    break;
	                }
	                if (find = Expr.find[type]) {
	                    // Search, expanding context for leading sibling combinators
	                    if (seed = find(token.matches[0].replace(runescape, funescape), rsibling.test(tokens[0].type) && testContext(context.parentNode) || context)) {

	                        // If seed is empty or no tokens remain, we can return early
	                        tokens.splice(i, 1);
	                        selector = seed.length && toSelector(tokens);
	                        if (!selector) {
	                            push.apply(results, seed);
	                            return results;
	                        }

	                        break;
	                    }
	                }
	            }
	        }

	        // Compile and execute a filtering function if one is not provided
	        // Provide `match` to avoid retokenization if we modified the selector above
	        (compiled || compile(selector, match))(seed, context, !documentIsHTML, results, !context || rsibling.test(selector) && testContext(context.parentNode) || context);
	        return results;
	    };

	    // One-time assignments

	    // Sort stability
	    support.sortStable = expando.split("").sort(sortOrder).join("") === expando;

	    // Support: Chrome 14-35+
	    // Always assume duplicates if they aren't passed to the comparison function
	    support.detectDuplicates = !!hasDuplicate;

	    // Initialize against the default document
	    setDocument();

	    // Support: Webkit<537.32 - Safari 6.0.3/Chrome 25 (fixed in Chrome 27)
	    // Detached nodes confoundingly follow *each other*
	    support.sortDetached = assert(function (el) {
	        // Should return 1, but returns 4 (following)
	        return el.compareDocumentPosition(document.createElement("fieldset")) & 1;
	    });

	    // Support: IE<8
	    // Prevent attribute/property "interpolation"
	    // https://msdn.microsoft.com/en-us/library/ms536429%28VS.85%29.aspx
	    if (!assert(function (el) {
			el.innerHTML = "<a href='#'></a>";
			return el.firstChild.getAttribute("href") === "#";
	    })) {
	        addHandle("type|href|height|width", function (elem, name, isXML) {
	            if (!isXML) {
	                return elem.getAttribute(name, name.toLowerCase() === "type" ? 1 : 2);
	            }
	        });
	    }

	    // Support: IE<9
	    // Use defaultValue in place of getAttribute("value")
	    if (!support.attributes || !assert(function (el) {
			el.innerHTML = "<input/>";
			el.firstChild.setAttribute("value", "");
			return el.firstChild.getAttribute("value") === "";
	    })) {
	        addHandle("value", function (elem, name, isXML) {
	            if (!isXML && elem.nodeName.toLowerCase() === "input") {
	                return elem.defaultValue;
	            }
	        });
	    }

	    // Support: IE<9
	    // Use getAttributeNode to fetch booleans when getAttribute lies
	    if (!assert(function (el) {
			return el.getAttribute("disabled") == null;
	    })) {
	        addHandle(booleans, function (elem, name, isXML) {
	            var val;
	            if (!isXML) {
	                return elem[name] === true ? name.toLowerCase() : (val = elem.getAttributeNode(name)) && val.specified ? val.value : null;
	            }
	        });
	    }

	    return Sizzle;
	}(window);

    jQuery.find = Sizzle;
    jQuery.expr = Sizzle.selectors;

    // Deprecated
    jQuery.expr[":"] = jQuery.expr.pseudos;
    jQuery.uniqueSort = jQuery.unique = Sizzle.uniqueSort;
    jQuery.text = Sizzle.getText;
    jQuery.isXMLDoc = Sizzle.isXML;
    jQuery.contains = Sizzle.contains;
    jQuery.escapeSelector = Sizzle.escape;

    var dir = function dir(elem, _dir, until) {
        var matched = [],
		    truncate = until !== undefined;

        while ((elem = elem[_dir]) && elem.nodeType !== 9) {
            if (elem.nodeType === 1) {
                if (truncate && jQuery(elem).is(until)) {
                    break;
                }
                matched.push(elem);
            }
        }
        return matched;
    };

    var _siblings = function _siblings(n, elem) {
        var matched = [];

        for (; n; n = n.nextSibling) {
            if (n.nodeType === 1 && n !== elem) {
                matched.push(n);
            }
        }

        return matched;
    };

    var rneedsContext = jQuery.expr.match.needsContext;

    var rsingleTag = /^<([a-z][^\/\0>:\x20\t\r\n\f]*)[\x20\t\r\n\f]*\/?>(?:<\/\1>|)$/i;

    var risSimple = /^.[^:#\[\.,]*$/;

    // Implement the identical functionality for filter and not
    function winnow(elements, qualifier, not) {
        if (jQuery.isFunction(qualifier)) {
            return jQuery.grep(elements, function (elem, i) {
                return !!qualifier.call(elem, i, elem) !== not;
            });
        }

        // Single element
        if (qualifier.nodeType) {
            return jQuery.grep(elements, function (elem) {
                return elem === qualifier !== not;
            });
        }

        // Arraylike of elements (jQuery, arguments, Array)
        if (typeof qualifier !== "string") {
            return jQuery.grep(elements, function (elem) {
                return indexOf.call(qualifier, elem) > -1 !== not;
            });
        }

        // Simple selector that can be filtered directly, removing non-Elements
        if (risSimple.test(qualifier)) {
            return jQuery.filter(qualifier, elements, not);
        }

        // Complex selector, compare the two sets, removing non-Elements
        qualifier = jQuery.filter(qualifier, elements);
        return jQuery.grep(elements, function (elem) {
            return indexOf.call(qualifier, elem) > -1 !== not && elem.nodeType === 1;
        });
    }

    jQuery.filter = function (expr, elems, not) {
        var elem = elems[0];

        if (not) {
            expr = ":not(" + expr + ")";
        }

        if (elems.length === 1 && elem.nodeType === 1) {
            return jQuery.find.matchesSelector(elem, expr) ? [elem] : [];
        }

        return jQuery.find.matches(expr, jQuery.grep(elems, function (elem) {
            return elem.nodeType === 1;
        }));
    };

    jQuery.fn.extend({
        find: function find(selector) {
            var i,
			    ret,
			    len = this.length,
			    self = this;

            if (typeof selector !== "string") {
                return this.pushStack(jQuery(selector).filter(function () {
                    for (i = 0; i < len; i++) {
                        if (jQuery.contains(self[i], this)) {
                            return true;
                        }
                    }
                }));
            }

            ret = this.pushStack([]);

            for (i = 0; i < len; i++) {
                jQuery.find(selector, self[i], ret);
            }

            return len > 1 ? jQuery.uniqueSort(ret) : ret;
        },
        filter: function filter(selector) {
            return this.pushStack(winnow(this, selector || [], false));
        },
        not: function not(selector) {
            return this.pushStack(winnow(this, selector || [], true));
        },
        is: function is(selector) {
            return !!winnow(this,

			// If this is a positional/relative selector, check membership in the returned set
			// so $("p:first").is("p:last") won't return true for a doc with two "p".
			typeof selector === "string" && rneedsContext.test(selector) ? jQuery(selector) : selector || [], false).length;
        }
    });

    // Initialize a jQuery object


    // A central reference to the root jQuery(document)
    var rootjQuery,


	// A simple way to check for HTML strings
	// Prioritize #id over <tag> to avoid XSS via location.hash (#9521)
	// Strict HTML recognition (#11290: must start with <)
	// Shortcut simple #id case for speed
	rquickExpr = /^(?:\s*(<[\w\W]+>)[^>]*|#([\w-]+))$/,
	    init = jQuery.fn.init = function (selector, context, root) {
	        var match, elem;

	        // HANDLE: $(""), $(null), $(undefined), $(false)
	        if (!selector) {
	            return this;
	        }

	        // Method init() accepts an alternate rootjQuery
	        // so migrate can support jQuery.sub (gh-2101)
	        root = root || rootjQuery;

	        // Handle HTML strings
	        if (typeof selector === "string") {
	            if (selector[0] === "<" && selector[selector.length - 1] === ">" && selector.length >= 3) {

	                // Assume that strings that start and end with <> are HTML and skip the regex check
	                match = [null, selector, null];
	            } else {
	                match = rquickExpr.exec(selector);
	            }

	            // Match html or make sure no context is specified for #id
	            if (match && (match[1] || !context)) {

	                // HANDLE: $(html) -> $(array)
	                if (match[1]) {
	                    context = context instanceof jQuery ? context[0] : context;

	                    // Option to run scripts is true for back-compat
	                    // Intentionally let the error be thrown if parseHTML is not present
	                    jQuery.merge(this, jQuery.parseHTML(match[1], context && context.nodeType ? context.ownerDocument || context : document, true));

	                    // HANDLE: $(html, props)
	                    if (rsingleTag.test(match[1]) && jQuery.isPlainObject(context)) {
	                        for (match in context) {

	                            // Properties of context are called as methods if possible
	                            if (jQuery.isFunction(this[match])) {
	                                this[match](context[match]);

	                                // ...and otherwise set as attributes
	                            } else {
	                                this.attr(match, context[match]);
	                            }
	                        }
	                    }

	                    return this;

	                    // HANDLE: $(#id)
	                } else {
	                    elem = document.getElementById(match[2]);

	                    if (elem) {

	                        // Inject the element directly into the jQuery object
	                        this[0] = elem;
	                        this.length = 1;
	                    }
	                    return this;
	                }

	                // HANDLE: $(expr, $(...))
	            } else if (!context || context.jquery) {
	                return (context || root).find(selector);

	                // HANDLE: $(expr, context)
	                // (which is just equivalent to: $(context).find(expr)
	            } else {
	                return this.constructor(context).find(selector);
	            }

	            // HANDLE: $(DOMElement)
	        } else if (selector.nodeType) {
	            this[0] = selector;
	            this.length = 1;
	            return this;

	            // HANDLE: $(function)
	            // Shortcut for document ready
	        } else if (jQuery.isFunction(selector)) {
	            return root.ready !== undefined ? root.ready(selector) :

                // Execute immediately if ready is not present
                selector(jQuery);
	        }

	        return jQuery.makeArray(selector, this);
	    };

    // Give the init function the jQuery prototype for later instantiation
    init.prototype = jQuery.fn;

    // Initialize central reference
    rootjQuery = jQuery(document);

    var rparentsprev = /^(?:parents|prev(?:Until|All))/,


	// Methods guaranteed to produce a unique set when starting from a unique set
	guaranteedUnique = {
	    children: true,
	    contents: true,
	    next: true,
	    prev: true
	};

    jQuery.fn.extend({
        has: function has(target) {
            var targets = jQuery(target, this),
			    l = targets.length;

            return this.filter(function () {
                var i = 0;
                for (; i < l; i++) {
                    if (jQuery.contains(this, targets[i])) {
                        return true;
                    }
                }
            });
        },

        closest: function closest(selectors, context) {
            var cur,
			    i = 0,
			    l = this.length,
			    matched = [],
			    targets = typeof selectors !== "string" && jQuery(selectors);

            // Positional selectors never match, since there's no _selection_ context
            if (!rneedsContext.test(selectors)) {
                for (; i < l; i++) {
                    for (cur = this[i]; cur && cur !== context; cur = cur.parentNode) {

                        // Always skip document fragments
                        if (cur.nodeType < 11 && (targets ? targets.index(cur) > -1 :

                            // Don't pass non-elements to Sizzle
						cur.nodeType === 1 && jQuery.find.matchesSelector(cur, selectors))) {

                            matched.push(cur);
                            break;
                        }
                    }
                }
            }

            return this.pushStack(matched.length > 1 ? jQuery.uniqueSort(matched) : matched);
        },

        // Determine the position of an element within the set
        index: function index(elem) {

            // No argument, return index in parent
            if (!elem) {
                return this[0] && this[0].parentNode ? this.first().prevAll().length : -1;
            }

            // Index in selector
            if (typeof elem === "string") {
                return indexOf.call(jQuery(elem), this[0]);
            }

            // Locate the position of the desired element
            return indexOf.call(this,

			// If it receives a jQuery object, the first element is used
			elem.jquery ? elem[0] : elem);
        },

        add: function add(selector, context) {
            return this.pushStack(jQuery.uniqueSort(jQuery.merge(this.get(), jQuery(selector, context))));
        },

        addBack: function addBack(selector) {
            return this.add(selector == null ? this.prevObject : this.prevObject.filter(selector));
        }
    });

    function sibling(cur, dir) {
        while ((cur = cur[dir]) && cur.nodeType !== 1) { }
        return cur;
    }

    jQuery.each({
        parent: function parent(elem) {
            var parent = elem.parentNode;
            return parent && parent.nodeType !== 11 ? parent : null;
        },
        parents: function parents(elem) {
            return dir(elem, "parentNode");
        },
        parentsUntil: function parentsUntil(elem, i, until) {
            return dir(elem, "parentNode", until);
        },
        next: function next(elem) {
            return sibling(elem, "nextSibling");
        },
        prev: function prev(elem) {
            return sibling(elem, "previousSibling");
        },
        nextAll: function nextAll(elem) {
            return dir(elem, "nextSibling");
        },
        prevAll: function prevAll(elem) {
            return dir(elem, "previousSibling");
        },
        nextUntil: function nextUntil(elem, i, until) {
            return dir(elem, "nextSibling", until);
        },
        prevUntil: function prevUntil(elem, i, until) {
            return dir(elem, "previousSibling", until);
        },
        siblings: function siblings(elem) {
            return _siblings((elem.parentNode || {}).firstChild, elem);
        },
        children: function children(elem) {
            return _siblings(elem.firstChild);
        },
        contents: function contents(elem) {
            return elem.contentDocument || jQuery.merge([], elem.childNodes);
        }
    }, function (name, fn) {
        jQuery.fn[name] = function (until, selector) {
            var matched = jQuery.map(this, fn, until);

            if (name.slice(-5) !== "Until") {
                selector = until;
            }

            if (selector && typeof selector === "string") {
                matched = jQuery.filter(selector, matched);
            }

            if (this.length > 1) {

                // Remove duplicates
                if (!guaranteedUnique[name]) {
                    jQuery.uniqueSort(matched);
                }

                // Reverse order for parents* and prev-derivatives
                if (rparentsprev.test(name)) {
                    matched.reverse();
                }
            }

            return this.pushStack(matched);
        };
    });
    var rnothtmlwhite = /[^\x20\t\r\n\f]+/g;

    // Convert String-formatted options into Object-formatted ones
    function createOptions(options) {
        var object = {};
        jQuery.each(options.match(rnothtmlwhite) || [], function (_, flag) {
            object[flag] = true;
        });
        return object;
    }

    /*
  * Create a callback list using the following parameters:
  *
  *	options: an optional list of space-separated options that will change how
  *			the callback list behaves or a more traditional option object
  *
  * By default a callback list will act like an event callback list and can be
  * "fired" multiple times.
  *
  * Possible options:
  *
  *	once:			will ensure the callback list can only be fired once (like a Deferred)
  *
  *	memory:			will keep track of previous values and will call any callback added
  *					after the list has been fired right away with the latest "memorized"
  *					values (like a Deferred)
  *
  *	unique:			will ensure a callback can only be added once (no duplicate in the list)
  *
  *	stopOnFalse:	interrupt callings when a callback returns false
  *
  */
    jQuery.Callbacks = function (options) {

        // Convert options from String-formatted to Object-formatted if needed
        // (we check in cache first)
        options = typeof options === "string" ? createOptions(options) : jQuery.extend({}, options);

        var // Flag to know if list is currently firing
		firing,


		// Last fire value for non-forgettable lists
		memory,


		// Flag to know if list was already fired
		_fired,


		// Flag to prevent firing
		_locked,


		// Actual callback list
		list = [],


		// Queue of execution data for repeatable lists
		queue = [],


		// Index of currently firing callback (modified by add/remove as needed)
		firingIndex = -1,


		// Fire callbacks
		fire = function fire() {

		    // Enforce single-firing
		    _locked = options.once;

		    // Execute callbacks for all pending executions,
		    // respecting firingIndex overrides and runtime changes
		    _fired = firing = true;
		    for (; queue.length; firingIndex = -1) {
		        memory = queue.shift();
		        while (++firingIndex < list.length) {

		            // Run callback and check for early termination
		            if (list[firingIndex].apply(memory[0], memory[1]) === false && options.stopOnFalse) {

		                // Jump to end and forget the data so .add doesn't re-fire
		                firingIndex = list.length;
		                memory = false;
		            }
		        }
		    }

		    // Forget the data if we're done with it
		    if (!options.memory) {
		        memory = false;
		    }

		    firing = false;

		    // Clean up if we're done firing for good
		    if (_locked) {

		        // Keep an empty list if we have data for future add calls
		        if (memory) {
		            list = [];

		            // Otherwise, this object is spent
		        } else {
		            list = "";
		        }
		    }
		},


		// Actual Callbacks object
		self = {

		    // Add a callback or a collection of callbacks to the list
		    add: function add() {
		        if (list) {

		            // If we have memory from a past run, we should fire after adding
		            if (memory && !firing) {
		                firingIndex = list.length - 1;
		                queue.push(memory);
		            }

		            (function add(args) {
		                jQuery.each(args, function (_, arg) {
		                    if (jQuery.isFunction(arg)) {
		                        if (!options.unique || !self.has(arg)) {
		                            list.push(arg);
		                        }
		                    } else if (arg && arg.length && jQuery.type(arg) !== "string") {

		                        // Inspect recursively
		                        add(arg);
		                    }
		                });
		            })(arguments);

		            if (memory && !firing) {
		                fire();
		            }
		        }
		        return this;
		    },

		    // Remove a callback from the list
		    remove: function remove() {
		        jQuery.each(arguments, function (_, arg) {
		            var index;
		            while ((index = jQuery.inArray(arg, list, index)) > -1) {
		                list.splice(index, 1);

		                // Handle firing indexes
		                if (index <= firingIndex) {
		                    firingIndex--;
		                }
		            }
		        });
		        return this;
		    },

		    // Check if a given callback is in the list.
		    // If no argument is given, return whether or not list has callbacks attached.
		    has: function has(fn) {
		        return fn ? jQuery.inArray(fn, list) > -1 : list.length > 0;
		    },

		    // Remove all callbacks from the list
		    empty: function empty() {
		        if (list) {
		            list = [];
		        }
		        return this;
		    },

		    // Disable .fire and .add
		    // Abort any current/pending executions
		    // Clear all callbacks and values
		    disable: function disable() {
		        _locked = queue = [];
		        list = memory = "";
		        return this;
		    },
		    disabled: function disabled() {
		        return !list;
		    },

		    // Disable .fire
		    // Also disable .add unless we have memory (since it would have no effect)
		    // Abort any pending executions
		    lock: function lock() {
		        _locked = queue = [];
		        if (!memory && !firing) {
		            list = memory = "";
		        }
		        return this;
		    },
		    locked: function locked() {
		        return !!_locked;
		    },

		    // Call all callbacks with the given context and arguments
		    fireWith: function fireWith(context, args) {
		        if (!_locked) {
		            args = args || [];
		            args = [context, args.slice ? args.slice() : args];
		            queue.push(args);
		            if (!firing) {
		                fire();
		            }
		        }
		        return this;
		    },

		    // Call all the callbacks with the given arguments
		    fire: function fire() {
		        self.fireWith(this, arguments);
		        return this;
		    },

		    // To know if the callbacks have already been called at least once
		    fired: function fired() {
		        return !!_fired;
		    }
		};

        return self;
    };

    function Identity(v) {
        return v;
    }
    function Thrower(ex) {
        throw ex;
    }

    function adoptValue(value, resolve, reject) {
        var method;

        try {

            // Check for promise aspect first to privilege synchronous behavior
            if (value && jQuery.isFunction(method = value.promise)) {
                method.call(value).done(resolve).fail(reject);

                // Other thenables
            } else if (value && jQuery.isFunction(method = value.then)) {
                method.call(value, resolve, reject);

                // Other non-thenables
            } else {

                // Support: Android 4.0 only
                // Strict mode functions invoked without .call/.apply get global-object context
                resolve.call(undefined, value);
            }

            // For Promises/A+, convert exceptions into rejections
            // Since jQuery.when doesn't unwrap thenables, we can skip the extra checks appearing in
            // Deferred#then to conditionally suppress rejection.
        } catch (value) {

            // Support: Android 4.0 only
            // Strict mode functions invoked without .call/.apply get global-object context
            reject.call(undefined, value);
        }
    }

    jQuery.extend({

        Deferred: function Deferred(func) {
            var tuples = [

			// action, add listener, callbacks,
			// ... .then handlers, argument index, [final state]
			["notify", "progress", jQuery.Callbacks("memory"), jQuery.Callbacks("memory"), 2], ["resolve", "done", jQuery.Callbacks("once memory"), jQuery.Callbacks("once memory"), 0, "resolved"], ["reject", "fail", jQuery.Callbacks("once memory"), jQuery.Callbacks("once memory"), 1, "rejected"]],
			    _state = "pending",
			    _promise = {
			        state: function state() {
			            return _state;
			        },
			        always: function always() {
			            deferred.done(arguments).fail(arguments);
			            return this;
			        },
			        "catch": function _catch(fn) {
			            return _promise.then(null, fn);
			        },

			        // Keep pipe for back-compat
			        pipe: function pipe() /* fnDone, fnFail, fnProgress */ {
			            var fns = arguments;

			            return jQuery.Deferred(function (newDefer) {
			                jQuery.each(tuples, function (i, tuple) {

			                    // Map tuples (progress, done, fail) to arguments (done, fail, progress)
			                    var fn = jQuery.isFunction(fns[tuple[4]]) && fns[tuple[4]];

			                    // deferred.progress(function() { bind to newDefer or newDefer.notify })
			                    // deferred.done(function() { bind to newDefer or newDefer.resolve })
			                    // deferred.fail(function() { bind to newDefer or newDefer.reject })
			                    deferred[tuple[1]](function () {
			                        var returned = fn && fn.apply(this, arguments);
			                        if (returned && jQuery.isFunction(returned.promise)) {
			                            returned.promise().progress(newDefer.notify).done(newDefer.resolve).fail(newDefer.reject);
			                        } else {
			                            newDefer[tuple[0] + "With"](this, fn ? [returned] : arguments);
			                        }
			                    });
			                });
			                fns = null;
			            }).promise();
			        },
			        then: function then(onFulfilled, onRejected, onProgress) {
			            var maxDepth = 0;
			            function resolve(depth, deferred, handler, special) {
			                return function () {
			                    var that = this,
                                    args = arguments,
                                    mightThrow = function mightThrow() {
                                        var returned, then;

                                        // Support: Promises/A+ section 2.3.3.3.3
                                        // https://promisesaplus.com/#point-59
                                        // Ignore double-resolution attempts
                                        if (depth < maxDepth) {
                                            return;
                                        }

                                        returned = handler.apply(that, args);

                                        // Support: Promises/A+ section 2.3.1
                                        // https://promisesaplus.com/#point-48
                                        if (returned === deferred.promise()) {
                                            throw new TypeError("Thenable self-resolution");
                                        }

                                        // Support: Promises/A+ sections 2.3.3.1, 3.5
                                        // https://promisesaplus.com/#point-54
                                        // https://promisesaplus.com/#point-75
                                        // Retrieve `then` only once
                                        then = returned && (

                                        // Support: Promises/A+ section 2.3.4
                                        // https://promisesaplus.com/#point-64
                                        // Only check objects and functions for thenability
                                        (typeof returned === "undefined" ? "undefined" : _typeof(returned)) === "object" || typeof returned === "function") && returned.then;

                                        // Handle a returned thenable
                                        if (jQuery.isFunction(then)) {

                                            // Special processors (notify) just wait for resolution
                                            if (special) {
                                                then.call(returned, resolve(maxDepth, deferred, Identity, special), resolve(maxDepth, deferred, Thrower, special));

                                                // Normal processors (resolve) also hook into progress
                                            } else {

                                                // ...and disregard older resolution values
                                                maxDepth++;

                                                then.call(returned, resolve(maxDepth, deferred, Identity, special), resolve(maxDepth, deferred, Thrower, special), resolve(maxDepth, deferred, Identity, deferred.notifyWith));
                                            }

                                            // Handle all other returned values
                                        } else {

                                            // Only substitute handlers pass on context
                                            // and multiple values (non-spec behavior)
                                            if (handler !== Identity) {
                                                that = undefined;
                                                args = [returned];
                                            }

                                            // Process the value(s)
                                            // Default process is resolve
                                            (special || deferred.resolveWith)(that, args);
                                        }
                                    },


                                // Only normal processors (resolve) catch and reject exceptions
                                process = special ? mightThrow : function () {
                                    try {
                                        mightThrow();
                                    } catch (e) {

                                        if (jQuery.Deferred.exceptionHook) {
                                            jQuery.Deferred.exceptionHook(e, process.stackTrace);
                                        }

                                        // Support: Promises/A+ section 2.3.3.3.4.1
                                        // https://promisesaplus.com/#point-61
                                        // Ignore post-resolution exceptions
                                        if (depth + 1 >= maxDepth) {

                                            // Only substitute handlers pass on context
                                            // and multiple values (non-spec behavior)
                                            if (handler !== Thrower) {
                                                that = undefined;
                                                args = [e];
                                            }

                                            deferred.rejectWith(that, args);
                                        }
                                    }
                                };

			                    // Support: Promises/A+ section 2.3.3.3.1
			                    // https://promisesaplus.com/#point-57
			                    // Re-resolve promises immediately to dodge false rejection from
			                    // subsequent errors
			                    if (depth) {
			                        process();
			                    } else {

			                        // Call an optional hook to record the stack, in case of exception
			                        // since it's otherwise lost when execution goes async
			                        if (jQuery.Deferred.getStackHook) {
			                            process.stackTrace = jQuery.Deferred.getStackHook();
			                        }
			                        window.setTimeout(process);
			                    }
			                };
			            }

			            return jQuery.Deferred(function (newDefer) {

			                // progress_handlers.add( ... )
			                tuples[0][3].add(resolve(0, newDefer, jQuery.isFunction(onProgress) ? onProgress : Identity, newDefer.notifyWith));

			                // fulfilled_handlers.add( ... )
			                tuples[1][3].add(resolve(0, newDefer, jQuery.isFunction(onFulfilled) ? onFulfilled : Identity));

			                // rejected_handlers.add( ... )
			                tuples[2][3].add(resolve(0, newDefer, jQuery.isFunction(onRejected) ? onRejected : Thrower));
			            }).promise();
			        },

			        // Get a promise for this deferred
			        // If obj is provided, the promise aspect is added to the object
			        promise: function promise(obj) {
			            return obj != null ? jQuery.extend(obj, _promise) : _promise;
			        }
			    },
			    deferred = {};

            // Add list-specific methods
            jQuery.each(tuples, function (i, tuple) {
                var list = tuple[2],
				    stateString = tuple[5];

                // promise.progress = list.add
                // promise.done = list.add
                // promise.fail = list.add
                _promise[tuple[1]] = list.add;

                // Handle state
                if (stateString) {
                    list.add(function () {

                        // state = "resolved" (i.e., fulfilled)
                        // state = "rejected"
                        _state = stateString;
                    },

					// rejected_callbacks.disable
					// fulfilled_callbacks.disable
					tuples[3 - i][2].disable,

					// progress_callbacks.lock
					tuples[0][2].lock);
                }

                // progress_handlers.fire
                // fulfilled_handlers.fire
                // rejected_handlers.fire
                list.add(tuple[3].fire);

                // deferred.notify = function() { deferred.notifyWith(...) }
                // deferred.resolve = function() { deferred.resolveWith(...) }
                // deferred.reject = function() { deferred.rejectWith(...) }
                deferred[tuple[0]] = function () {
                    deferred[tuple[0] + "With"](this === deferred ? undefined : this, arguments);
                    return this;
                };

                // deferred.notifyWith = list.fireWith
                // deferred.resolveWith = list.fireWith
                // deferred.rejectWith = list.fireWith
                deferred[tuple[0] + "With"] = list.fireWith;
            });

            // Make the deferred a promise
            _promise.promise(deferred);

            // Call given func if any
            if (func) {
                func.call(deferred, deferred);
            }

            // All done!
            return deferred;
        },

        // Deferred helper
        when: function when(singleValue) {
            var

			// count of uncompleted subordinates
			remaining = arguments.length,


			// count of unprocessed arguments
			i = remaining,


			// subordinate fulfillment data
			resolveContexts = Array(i),
			    resolveValues = _slice.call(arguments),


			// the master Deferred
			master = jQuery.Deferred(),


			// subordinate callback factory
			updateFunc = function updateFunc(i) {
			    return function (value) {
			        resolveContexts[i] = this;
			        resolveValues[i] = arguments.length > 1 ? _slice.call(arguments) : value;
			        if (! --remaining) {
			            master.resolveWith(resolveContexts, resolveValues);
			        }
			    };
			};

            // Single- and empty arguments are adopted like Promise.resolve
            if (remaining <= 1) {
                adoptValue(singleValue, master.done(updateFunc(i)).resolve, master.reject);

                // Use .then() to unwrap secondary thenables (cf. gh-3000)
                if (master.state() === "pending" || jQuery.isFunction(resolveValues[i] && resolveValues[i].then)) {

                    return master.then();
                }
            }

            // Multiple arguments are aggregated like Promise.all array elements
            while (i--) {
                adoptValue(resolveValues[i], updateFunc(i), master.reject);
            }

            return master.promise();
        }
    });

    // These usually indicate a programmer mistake during development,
    // warn about them ASAP rather than swallowing them by default.
    var rerrorNames = /^(Eval|Internal|Range|Reference|Syntax|Type|URI)Error$/;

    jQuery.Deferred.exceptionHook = function (error, stack) {

        // Support: IE 8 - 9 only
        // Console exists when dev tools are open, which can happen at any time
        if (window.console && window.console.warn && error && rerrorNames.test(error.name)) {
            window.console.warn("jQuery.Deferred exception: " + error.message, error.stack, stack);
        }
    };

    jQuery.readyException = function (error) {
        window.setTimeout(function () {
            throw error;
        });
    };

    // The deferred used on DOM ready
    var readyList = jQuery.Deferred();

    jQuery.fn.ready = function (fn) {

        readyList.then(fn)

		// Wrap jQuery.readyException in a function so that the lookup
		// happens at the time of error handling instead of callback
		// registration.
		.catch(function (error) {
		    jQuery.readyException(error);
		});

        return this;
    };

    jQuery.extend({

        // Is the DOM ready to be used? Set to true once it occurs.
        isReady: false,

        // A counter to track how many items to wait for before
        // the ready event fires. See #6781
        readyWait: 1,

        // Hold (or release) the ready event
        holdReady: function holdReady(hold) {
            if (hold) {
                jQuery.readyWait++;
            } else {
                jQuery.ready(true);
            }
        },

        // Handle when the DOM is ready
        ready: function ready(wait) {

            // Abort if there are pending holds or we're already ready
            if (wait === true ? --jQuery.readyWait : jQuery.isReady) {
                return;
            }

            // Remember that the DOM is ready
            jQuery.isReady = true;

            // If a normal DOM Ready event fired, decrement, and wait if need be
            if (wait !== true && --jQuery.readyWait > 0) {
                return;
            }

            // If there are functions bound, to execute
            readyList.resolveWith(document, [jQuery]);
        }
    });

    jQuery.ready.then = readyList.then;

    // The ready event handler and self cleanup method
    function completed() {
        document.removeEventListener("DOMContentLoaded", completed);
        window.removeEventListener("load", completed);
        jQuery.ready();
    }

    // Catch cases where $(document).ready() is called
    // after the browser event has already occurred.
    // Support: IE <=9 - 10 only
    // Older IE sometimes signals "interactive" too soon
    if (document.readyState === "complete" || document.readyState !== "loading" && !document.documentElement.doScroll) {

        // Handle it asynchronously to allow scripts the opportunity to delay ready
        window.setTimeout(jQuery.ready);
    } else {

        // Use the handy event callback
        document.addEventListener("DOMContentLoaded", completed);

        // A fallback to window.onload, that will always work
        window.addEventListener("load", completed);
    }

    // Multifunctional method to get and set values of a collection
    // The value/s can optionally be executed if it's a function
    var access = function access(elems, fn, key, value, chainable, emptyGet, raw) {
        var i = 0,
		    len = elems.length,
		    bulk = key == null;

        // Sets many values
        if (jQuery.type(key) === "object") {
            chainable = true;
            for (i in key) {
                access(elems, fn, i, key[i], true, emptyGet, raw);
            }

            // Sets one value
        } else if (value !== undefined) {
            chainable = true;

            if (!jQuery.isFunction(value)) {
                raw = true;
            }

            if (bulk) {

                // Bulk operations run against the entire set
                if (raw) {
                    fn.call(elems, value);
                    fn = null;

                    // ...except when executing function values
                } else {
                    bulk = fn;
                    fn = function fn(elem, key, value) {
                        return bulk.call(jQuery(elem), value);
                    };
                }
            }

            if (fn) {
                for (; i < len; i++) {
                    fn(elems[i], key, raw ? value : value.call(elems[i], i, fn(elems[i], key)));
                }
            }
        }

        if (chainable) {
            return elems;
        }

        // Gets
        if (bulk) {
            return fn.call(elems);
        }

        return len ? fn(elems[0], key) : emptyGet;
    };
    var acceptData = function acceptData(owner) {

        // Accepts only:
        //  - Node
        //    - Node.ELEMENT_NODE
        //    - Node.DOCUMENT_NODE
        //  - Object
        //    - Any
        return owner.nodeType === 1 || owner.nodeType === 9 || !+owner.nodeType;
    };

    function Data() {
        this.expando = jQuery.expando + Data.uid++;
    }

    Data.uid = 1;

    Data.prototype = {

        cache: function cache(owner) {

            // Check if the owner object already has a cache
            var value = owner[this.expando];

            // If not, create one
            if (!value) {
                value = {};

                // We can accept data for non-element nodes in modern browsers,
                // but we should not, see #8335.
                // Always return an empty object.
                if (acceptData(owner)) {

                    // If it is a node unlikely to be stringify-ed or looped over
                    // use plain assignment
                    if (owner.nodeType) {
                        owner[this.expando] = value;

                        // Otherwise secure it in a non-enumerable property
                        // configurable must be true to allow the property to be
                        // deleted when data is removed
                    } else {
                        Object.defineProperty(owner, this.expando, {
                            value: value,
                            configurable: true
                        });
                    }
                }
            }

            return value;
        },
        set: function set(owner, data, value) {
            var prop,
			    cache = this.cache(owner);

            // Handle: [ owner, key, value ] args
            // Always use camelCase key (gh-2257)
            if (typeof data === "string") {
                cache[jQuery.camelCase(data)] = value;

                // Handle: [ owner, { properties } ] args
            } else {

                // Copy the properties one-by-one to the cache object
                for (prop in data) {
                    cache[jQuery.camelCase(prop)] = data[prop];
                }
            }
            return cache;
        },
        get: function get(owner, key) {
            return key === undefined ? this.cache(owner) :

			// Always use camelCase key (gh-2257)
			owner[this.expando] && owner[this.expando][jQuery.camelCase(key)];
        },
        access: function access(owner, key, value) {

            // In cases where either:
            //
            //   1. No key was specified
            //   2. A string key was specified, but no value provided
            //
            // Take the "read" path and allow the get method to determine
            // which value to return, respectively either:
            //
            //   1. The entire cache object
            //   2. The data stored at the key
            //
            if (key === undefined || key && typeof key === "string" && value === undefined) {

                return this.get(owner, key);
            }

            // When the key is not a string, or both a key and value
            // are specified, set or extend (existing objects) with either:
            //
            //   1. An object of properties
            //   2. A key and value
            //
            this.set(owner, key, value);

            // Since the "set" path can have two possible entry points
            // return the expected data based on which path was taken[*]
            return value !== undefined ? value : key;
        },
        remove: function remove(owner, key) {
            var i,
			    cache = owner[this.expando];

            if (cache === undefined) {
                return;
            }

            if (key !== undefined) {

                // Support array or space separated string of keys
                if (jQuery.isArray(key)) {

                    // If key is an array of keys...
                    // We always set camelCase keys, so remove that.
                    key = key.map(jQuery.camelCase);
                } else {
                    key = jQuery.camelCase(key);

                    // If a key with the spaces exists, use it.
                    // Otherwise, create an array by matching non-whitespace
                    key = key in cache ? [key] : key.match(rnothtmlwhite) || [];
                }

                i = key.length;

                while (i--) {
                    delete cache[key[i]];
                }
            }

            // Remove the expando if there's no more data
            if (key === undefined || jQuery.isEmptyObject(cache)) {

                // Support: Chrome <=35 - 45
                // Webkit & Blink performance suffers when deleting properties
                // from DOM nodes, so set to undefined instead
                // https://bugs.chromium.org/p/chromium/issues/detail?id=378607 (bug restricted)
                if (owner.nodeType) {
                    owner[this.expando] = undefined;
                } else {
                    delete owner[this.expando];
                }
            }
        },
        hasData: function hasData(owner) {
            var cache = owner[this.expando];
            return cache !== undefined && !jQuery.isEmptyObject(cache);
        }
    };
    var dataPriv = new Data();

    var dataUser = new Data();

    //	Implementation Summary
    //
    //	1. Enforce API surface and semantic compatibility with 1.9.x branch
    //	2. Improve the module's maintainability by reducing the storage
    //		paths to a single mechanism.
    //	3. Use the same single mechanism to support "private" and "user" data.
    //	4. _Never_ expose "private" data to user code (TODO: Drop _data, _removeData)
    //	5. Avoid exposing implementation details on user objects (eg. expando properties)
    //	6. Provide a clear path for implementation upgrade to WeakMap in 2014

    var rbrace = /^(?:\{[\w\W]*\}|\[[\w\W]*\])$/,
	    rmultiDash = /[A-Z]/g;

    function getData(data) {
        if (data === "true") {
            return true;
        }

        if (data === "false") {
            return false;
        }

        if (data === "null") {
            return null;
        }

        // Only convert to a number if it doesn't change the string
        if (data === +data + "") {
            return +data;
        }

        if (rbrace.test(data)) {
            return JSON.parse(data);
        }

        return data;
    }

    function dataAttr(elem, key, data) {
        var name;

        // If nothing was found internally, try to fetch any
        // data from the HTML5 data-* attribute
        if (data === undefined && elem.nodeType === 1) {
            name = "data-" + key.replace(rmultiDash, "-$&").toLowerCase();
            data = elem.getAttribute(name);

            if (typeof data === "string") {
                try {
                    data = getData(data);
                } catch (e) { }

                // Make sure we set the data so it isn't changed later
                dataUser.set(elem, key, data);
            } else {
                data = undefined;
            }
        }
        return data;
    }

    jQuery.extend({
        hasData: function hasData(elem) {
            return dataUser.hasData(elem) || dataPriv.hasData(elem);
        },

        data: function data(elem, name, _data) {
            return dataUser.access(elem, name, _data);
        },

        removeData: function removeData(elem, name) {
            dataUser.remove(elem, name);
        },

        // TODO: Now that all calls to _data and _removeData have been replaced
        // with direct calls to dataPriv methods, these can be deprecated.
        _data: function _data(elem, name, data) {
            return dataPriv.access(elem, name, data);
        },

        _removeData: function _removeData(elem, name) {
            dataPriv.remove(elem, name);
        }
    });

    jQuery.fn.extend({
        data: function data(key, value) {
            var i,
			    name,
			    data,
			    elem = this[0],
			    attrs = elem && elem.attributes;

            // Gets all values
            if (key === undefined) {
                if (this.length) {
                    data = dataUser.get(elem);

                    if (elem.nodeType === 1 && !dataPriv.get(elem, "hasDataAttrs")) {
                        i = attrs.length;
                        while (i--) {

                            // Support: IE 11 only
                            // The attrs elements can be null (#14894)
                            if (attrs[i]) {
                                name = attrs[i].name;
                                if (name.indexOf("data-") === 0) {
                                    name = jQuery.camelCase(name.slice(5));
                                    dataAttr(elem, name, data[name]);
                                }
                            }
                        }
                        dataPriv.set(elem, "hasDataAttrs", true);
                    }
                }

                return data;
            }

            // Sets multiple values
            if ((typeof key === "undefined" ? "undefined" : _typeof(key)) === "object") {
                return this.each(function () {
                    dataUser.set(this, key);
                });
            }

            return access(this, function (value) {
                var data;

                // The calling jQuery object (element matches) is not empty
                // (and therefore has an element appears at this[ 0 ]) and the
                // `value` parameter was not undefined. An empty jQuery object
                // will result in `undefined` for elem = this[ 0 ] which will
                // throw an exception if an attempt to read a data cache is made.
                if (elem && value === undefined) {

                    // Attempt to get data from the cache
                    // The key will always be camelCased in Data
                    data = dataUser.get(elem, key);
                    if (data !== undefined) {
                        return data;
                    }

                    // Attempt to "discover" the data in
                    // HTML5 custom data-* attrs
                    data = dataAttr(elem, key);
                    if (data !== undefined) {
                        return data;
                    }

                    // We tried really hard, but the data doesn't exist.
                    return;
                }

                // Set the data...
                this.each(function () {

                    // We always store the camelCased key
                    dataUser.set(this, key, value);
                });
            }, null, value, arguments.length > 1, null, true);
        },

        removeData: function removeData(key) {
            return this.each(function () {
                dataUser.remove(this, key);
            });
        }
    });

    jQuery.extend({
        queue: function queue(elem, type, data) {
            var queue;

            if (elem) {
                type = (type || "fx") + "queue";
                queue = dataPriv.get(elem, type);

                // Speed up dequeue by getting out quickly if this is just a lookup
                if (data) {
                    if (!queue || jQuery.isArray(data)) {
                        queue = dataPriv.access(elem, type, jQuery.makeArray(data));
                    } else {
                        queue.push(data);
                    }
                }
                return queue || [];
            }
        },

        dequeue: function dequeue(elem, type) {
            type = type || "fx";

            var queue = jQuery.queue(elem, type),
			    startLength = queue.length,
			    fn = queue.shift(),
			    hooks = jQuery._queueHooks(elem, type),
			    next = function next() {
			        jQuery.dequeue(elem, type);
			    };

            // If the fx queue is dequeued, always remove the progress sentinel
            if (fn === "inprogress") {
                fn = queue.shift();
                startLength--;
            }

            if (fn) {

                // Add a progress sentinel to prevent the fx queue from being
                // automatically dequeued
                if (type === "fx") {
                    queue.unshift("inprogress");
                }

                // Clear up the last queue stop function
                delete hooks.stop;
                fn.call(elem, next, hooks);
            }

            if (!startLength && hooks) {
                hooks.empty.fire();
            }
        },

        // Not public - generate a queueHooks object, or return the current one
        _queueHooks: function _queueHooks(elem, type) {
            var key = type + "queueHooks";
            return dataPriv.get(elem, key) || dataPriv.access(elem, key, {
                empty: jQuery.Callbacks("once memory").add(function () {
                    dataPriv.remove(elem, [type + "queue", key]);
                })
            });
        }
    });

    jQuery.fn.extend({
        queue: function queue(type, data) {
            var setter = 2;

            if (typeof type !== "string") {
                data = type;
                type = "fx";
                setter--;
            }

            if (arguments.length < setter) {
                return jQuery.queue(this[0], type);
            }

            return data === undefined ? this : this.each(function () {
                var queue = jQuery.queue(this, type, data);

                // Ensure a hooks for this queue
                jQuery._queueHooks(this, type);

                if (type === "fx" && queue[0] !== "inprogress") {
                    jQuery.dequeue(this, type);
                }
            });
        },
        dequeue: function dequeue(type) {
            return this.each(function () {
                jQuery.dequeue(this, type);
            });
        },
        clearQueue: function clearQueue(type) {
            return this.queue(type || "fx", []);
        },

        // Get a promise resolved when queues of a certain type
        // are emptied (fx is the type by default)
        promise: function promise(type, obj) {
            var tmp,
			    count = 1,
			    defer = jQuery.Deferred(),
			    elements = this,
			    i = this.length,
			    resolve = function resolve() {
			        if (! --count) {
			            defer.resolveWith(elements, [elements]);
			        }
			    };

            if (typeof type !== "string") {
                obj = type;
                type = undefined;
            }
            type = type || "fx";

            while (i--) {
                tmp = dataPriv.get(elements[i], type + "queueHooks");
                if (tmp && tmp.empty) {
                    count++;
                    tmp.empty.add(resolve);
                }
            }
            resolve();
            return defer.promise(obj);
        }
    });
    var pnum = /[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source;

    var rcssNum = new RegExp("^(?:([+-])=|)(" + pnum + ")([a-z%]*)$", "i");

    var cssExpand = ["Top", "Right", "Bottom", "Left"];

    var isHiddenWithinTree = function isHiddenWithinTree(elem, el) {

        // isHiddenWithinTree might be called from jQuery#filter function;
        // in that case, element will be second argument
        elem = el || elem;

        // Inline style trumps all
        return elem.style.display === "none" || elem.style.display === "" &&

		// Otherwise, check computed style
		// Support: Firefox <=43 - 45
		// Disconnected elements can have computed display: none, so first confirm that elem is
		// in the document.
		jQuery.contains(elem.ownerDocument, elem) && jQuery.css(elem, "display") === "none";
    };

    var swap = function swap(elem, options, callback, args) {
        var ret,
		    name,
		    old = {};

        // Remember the old values, and insert the new ones
        for (name in options) {
            old[name] = elem.style[name];
            elem.style[name] = options[name];
        }

        ret = callback.apply(elem, args || []);

        // Revert the old values
        for (name in options) {
            elem.style[name] = old[name];
        }

        return ret;
    };

    function adjustCSS(elem, prop, valueParts, tween) {
        var adjusted,
		    scale = 1,
		    maxIterations = 20,
		    currentValue = tween ? function () {
		        return tween.cur();
		    } : function () {
		        return jQuery.css(elem, prop, "");
		    },
		    initial = currentValue(),
		    unit = valueParts && valueParts[3] || (jQuery.cssNumber[prop] ? "" : "px"),


		// Starting value computation is required for potential unit mismatches
		initialInUnit = (jQuery.cssNumber[prop] || unit !== "px" && +initial) && rcssNum.exec(jQuery.css(elem, prop));

        if (initialInUnit && initialInUnit[3] !== unit) {

            // Trust units reported by jQuery.css
            unit = unit || initialInUnit[3];

            // Make sure we update the tween properties later on
            valueParts = valueParts || [];

            // Iteratively approximate from a nonzero starting point
            initialInUnit = +initial || 1;

            do {

                // If previous iteration zeroed out, double until we get *something*.
                // Use string for doubling so we don't accidentally see scale as unchanged below
                scale = scale || ".5";

                // Adjust and apply
                initialInUnit = initialInUnit / scale;
                jQuery.style(elem, prop, initialInUnit + unit);

                // Update scale, tolerating zero or NaN from tween.cur()
                // Break the loop if scale is unchanged or perfect, or if we've just had enough.
            } while (scale !== (scale = currentValue() / initial) && scale !== 1 && --maxIterations);
        }

        if (valueParts) {
            initialInUnit = +initialInUnit || +initial || 0;

            // Apply relative offset (+=/-=) if specified
            adjusted = valueParts[1] ? initialInUnit + (valueParts[1] + 1) * valueParts[2] : +valueParts[2];
            if (tween) {
                tween.unit = unit;
                tween.start = initialInUnit;
                tween.end = adjusted;
            }
        }
        return adjusted;
    }

    var defaultDisplayMap = {};

    function getDefaultDisplay(elem) {
        var temp,
		    doc = elem.ownerDocument,
		    nodeName = elem.nodeName,
		    display = defaultDisplayMap[nodeName];

        if (display) {
            return display;
        }

        temp = doc.body.appendChild(doc.createElement(nodeName));
        display = jQuery.css(temp, "display");

        temp.parentNode.removeChild(temp);

        if (display === "none") {
            display = "block";
        }
        defaultDisplayMap[nodeName] = display;

        return display;
    }

    function showHide(elements, show) {
        var display,
		    elem,
		    values = [],
		    index = 0,
		    length = elements.length;

        // Determine new display value for elements that need to change
        for (; index < length; index++) {
            elem = elements[index];
            if (!elem.style) {
                continue;
            }

            display = elem.style.display;
            if (show) {

                // Since we force visibility upon cascade-hidden elements, an immediate (and slow)
                // check is required in this first loop unless we have a nonempty display value (either
                // inline or about-to-be-restored)
                if (display === "none") {
                    values[index] = dataPriv.get(elem, "display") || null;
                    if (!values[index]) {
                        elem.style.display = "";
                    }
                }
                if (elem.style.display === "" && isHiddenWithinTree(elem)) {
                    values[index] = getDefaultDisplay(elem);
                }
            } else {
                if (display !== "none") {
                    values[index] = "none";

                    // Remember what we're overwriting
                    dataPriv.set(elem, "display", display);
                }
            }
        }

        // Set the display of the elements in a second loop to avoid constant reflow
        for (index = 0; index < length; index++) {
            if (values[index] != null) {
                elements[index].style.display = values[index];
            }
        }

        return elements;
    }

    jQuery.fn.extend({
        show: function show() {
            return showHide(this, true);
        },
        hide: function hide() {
            return showHide(this);
        },
        toggle: function toggle(state) {
            if (typeof state === "boolean") {
                return state ? this.show() : this.hide();
            }

            return this.each(function () {
                if (isHiddenWithinTree(this)) {
                    jQuery(this).show();
                } else {
                    jQuery(this).hide();
                }
            });
        }
    });
    var rcheckableType = /^(?:checkbox|radio)$/i;

    var rtagName = /<([a-z][^\/\0>\x20\t\r\n\f]+)/i;

    var rscriptType = /^$|\/(?:java|ecma)script/i;

    // We have to close these tags to support XHTML (#13200)
    var wrapMap = {

        // Support: IE <=9 only
        option: [1, "<select multiple='multiple'>", "</select>"],

        // XHTML parsers do not magically insert elements in the
        // same way that tag soup parsers do. So we cannot shorten
        // this by omitting <tbody> or other required elements.
        thead: [1, "<table>", "</table>"],
        col: [2, "<table><colgroup>", "</colgroup></table>"],
        tr: [2, "<table><tbody>", "</tbody></table>"],
        td: [3, "<table><tbody><tr>", "</tr></tbody></table>"],

        _default: [0, "", ""]
    };

    // Support: IE <=9 only
    wrapMap.optgroup = wrapMap.option;

    wrapMap.tbody = wrapMap.tfoot = wrapMap.colgroup = wrapMap.caption = wrapMap.thead;
    wrapMap.th = wrapMap.td;

    function getAll(context, tag) {

        // Support: IE <=9 - 11 only
        // Use typeof to avoid zero-argument method invocation on host objects (#15151)
        var ret;

        if (typeof context.getElementsByTagName !== "undefined") {
            ret = context.getElementsByTagName(tag || "*");
        } else if (typeof context.querySelectorAll !== "undefined") {
            ret = context.querySelectorAll(tag || "*");
        } else {
            ret = [];
        }

        if (tag === undefined || tag && jQuery.nodeName(context, tag)) {
            return jQuery.merge([context], ret);
        }

        return ret;
    }

    // Mark scripts as having already been evaluated
    function setGlobalEval(elems, refElements) {
        var i = 0,
		    l = elems.length;

        for (; i < l; i++) {
            dataPriv.set(elems[i], "globalEval", !refElements || dataPriv.get(refElements[i], "globalEval"));
        }
    }

    var rhtml = /<|&#?\w+;/;

    function buildFragment(elems, context, scripts, selection, ignored) {
        var elem,
		    tmp,
		    tag,
		    wrap,
		    contains,
		    j,
		    fragment = context.createDocumentFragment(),
		    nodes = [],
		    i = 0,
		    l = elems.length;

        for (; i < l; i++) {
            elem = elems[i];

            if (elem || elem === 0) {

                // Add nodes directly
                if (jQuery.type(elem) === "object") {

                    // Support: Android <=4.0 only, PhantomJS 1 only
                    // push.apply(_, arraylike) throws on ancient WebKit
                    jQuery.merge(nodes, elem.nodeType ? [elem] : elem);

                    // Convert non-html into a text node
                } else if (!rhtml.test(elem)) {
                    nodes.push(context.createTextNode(elem));

                    // Convert html into DOM nodes
                } else {
                    tmp = tmp || fragment.appendChild(context.createElement("div"));

                    // Deserialize a standard representation
                    tag = (rtagName.exec(elem) || ["", ""])[1].toLowerCase();
                    wrap = wrapMap[tag] || wrapMap._default;
                    tmp.innerHTML = wrap[1] + jQuery.htmlPrefilter(elem) + wrap[2];

                    // Descend through wrappers to the right content
                    j = wrap[0];
                    while (j--) {
                        tmp = tmp.lastChild;
                    }

                    // Support: Android <=4.0 only, PhantomJS 1 only
                    // push.apply(_, arraylike) throws on ancient WebKit
                    jQuery.merge(nodes, tmp.childNodes);

                    // Remember the top-level container
                    tmp = fragment.firstChild;

                    // Ensure the created nodes are orphaned (#12392)
                    tmp.textContent = "";
                }
            }
        }

        // Remove wrapper from fragment
        fragment.textContent = "";

        i = 0;
        while (elem = nodes[i++]) {

            // Skip elements already in the context collection (trac-4087)
            if (selection && jQuery.inArray(elem, selection) > -1) {
                if (ignored) {
                    ignored.push(elem);
                }
                continue;
            }

            contains = jQuery.contains(elem.ownerDocument, elem);

            // Append to fragment
            tmp = getAll(fragment.appendChild(elem), "script");

            // Preserve script evaluation history
            if (contains) {
                setGlobalEval(tmp);
            }

            // Capture executables
            if (scripts) {
                j = 0;
                while (elem = tmp[j++]) {
                    if (rscriptType.test(elem.type || "")) {
                        scripts.push(elem);
                    }
                }
            }
        }

        return fragment;
    }

    (function () {
        var fragment = document.createDocumentFragment(),
		    div = fragment.appendChild(document.createElement("div")),
		    input = document.createElement("input");

        // Support: Android 4.0 - 4.3 only
        // Check state lost if the name is set (#11217)
        // Support: Windows Web Apps (WWA)
        // `name` and `type` must use .setAttribute for WWA (#14901)
        input.setAttribute("type", "radio");
        input.setAttribute("checked", "checked");
        input.setAttribute("name", "t");

        div.appendChild(input);

        // Support: Android <=4.1 only
        // Older WebKit doesn't clone checked state correctly in fragments
        support.checkClone = div.cloneNode(true).cloneNode(true).lastChild.checked;

        // Support: IE <=11 only
        // Make sure textarea (and checkbox) defaultValue is properly cloned
        div.innerHTML = "<textarea>x</textarea>";
        support.noCloneChecked = !!div.cloneNode(true).lastChild.defaultValue;
    })();
    var documentElement = document.documentElement;

    var rkeyEvent = /^key/,
	    rmouseEvent = /^(?:mouse|pointer|contextmenu|drag|drop)|click/,
	    rtypenamespace = /^([^.]*)(?:\.(.+)|)/;

    function returnTrue() {
        return true;
    }

    function returnFalse() {
        return false;
    }

    // Support: IE <=9 only
    // See #13393 for more info
    function safeActiveElement() {
        try {
            return document.activeElement;
        } catch (err) { }
    }

    function _on(elem, types, selector, data, fn, one) {
        var origFn, type;

        // Types can be a map of types/handlers
        if ((typeof types === "undefined" ? "undefined" : _typeof(types)) === "object") {

            // ( types-Object, selector, data )
            if (typeof selector !== "string") {

                // ( types-Object, data )
                data = data || selector;
                selector = undefined;
            }
            for (type in types) {
                _on(elem, type, selector, data, types[type], one);
            }
            return elem;
        }

        if (data == null && fn == null) {

            // ( types, fn )
            fn = selector;
            data = selector = undefined;
        } else if (fn == null) {
            if (typeof selector === "string") {

                // ( types, selector, fn )
                fn = data;
                data = undefined;
            } else {

                // ( types, data, fn )
                fn = data;
                data = selector;
                selector = undefined;
            }
        }
        if (fn === false) {
            fn = returnFalse;
        } else if (!fn) {
            return elem;
        }

        if (one === 1) {
            origFn = fn;
            fn = function fn(event) {

                // Can use an empty set, since event contains the info
                jQuery().off(event);
                return origFn.apply(this, arguments);
            };

            // Use same guid so caller can remove using origFn
            fn.guid = origFn.guid || (origFn.guid = jQuery.guid++);
        }
        return elem.each(function () {
            jQuery.event.add(this, types, fn, data, selector);
        });
    }

    /*
  * Helper functions for managing events -- not part of the public interface.
  * Props to Dean Edwards' addEvent library for many of the ideas.
  */
    jQuery.event = {

        global: {},

        add: function add(elem, types, handler, data, selector) {

            var handleObjIn,
			    eventHandle,
			    tmp,
			    events,
			    t,
			    handleObj,
			    special,
			    handlers,
			    type,
			    namespaces,
			    origType,
			    elemData = dataPriv.get(elem);

            // Don't attach events to noData or text/comment nodes (but allow plain objects)
            if (!elemData) {
                return;
            }

            // Caller can pass in an object of custom data in lieu of the handler
            if (handler.handler) {
                handleObjIn = handler;
                handler = handleObjIn.handler;
                selector = handleObjIn.selector;
            }

            // Ensure that invalid selectors throw exceptions at attach time
            // Evaluate against documentElement in case elem is a non-element node (e.g., document)
            if (selector) {
                jQuery.find.matchesSelector(documentElement, selector);
            }

            // Make sure that the handler has a unique ID, used to find/remove it later
            if (!handler.guid) {
                handler.guid = jQuery.guid++;
            }

            // Init the element's event structure and main handler, if this is the first
            if (!(events = elemData.events)) {
                events = elemData.events = {};
            }
            if (!(eventHandle = elemData.handle)) {
                eventHandle = elemData.handle = function (e) {

                    // Discard the second event of a jQuery.event.trigger() and
                    // when an event is called after a page has unloaded
                    return typeof jQuery !== "undefined" && jQuery.event.triggered !== e.type ? jQuery.event.dispatch.apply(elem, arguments) : undefined;
                };
            }

            // Handle multiple events separated by a space
            types = (types || "").match(rnothtmlwhite) || [""];
            t = types.length;
            while (t--) {
                tmp = rtypenamespace.exec(types[t]) || [];
                type = origType = tmp[1];
                namespaces = (tmp[2] || "").split(".").sort();

                // There *must* be a type, no attaching namespace-only handlers
                if (!type) {
                    continue;
                }

                // If event changes its type, use the special event handlers for the changed type
                special = jQuery.event.special[type] || {};

                // If selector defined, determine special event api type, otherwise given type
                type = (selector ? special.delegateType : special.bindType) || type;

                // Update special based on newly reset type
                special = jQuery.event.special[type] || {};

                // handleObj is passed to all event handlers
                handleObj = jQuery.extend({
                    type: type,
                    origType: origType,
                    data: data,
                    handler: handler,
                    guid: handler.guid,
                    selector: selector,
                    needsContext: selector && jQuery.expr.match.needsContext.test(selector),
                    namespace: namespaces.join(".")
                }, handleObjIn);

                // Init the event handler queue if we're the first
                if (!(handlers = events[type])) {
                    handlers = events[type] = [];
                    handlers.delegateCount = 0;

                    // Only use addEventListener if the special events handler returns false
                    if (!special.setup || special.setup.call(elem, data, namespaces, eventHandle) === false) {

                        if (elem.addEventListener) {
                            elem.addEventListener(type, eventHandle);
                        }
                    }
                }

                if (special.add) {
                    special.add.call(elem, handleObj);

                    if (!handleObj.handler.guid) {
                        handleObj.handler.guid = handler.guid;
                    }
                }

                // Add to the element's handler list, delegates in front
                if (selector) {
                    handlers.splice(handlers.delegateCount++, 0, handleObj);
                } else {
                    handlers.push(handleObj);
                }

                // Keep track of which events have ever been used, for event optimization
                jQuery.event.global[type] = true;
            }
        },

        // Detach an event or set of events from an element
        remove: function remove(elem, types, handler, selector, mappedTypes) {

            var j,
			    origCount,
			    tmp,
			    events,
			    t,
			    handleObj,
			    special,
			    handlers,
			    type,
			    namespaces,
			    origType,
			    elemData = dataPriv.hasData(elem) && dataPriv.get(elem);

            if (!elemData || !(events = elemData.events)) {
                return;
            }

            // Once for each type.namespace in types; type may be omitted
            types = (types || "").match(rnothtmlwhite) || [""];
            t = types.length;
            while (t--) {
                tmp = rtypenamespace.exec(types[t]) || [];
                type = origType = tmp[1];
                namespaces = (tmp[2] || "").split(".").sort();

                // Unbind all events (on this namespace, if provided) for the element
                if (!type) {
                    for (type in events) {
                        jQuery.event.remove(elem, type + types[t], handler, selector, true);
                    }
                    continue;
                }

                special = jQuery.event.special[type] || {};
                type = (selector ? special.delegateType : special.bindType) || type;
                handlers = events[type] || [];
                tmp = tmp[2] && new RegExp("(^|\\.)" + namespaces.join("\\.(?:.*\\.|)") + "(\\.|$)");

                // Remove matching events
                origCount = j = handlers.length;
                while (j--) {
                    handleObj = handlers[j];

                    if ((mappedTypes || origType === handleObj.origType) && (!handler || handler.guid === handleObj.guid) && (!tmp || tmp.test(handleObj.namespace)) && (!selector || selector === handleObj.selector || selector === "**" && handleObj.selector)) {
                        handlers.splice(j, 1);

                        if (handleObj.selector) {
                            handlers.delegateCount--;
                        }
                        if (special.remove) {
                            special.remove.call(elem, handleObj);
                        }
                    }
                }

                // Remove generic event handler if we removed something and no more handlers exist
                // (avoids potential for endless recursion during removal of special event handlers)
                if (origCount && !handlers.length) {
                    if (!special.teardown || special.teardown.call(elem, namespaces, elemData.handle) === false) {

                        jQuery.removeEvent(elem, type, elemData.handle);
                    }

                    delete events[type];
                }
            }

            // Remove data and the expando if it's no longer used
            if (jQuery.isEmptyObject(events)) {
                dataPriv.remove(elem, "handle events");
            }
        },

        dispatch: function dispatch(nativeEvent) {

            // Make a writable jQuery.Event from the native event object
            var event = jQuery.event.fix(nativeEvent);

            var i,
			    j,
			    ret,
			    matched,
			    handleObj,
			    handlerQueue,
			    args = new Array(arguments.length),
			    handlers = (dataPriv.get(this, "events") || {})[event.type] || [],
			    special = jQuery.event.special[event.type] || {};

            // Use the fix-ed jQuery.Event rather than the (read-only) native event
            args[0] = event;

            for (i = 1; i < arguments.length; i++) {
                args[i] = arguments[i];
            }

            event.delegateTarget = this;

            // Call the preDispatch hook for the mapped type, and let it bail if desired
            if (special.preDispatch && special.preDispatch.call(this, event) === false) {
                return;
            }

            // Determine handlers
            handlerQueue = jQuery.event.handlers.call(this, event, handlers);

            // Run delegates first; they may want to stop propagation beneath us
            i = 0;
            while ((matched = handlerQueue[i++]) && !event.isPropagationStopped()) {
                event.currentTarget = matched.elem;

                j = 0;
                while ((handleObj = matched.handlers[j++]) && !event.isImmediatePropagationStopped()) {

                    // Triggered event must either 1) have no namespace, or 2) have namespace(s)
                    // a subset or equal to those in the bound event (both can have no namespace).
                    if (!event.rnamespace || event.rnamespace.test(handleObj.namespace)) {

                        event.handleObj = handleObj;
                        event.data = handleObj.data;

                        ret = ((jQuery.event.special[handleObj.origType] || {}).handle || handleObj.handler).apply(matched.elem, args);

                        if (ret !== undefined) {
                            if ((event.result = ret) === false) {
                                event.preventDefault();
                                event.stopPropagation();
                            }
                        }
                    }
                }
            }

            // Call the postDispatch hook for the mapped type
            if (special.postDispatch) {
                special.postDispatch.call(this, event);
            }

            return event.result;
        },

        handlers: function handlers(event, _handlers) {
            var i,
			    handleObj,
			    sel,
			    matchedHandlers,
			    matchedSelectors,
			    handlerQueue = [],
			    delegateCount = _handlers.delegateCount,
			    cur = event.target;

            // Find delegate handlers
            if (delegateCount &&

                // Support: IE <=9
                // Black-hole SVG <use> instance trees (trac-13180)
			cur.nodeType &&

                // Support: Firefox <=42
                // Suppress spec-violating clicks indicating a non-primary pointer button (trac-3861)
                // https://www.w3.org/TR/DOM-Level-3-Events/#event-type-click
                // Support: IE 11 only
                // ...but not arrow key "clicks" of radio inputs, which can have `button` -1 (gh-2343)
			!(event.type === "click" && event.button >= 1)) {

                for (; cur !== this; cur = cur.parentNode || this) {

                    // Don't check non-elements (#13208)
                    // Don't process clicks on disabled elements (#6911, #8165, #11382, #11764)
                    if (cur.nodeType === 1 && !(event.type === "click" && cur.disabled === true)) {
                        matchedHandlers = [];
                        matchedSelectors = {};
                        for (i = 0; i < delegateCount; i++) {
                            handleObj = _handlers[i];

                            // Don't conflict with Object.prototype properties (#13203)
                            sel = handleObj.selector + " ";

                            if (matchedSelectors[sel] === undefined) {
                                matchedSelectors[sel] = handleObj.needsContext ? jQuery(sel, this).index(cur) > -1 : jQuery.find(sel, this, null, [cur]).length;
                            }
                            if (matchedSelectors[sel]) {
                                matchedHandlers.push(handleObj);
                            }
                        }
                        if (matchedHandlers.length) {
                            handlerQueue.push({ elem: cur, handlers: matchedHandlers });
                        }
                    }
                }
            }

            // Add the remaining (directly-bound) handlers
            cur = this;
            if (delegateCount < _handlers.length) {
                handlerQueue.push({ elem: cur, handlers: _handlers.slice(delegateCount) });
            }

            return handlerQueue;
        },

        addProp: function addProp(name, hook) {
            Object.defineProperty(jQuery.Event.prototype, name, {
                enumerable: true,
                configurable: true,

                get: jQuery.isFunction(hook) ? function () {
                    if (this.originalEvent) {
                        return hook(this.originalEvent);
                    }
                } : function () {
                    if (this.originalEvent) {
                        return this.originalEvent[name];
                    }
                },

                set: function set(value) {
                    Object.defineProperty(this, name, {
                        enumerable: true,
                        configurable: true,
                        writable: true,
                        value: value
                    });
                }
            });
        },

        fix: function fix(originalEvent) {
            return originalEvent[jQuery.expando] ? originalEvent : new jQuery.Event(originalEvent);
        },

        special: {
            load: {

                // Prevent triggered image.load events from bubbling to window.load
                noBubble: true
            },
            focus: {

                // Fire native event if possible so blur/focus sequence is correct
                trigger: function trigger() {
                    if (this !== safeActiveElement() && this.focus) {
                        this.focus();
                        return false;
                    }
                },
                delegateType: "focusin"
            },
            blur: {
                trigger: function trigger() {
                    if (this === safeActiveElement() && this.blur) {
                        this.blur();
                        return false;
                    }
                },
                delegateType: "focusout"
            },
            click: {

                // For checkbox, fire native event so checked state will be right
                trigger: function trigger() {
                    if (this.type === "checkbox" && this.click && jQuery.nodeName(this, "input")) {
                        this.click();
                        return false;
                    }
                },

                // For cross-browser consistency, don't fire native .click() on links
                _default: function _default(event) {
                    return jQuery.nodeName(event.target, "a");
                }
            },

            beforeunload: {
                postDispatch: function postDispatch(event) {

                    // Support: Firefox 20+
                    // Firefox doesn't alert if the returnValue field is not set.
                    if (event.result !== undefined && event.originalEvent) {
                        event.originalEvent.returnValue = event.result;
                    }
                }
            }
        }
    };

    jQuery.removeEvent = function (elem, type, handle) {

        // This "if" is needed for plain objects
        if (elem.removeEventListener) {
            elem.removeEventListener(type, handle);
        }
    };

    jQuery.Event = function (src, props) {

        // Allow instantiation without the 'new' keyword
        if (!(this instanceof jQuery.Event)) {
            return new jQuery.Event(src, props);
        }

        // Event object
        if (src && src.type) {
            this.originalEvent = src;
            this.type = src.type;

            // Events bubbling up the document may have been marked as prevented
            // by a handler lower down the tree; reflect the correct value.
            this.isDefaultPrevented = src.defaultPrevented || src.defaultPrevented === undefined &&

			// Support: Android <=2.3 only
			src.returnValue === false ? returnTrue : returnFalse;

            // Create target properties
            // Support: Safari <=6 - 7 only
            // Target should not be a text node (#504, #13143)
            this.target = src.target && src.target.nodeType === 3 ? src.target.parentNode : src.target;

            this.currentTarget = src.currentTarget;
            this.relatedTarget = src.relatedTarget;

            // Event type
        } else {
            this.type = src;
        }

        // Put explicitly provided properties onto the event object
        if (props) {
            jQuery.extend(this, props);
        }

        // Create a timestamp if incoming event doesn't have one
        this.timeStamp = src && src.timeStamp || jQuery.now();

        // Mark it as fixed
        this[jQuery.expando] = true;
    };

    // jQuery.Event is based on DOM3 Events as specified by the ECMAScript Language Binding
    // https://www.w3.org/TR/2003/WD-DOM-Level-3-Events-20030331/ecma-script-binding.html
    jQuery.Event.prototype = {
        constructor: jQuery.Event,
        isDefaultPrevented: returnFalse,
        isPropagationStopped: returnFalse,
        isImmediatePropagationStopped: returnFalse,
        isSimulated: false,

        preventDefault: function preventDefault() {
            var e = this.originalEvent;

            this.isDefaultPrevented = returnTrue;

            if (e && !this.isSimulated) {
                e.preventDefault();
            }
        },
        stopPropagation: function stopPropagation() {
            var e = this.originalEvent;

            this.isPropagationStopped = returnTrue;

            if (e && !this.isSimulated) {
                e.stopPropagation();
            }
        },
        stopImmediatePropagation: function stopImmediatePropagation() {
            var e = this.originalEvent;

            this.isImmediatePropagationStopped = returnTrue;

            if (e && !this.isSimulated) {
                e.stopImmediatePropagation();
            }

            this.stopPropagation();
        }
    };

    // Includes all common event props including KeyEvent and MouseEvent specific props
    jQuery.each({
        altKey: true,
        bubbles: true,
        cancelable: true,
        changedTouches: true,
        ctrlKey: true,
        detail: true,
        eventPhase: true,
        metaKey: true,
        pageX: true,
        pageY: true,
        shiftKey: true,
        view: true,
        "char": true,
        charCode: true,
        key: true,
        keyCode: true,
        button: true,
        buttons: true,
        clientX: true,
        clientY: true,
        offsetX: true,
        offsetY: true,
        pointerId: true,
        pointerType: true,
        screenX: true,
        screenY: true,
        targetTouches: true,
        toElement: true,
        touches: true,

        which: function which(event) {
            var button = event.button;

            // Add which for key events
            if (event.which == null && rkeyEvent.test(event.type)) {
                return event.charCode != null ? event.charCode : event.keyCode;
            }

            // Add which for click: 1 === left; 2 === middle; 3 === right
            if (!event.which && button !== undefined && rmouseEvent.test(event.type)) {
                if (button & 1) {
                    return 1;
                }

                if (button & 2) {
                    return 3;
                }

                if (button & 4) {
                    return 2;
                }

                return 0;
            }

            return event.which;
        }
    }, jQuery.event.addProp);

    // Create mouseenter/leave events using mouseover/out and event-time checks
    // so that event delegation works in jQuery.
    // Do the same for pointerenter/pointerleave and pointerover/pointerout
    //
    // Support: Safari 7 only
    // Safari sends mouseenter too often; see:
    // https://bugs.chromium.org/p/chromium/issues/detail?id=470258
    // for the description of the bug (it existed in older Chrome versions as well).
    jQuery.each({
        mouseenter: "mouseover",
        mouseleave: "mouseout",
        pointerenter: "pointerover",
        pointerleave: "pointerout"
    }, function (orig, fix) {
        jQuery.event.special[orig] = {
            delegateType: fix,
            bindType: fix,

            handle: function handle(event) {
                var ret,
				    target = this,
				    related = event.relatedTarget,
				    handleObj = event.handleObj;

                // For mouseenter/leave call the handler if related is outside the target.
                // NB: No relatedTarget if the mouse left/entered the browser window
                if (!related || related !== target && !jQuery.contains(target, related)) {
                    event.type = handleObj.origType;
                    ret = handleObj.handler.apply(this, arguments);
                    event.type = fix;
                }
                return ret;
            }
        };
    });

    jQuery.fn.extend({

        on: function on(types, selector, data, fn) {
            return _on(this, types, selector, data, fn);
        },
        one: function one(types, selector, data, fn) {
            return _on(this, types, selector, data, fn, 1);
        },
        off: function off(types, selector, fn) {
            var handleObj, type;
            if (types && types.preventDefault && types.handleObj) {

                // ( event )  dispatched jQuery.Event
                handleObj = types.handleObj;
                jQuery(types.delegateTarget).off(handleObj.namespace ? handleObj.origType + "." + handleObj.namespace : handleObj.origType, handleObj.selector, handleObj.handler);
                return this;
            }
            if ((typeof types === "undefined" ? "undefined" : _typeof(types)) === "object") {

                // ( types-object [, selector] )
                for (type in types) {
                    this.off(type, selector, types[type]);
                }
                return this;
            }
            if (selector === false || typeof selector === "function") {

                // ( types [, fn] )
                fn = selector;
                selector = undefined;
            }
            if (fn === false) {
                fn = returnFalse;
            }
            return this.each(function () {
                jQuery.event.remove(this, types, fn, selector);
            });
        }
    });

    var

	/* eslint-disable max-len */

	// See https://github.com/eslint/eslint/issues/3229
	rxhtmlTag = /<(?!area|br|col|embed|hr|img|input|link|meta|param)(([a-z][^\/\0>\x20\t\r\n\f]*)[^>]*)\/>/gi,


	/* eslint-enable */

	// Support: IE <=10 - 11, Edge 12 - 13
	// In IE/Edge using regex groups here causes severe slowdowns.
	// See https://connect.microsoft.com/IE/feedback/details/1736512/
	rnoInnerhtml = /<script|<style|<link/i,


	// checked="checked" or checked
	rchecked = /checked\s*(?:[^=]|=\s*.checked.)/i,
	    rscriptTypeMasked = /^true\/(.*)/,
	    rcleanScript = /^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g;

    function manipulationTarget(elem, content) {
        if (jQuery.nodeName(elem, "table") && jQuery.nodeName(content.nodeType !== 11 ? content : content.firstChild, "tr")) {

            return elem.getElementsByTagName("tbody")[0] || elem;
        }

        return elem;
    }

    // Replace/restore the type attribute of script elements for safe DOM manipulation
    function disableScript(elem) {
        elem.type = (elem.getAttribute("type") !== null) + "/" + elem.type;
        return elem;
    }
    function restoreScript(elem) {
        var match = rscriptTypeMasked.exec(elem.type);

        if (match) {
            elem.type = match[1];
        } else {
            elem.removeAttribute("type");
        }

        return elem;
    }

    function cloneCopyEvent(src, dest) {
        var i, l, type, pdataOld, pdataCur, udataOld, udataCur, events;

        if (dest.nodeType !== 1) {
            return;
        }

        // 1. Copy private data: events, handlers, etc.
        if (dataPriv.hasData(src)) {
            pdataOld = dataPriv.access(src);
            pdataCur = dataPriv.set(dest, pdataOld);
            events = pdataOld.events;

            if (events) {
                delete pdataCur.handle;
                pdataCur.events = {};

                for (type in events) {
                    for (i = 0, l = events[type].length; i < l; i++) {
                        jQuery.event.add(dest, type, events[type][i]);
                    }
                }
            }
        }

        // 2. Copy user data
        if (dataUser.hasData(src)) {
            udataOld = dataUser.access(src);
            udataCur = jQuery.extend({}, udataOld);

            dataUser.set(dest, udataCur);
        }
    }

    // Fix IE bugs, see support tests
    function fixInput(src, dest) {
        var nodeName = dest.nodeName.toLowerCase();

        // Fails to persist the checked state of a cloned checkbox or radio button.
        if (nodeName === "input" && rcheckableType.test(src.type)) {
            dest.checked = src.checked;

            // Fails to return the selected option to the default selected state when cloning options
        } else if (nodeName === "input" || nodeName === "textarea") {
            dest.defaultValue = src.defaultValue;
        }
    }

    function domManip(collection, args, callback, ignored) {

        // Flatten any nested arrays
        args = concat.apply([], args);

        var fragment,
		    first,
		    scripts,
		    hasScripts,
		    node,
		    doc,
		    i = 0,
		    l = collection.length,
		    iNoClone = l - 1,
		    value = args[0],
		    isFunction = jQuery.isFunction(value);

        // We can't cloneNode fragments that contain checked, in WebKit
        if (isFunction || l > 1 && typeof value === "string" && !support.checkClone && rchecked.test(value)) {
            return collection.each(function (index) {
                var self = collection.eq(index);
                if (isFunction) {
                    args[0] = value.call(this, index, self.html());
                }
                domManip(self, args, callback, ignored);
            });
        }

        if (l) {
            fragment = buildFragment(args, collection[0].ownerDocument, false, collection, ignored);
            first = fragment.firstChild;

            if (fragment.childNodes.length === 1) {
                fragment = first;
            }

            // Require either new content or an interest in ignored elements to invoke the callback
            if (first || ignored) {
                scripts = jQuery.map(getAll(fragment, "script"), disableScript);
                hasScripts = scripts.length;

                // Use the original fragment for the last item
                // instead of the first because it can end up
                // being emptied incorrectly in certain situations (#8070).
                for (; i < l; i++) {
                    node = fragment;

                    if (i !== iNoClone) {
                        node = jQuery.clone(node, true, true);

                        // Keep references to cloned scripts for later restoration
                        if (hasScripts) {

                            // Support: Android <=4.0 only, PhantomJS 1 only
                            // push.apply(_, arraylike) throws on ancient WebKit
                            jQuery.merge(scripts, getAll(node, "script"));
                        }
                    }

                    callback.call(collection[i], node, i);
                }

                if (hasScripts) {
                    doc = scripts[scripts.length - 1].ownerDocument;

                    // Reenable scripts
                    jQuery.map(scripts, restoreScript);

                    // Evaluate executable scripts on first document insertion
                    for (i = 0; i < hasScripts; i++) {
                        node = scripts[i];
                        if (rscriptType.test(node.type || "") && !dataPriv.access(node, "globalEval") && jQuery.contains(doc, node)) {

                            if (node.src) {

                                // Optional AJAX dependency, but won't run scripts if not present
                                if (jQuery._evalUrl) {
                                    jQuery._evalUrl(node.src);
                                }
                            } else {
                                DOMEval(node.textContent.replace(rcleanScript, ""), doc);
                            }
                        }
                    }
                }
            }
        }

        return collection;
    }

    function _remove(elem, selector, keepData) {
        var node,
		    nodes = selector ? jQuery.filter(selector, elem) : elem,
		    i = 0;

        for (; (node = nodes[i]) != null; i++) {
            if (!keepData && node.nodeType === 1) {
                jQuery.cleanData(getAll(node));
            }

            if (node.parentNode) {
                if (keepData && jQuery.contains(node.ownerDocument, node)) {
                    setGlobalEval(getAll(node, "script"));
                }
                node.parentNode.removeChild(node);
            }
        }

        return elem;
    }

    jQuery.extend({
        htmlPrefilter: function htmlPrefilter(html) {
            return html.replace(rxhtmlTag, "<$1></$2>");
        },

        clone: function clone(elem, dataAndEvents, deepDataAndEvents) {
            var i,
			    l,
			    srcElements,
			    destElements,
			    clone = elem.cloneNode(true),
			    inPage = jQuery.contains(elem.ownerDocument, elem);

            // Fix IE cloning issues
            if (!support.noCloneChecked && (elem.nodeType === 1 || elem.nodeType === 11) && !jQuery.isXMLDoc(elem)) {

                // We eschew Sizzle here for performance reasons: https://jsperf.com/getall-vs-sizzle/2
                destElements = getAll(clone);
                srcElements = getAll(elem);

                for (i = 0, l = srcElements.length; i < l; i++) {
                    fixInput(srcElements[i], destElements[i]);
                }
            }

            // Copy the events from the original to the clone
            if (dataAndEvents) {
                if (deepDataAndEvents) {
                    srcElements = srcElements || getAll(elem);
                    destElements = destElements || getAll(clone);

                    for (i = 0, l = srcElements.length; i < l; i++) {
                        cloneCopyEvent(srcElements[i], destElements[i]);
                    }
                } else {
                    cloneCopyEvent(elem, clone);
                }
            }

            // Preserve script evaluation history
            destElements = getAll(clone, "script");
            if (destElements.length > 0) {
                setGlobalEval(destElements, !inPage && getAll(elem, "script"));
            }

            // Return the cloned set
            return clone;
        },

        cleanData: function cleanData(elems) {
            var data,
			    elem,
			    type,
			    special = jQuery.event.special,
			    i = 0;

            for (; (elem = elems[i]) !== undefined; i++) {
                if (acceptData(elem)) {
                    if (data = elem[dataPriv.expando]) {
                        if (data.events) {
                            for (type in data.events) {
                                if (special[type]) {
                                    jQuery.event.remove(elem, type);

                                    // This is a shortcut to avoid jQuery.event.remove's overhead
                                } else {
                                    jQuery.removeEvent(elem, type, data.handle);
                                }
                            }
                        }

                        // Support: Chrome <=35 - 45+
                        // Assign undefined instead of using delete, see Data#remove
                        elem[dataPriv.expando] = undefined;
                    }
                    if (elem[dataUser.expando]) {

                        // Support: Chrome <=35 - 45+
                        // Assign undefined instead of using delete, see Data#remove
                        elem[dataUser.expando] = undefined;
                    }
                }
            }
        }
    });

    jQuery.fn.extend({
        detach: function detach(selector) {
            return _remove(this, selector, true);
        },

        remove: function remove(selector) {
            return _remove(this, selector);
        },

        text: function text(value) {
            return access(this, function (value) {
                return value === undefined ? jQuery.text(this) : this.empty().each(function () {
                    if (this.nodeType === 1 || this.nodeType === 11 || this.nodeType === 9) {
                        this.textContent = value;
                    }
                });
            }, null, value, arguments.length);
        },

        append: function append() {
            return domManip(this, arguments, function (elem) {
                if (this.nodeType === 1 || this.nodeType === 11 || this.nodeType === 9) {
                    var target = manipulationTarget(this, elem);
                    target.appendChild(elem);
                }
            });
        },

        prepend: function prepend() {
            return domManip(this, arguments, function (elem) {
                if (this.nodeType === 1 || this.nodeType === 11 || this.nodeType === 9) {
                    var target = manipulationTarget(this, elem);
                    target.insertBefore(elem, target.firstChild);
                }
            });
        },

        before: function before() {
            return domManip(this, arguments, function (elem) {
                if (this.parentNode) {
                    this.parentNode.insertBefore(elem, this);
                }
            });
        },

        after: function after() {
            return domManip(this, arguments, function (elem) {
                if (this.parentNode) {
                    this.parentNode.insertBefore(elem, this.nextSibling);
                }
            });
        },

        empty: function empty() {
            var elem,
			    i = 0;

            for (; (elem = this[i]) != null; i++) {
                if (elem.nodeType === 1) {

                    // Prevent memory leaks
                    jQuery.cleanData(getAll(elem, false));

                    // Remove any remaining nodes
                    elem.textContent = "";
                }
            }

            return this;
        },

        clone: function clone(dataAndEvents, deepDataAndEvents) {
            dataAndEvents = dataAndEvents == null ? false : dataAndEvents;
            deepDataAndEvents = deepDataAndEvents == null ? dataAndEvents : deepDataAndEvents;

            return this.map(function () {
                return jQuery.clone(this, dataAndEvents, deepDataAndEvents);
            });
        },

        html: function html(value) {
            return access(this, function (value) {
                var elem = this[0] || {},
				    i = 0,
				    l = this.length;

                if (value === undefined && elem.nodeType === 1) {
                    return elem.innerHTML;
                }

                // See if we can take a shortcut and just use innerHTML
                if (typeof value === "string" && !rnoInnerhtml.test(value) && !wrapMap[(rtagName.exec(value) || ["", ""])[1].toLowerCase()]) {

                    value = jQuery.htmlPrefilter(value);

                    try {
                        for (; i < l; i++) {
                            elem = this[i] || {};

                            // Remove element nodes and prevent memory leaks
                            if (elem.nodeType === 1) {
                                jQuery.cleanData(getAll(elem, false));
                                elem.innerHTML = value;
                            }
                        }

                        elem = 0;

                        // If using innerHTML throws an exception, use the fallback method
                    } catch (e) { }
                }

                if (elem) {
                    this.empty().append(value);
                }
            }, null, value, arguments.length);
        },

        replaceWith: function replaceWith() {
            var ignored = [];

            // Make the changes, replacing each non-ignored context element with the new content
            return domManip(this, arguments, function (elem) {
                var parent = this.parentNode;

                if (jQuery.inArray(this, ignored) < 0) {
                    jQuery.cleanData(getAll(this));
                    if (parent) {
                        parent.replaceChild(elem, this);
                    }
                }

                // Force callback invocation
            }, ignored);
        }
    });

    jQuery.each({
        appendTo: "append",
        prependTo: "prepend",
        insertBefore: "before",
        insertAfter: "after",
        replaceAll: "replaceWith"
    }, function (name, original) {
        jQuery.fn[name] = function (selector) {
            var elems,
			    ret = [],
			    insert = jQuery(selector),
			    last = insert.length - 1,
			    i = 0;

            for (; i <= last; i++) {
                elems = i === last ? this : this.clone(true);
                jQuery(insert[i])[original](elems);

                // Support: Android <=4.0 only, PhantomJS 1 only
                // .get() because push.apply(_, arraylike) throws on ancient WebKit
                push.apply(ret, elems.get());
            }

            return this.pushStack(ret);
        };
    });
    var rmargin = /^margin/;

    var rnumnonpx = new RegExp("^(" + pnum + ")(?!px)[a-z%]+$", "i");

    var getStyles = function getStyles(elem) {

        // Support: IE <=11 only, Firefox <=30 (#15098, #14150)
        // IE throws on elements created in popups
        // FF meanwhile throws on frame elements through "defaultView.getComputedStyle"
        var view = elem.ownerDocument.defaultView;

        if (!view || !view.opener) {
            view = window;
        }

        return view.getComputedStyle(elem);
    };

    (function () {

        // Executing both pixelPosition & boxSizingReliable tests require only one layout
        // so they're executed at the same time to save the second computation.
        function computeStyleTests() {

            // This is a singleton, we need to execute it only once
            if (!div) {
                return;
            }

            div.style.cssText = "box-sizing:border-box;" + "position:relative;display:block;" + "margin:auto;border:1px;padding:1px;" + "top:1%;width:50%";
            div.innerHTML = "";
            documentElement.appendChild(container);

            var divStyle = window.getComputedStyle(div);
            pixelPositionVal = divStyle.top !== "1%";

            // Support: Android 4.0 - 4.3 only, Firefox <=3 - 44
            reliableMarginLeftVal = divStyle.marginLeft === "2px";
            boxSizingReliableVal = divStyle.width === "4px";

            // Support: Android 4.0 - 4.3 only
            // Some styles come back with percentage values, even though they shouldn't
            div.style.marginRight = "50%";
            pixelMarginRightVal = divStyle.marginRight === "4px";

            documentElement.removeChild(container);

            // Nullify the div so it wouldn't be stored in the memory and
            // it will also be a sign that checks already performed
            div = null;
        }

        var pixelPositionVal,
		    boxSizingReliableVal,
		    pixelMarginRightVal,
		    reliableMarginLeftVal,
		    container = document.createElement("div"),
		    div = document.createElement("div");

        // Finish early in limited (non-browser) environments
        if (!div.style) {
            return;
        }

        // Support: IE <=9 - 11 only
        // Style of cloned element affects source element cloned (#8908)
        div.style.backgroundClip = "content-box";
        div.cloneNode(true).style.backgroundClip = "";
        support.clearCloneStyle = div.style.backgroundClip === "content-box";

        container.style.cssText = "border:0;width:8px;height:0;top:0;left:-9999px;" + "padding:0;margin-top:1px;position:absolute";
        container.appendChild(div);

        jQuery.extend(support, {
            pixelPosition: function pixelPosition() {
                computeStyleTests();
                return pixelPositionVal;
            },
            boxSizingReliable: function boxSizingReliable() {
                computeStyleTests();
                return boxSizingReliableVal;
            },
            pixelMarginRight: function pixelMarginRight() {
                computeStyleTests();
                return pixelMarginRightVal;
            },
            reliableMarginLeft: function reliableMarginLeft() {
                computeStyleTests();
                return reliableMarginLeftVal;
            }
        });
    })();

    function curCSS(elem, name, computed) {
        var width,
		    minWidth,
		    maxWidth,
		    ret,
		    style = elem.style;

        computed = computed || getStyles(elem);

        // Support: IE <=9 only
        // getPropertyValue is only needed for .css('filter') (#12537)
        if (computed) {
            ret = computed.getPropertyValue(name) || computed[name];

            if (ret === "" && !jQuery.contains(elem.ownerDocument, elem)) {
                ret = jQuery.style(elem, name);
            }

            // A tribute to the "awesome hack by Dean Edwards"
            // Android Browser returns percentage for some values,
            // but width seems to be reliably pixels.
            // This is against the CSSOM draft spec:
            // https://drafts.csswg.org/cssom/#resolved-values
            if (!support.pixelMarginRight() && rnumnonpx.test(ret) && rmargin.test(name)) {

                // Remember the original values
                width = style.width;
                minWidth = style.minWidth;
                maxWidth = style.maxWidth;

                // Put in the new values to get a computed value out
                style.minWidth = style.maxWidth = style.width = ret;
                ret = computed.width;

                // Revert the changed values
                style.width = width;
                style.minWidth = minWidth;
                style.maxWidth = maxWidth;
            }
        }

        return ret !== undefined ?

		// Support: IE <=9 - 11 only
		// IE returns zIndex value as an integer.
		ret + "" : ret;
    }

    function addGetHookIf(conditionFn, hookFn) {

        // Define the hook, we'll check on the first run if it's really needed.
        return {
            get: function get() {
                if (conditionFn()) {

                    // Hook not needed (or it's not possible to use it due
                    // to missing dependency), remove it.
                    delete this.get;
                    return;
                }

                // Hook needed; redefine it so that the support test is not executed again.
                return (this.get = hookFn).apply(this, arguments);
            }
        };
    }

    var

	// Swappable if display is none or starts with table
	// except "table", "table-cell", or "table-caption"
	// See here for display values: https://developer.mozilla.org/en-US/docs/CSS/display
	rdisplayswap = /^(none|table(?!-c[ea]).+)/,
	    cssShow = { position: "absolute", visibility: "hidden", display: "block" },
	    cssNormalTransform = {
	        letterSpacing: "0",
	        fontWeight: "400"
	    },
	    cssPrefixes = ["Webkit", "Moz", "ms"],
	    emptyStyle = document.createElement("div").style;

    // Return a css property mapped to a potentially vendor prefixed property
    function vendorPropName(name) {

        // Shortcut for names that are not vendor prefixed
        if (name in emptyStyle) {
            return name;
        }

        // Check for vendor prefixed names
        var capName = name[0].toUpperCase() + name.slice(1),
		    i = cssPrefixes.length;

        while (i--) {
            name = cssPrefixes[i] + capName;
            if (name in emptyStyle) {
                return name;
            }
        }
    }

    function setPositiveNumber(elem, value, subtract) {

        // Any relative (+/-) values have already been
        // normalized at this point
        var matches = rcssNum.exec(value);
        return matches ?

		// Guard against undefined "subtract", e.g., when used as in cssHooks
		Math.max(0, matches[2] - (subtract || 0)) + (matches[3] || "px") : value;
    }

    function augmentWidthOrHeight(elem, name, extra, isBorderBox, styles) {
        var i,
		    val = 0;

        // If we already have the right measurement, avoid augmentation
        if (extra === (isBorderBox ? "border" : "content")) {
            i = 4;

            // Otherwise initialize for horizontal or vertical properties
        } else {
            i = name === "width" ? 1 : 0;
        }

        for (; i < 4; i += 2) {

            // Both box models exclude margin, so add it if we want it
            if (extra === "margin") {
                val += jQuery.css(elem, extra + cssExpand[i], true, styles);
            }

            if (isBorderBox) {

                // border-box includes padding, so remove it if we want content
                if (extra === "content") {
                    val -= jQuery.css(elem, "padding" + cssExpand[i], true, styles);
                }

                // At this point, extra isn't border nor margin, so remove border
                if (extra !== "margin") {
                    val -= jQuery.css(elem, "border" + cssExpand[i] + "Width", true, styles);
                }
            } else {

                // At this point, extra isn't content, so add padding
                val += jQuery.css(elem, "padding" + cssExpand[i], true, styles);

                // At this point, extra isn't content nor padding, so add border
                if (extra !== "padding") {
                    val += jQuery.css(elem, "border" + cssExpand[i] + "Width", true, styles);
                }
            }
        }

        return val;
    }

    function getWidthOrHeight(elem, name, extra) {

        // Start with offset property, which is equivalent to the border-box value
        var val,
		    valueIsBorderBox = true,
		    styles = getStyles(elem),
		    isBorderBox = jQuery.css(elem, "boxSizing", false, styles) === "border-box";

        // Support: IE <=11 only
        // Running getBoundingClientRect on a disconnected node
        // in IE throws an error.
        if (elem.getClientRects().length) {
            val = elem.getBoundingClientRect()[name];
        }

        // Some non-html elements return undefined for offsetWidth, so check for null/undefined
        // svg - https://bugzilla.mozilla.org/show_bug.cgi?id=649285
        // MathML - https://bugzilla.mozilla.org/show_bug.cgi?id=491668
        if (val <= 0 || val == null) {

            // Fall back to computed then uncomputed css if necessary
            val = curCSS(elem, name, styles);
            if (val < 0 || val == null) {
                val = elem.style[name];
            }

            // Computed unit is not pixels. Stop here and return.
            if (rnumnonpx.test(val)) {
                return val;
            }

            // Check for style in case a browser which returns unreliable values
            // for getComputedStyle silently falls back to the reliable elem.style
            valueIsBorderBox = isBorderBox && (support.boxSizingReliable() || val === elem.style[name]);

            // Normalize "", auto, and prepare for extra
            val = parseFloat(val) || 0;
        }

        // Use the active box-sizing model to add/subtract irrelevant styles
        return val + augmentWidthOrHeight(elem, name, extra || (isBorderBox ? "border" : "content"), valueIsBorderBox, styles) + "px";
    }

    jQuery.extend({

        // Add in style property hooks for overriding the default
        // behavior of getting and setting a style property
        cssHooks: {
            opacity: {
                get: function get(elem, computed) {
                    if (computed) {

                        // We should always get a number back from opacity
                        var ret = curCSS(elem, "opacity");
                        return ret === "" ? "1" : ret;
                    }
                }
            }
        },

        // Don't automatically add "px" to these possibly-unitless properties
        cssNumber: {
            "animationIterationCount": true,
            "columnCount": true,
            "fillOpacity": true,
            "flexGrow": true,
            "flexShrink": true,
            "fontWeight": true,
            "lineHeight": true,
            "opacity": true,
            "order": true,
            "orphans": true,
            "widows": true,
            "zIndex": true,
            "zoom": true
        },

        // Add in properties whose names you wish to fix before
        // setting or getting the value
        cssProps: {
            "float": "cssFloat"
        },

        // Get and set the style property on a DOM Node
        style: function style(elem, name, value, extra) {

            // Don't set styles on text and comment nodes
            if (!elem || elem.nodeType === 3 || elem.nodeType === 8 || !elem.style) {
                return;
            }

            // Make sure that we're working with the right name
            var ret,
			    type,
			    hooks,
			    origName = jQuery.camelCase(name),
			    style = elem.style;

            name = jQuery.cssProps[origName] || (jQuery.cssProps[origName] = vendorPropName(origName) || origName);

            // Gets hook for the prefixed version, then unprefixed version
            hooks = jQuery.cssHooks[name] || jQuery.cssHooks[origName];

            // Check if we're setting a value
            if (value !== undefined) {
                type = typeof value === "undefined" ? "undefined" : _typeof(value);

                // Convert "+=" or "-=" to relative numbers (#7345)
                if (type === "string" && (ret = rcssNum.exec(value)) && ret[1]) {
                    value = adjustCSS(elem, name, ret);

                    // Fixes bug #9237
                    type = "number";
                }

                // Make sure that null and NaN values aren't set (#7116)
                if (value == null || value !== value) {
                    return;
                }

                // If a number was passed in, add the unit (except for certain CSS properties)
                if (type === "number") {
                    value += ret && ret[3] || (jQuery.cssNumber[origName] ? "" : "px");
                }

                // background-* props affect original clone's values
                if (!support.clearCloneStyle && value === "" && name.indexOf("background") === 0) {
                    style[name] = "inherit";
                }

                // If a hook was provided, use that value, otherwise just set the specified value
                if (!hooks || !("set" in hooks) || (value = hooks.set(elem, value, extra)) !== undefined) {

                    style[name] = value;
                }
            } else {

                // If a hook was provided get the non-computed value from there
                if (hooks && "get" in hooks && (ret = hooks.get(elem, false, extra)) !== undefined) {

                    return ret;
                }

                // Otherwise just get the value from the style object
                return style[name];
            }
        },

        css: function css(elem, name, extra, styles) {
            var val,
			    num,
			    hooks,
			    origName = jQuery.camelCase(name);

            // Make sure that we're working with the right name
            name = jQuery.cssProps[origName] || (jQuery.cssProps[origName] = vendorPropName(origName) || origName);

            // Try prefixed name followed by the unprefixed name
            hooks = jQuery.cssHooks[name] || jQuery.cssHooks[origName];

            // If a hook was provided get the computed value from there
            if (hooks && "get" in hooks) {
                val = hooks.get(elem, true, extra);
            }

            // Otherwise, if a way to get the computed value exists, use that
            if (val === undefined) {
                val = curCSS(elem, name, styles);
            }

            // Convert "normal" to computed value
            if (val === "normal" && name in cssNormalTransform) {
                val = cssNormalTransform[name];
            }

            // Make numeric if forced or a qualifier was provided and val looks numeric
            if (extra === "" || extra) {
                num = parseFloat(val);
                return extra === true || isFinite(num) ? num || 0 : val;
            }
            return val;
        }
    });

    jQuery.each(["height", "width"], function (i, name) {
        jQuery.cssHooks[name] = {
            get: function get(elem, computed, extra) {
                if (computed) {

                    // Certain elements can have dimension info if we invisibly show them
                    // but it must have a current display style that would benefit
                    return rdisplayswap.test(jQuery.css(elem, "display")) && (

					// Support: Safari 8+
					// Table columns in Safari have non-zero offsetWidth & zero
					// getBoundingClientRect().width unless display is changed.
					// Support: IE <=11 only
					// Running getBoundingClientRect on a disconnected node
					// in IE throws an error.
					!elem.getClientRects().length || !elem.getBoundingClientRect().width) ? swap(elem, cssShow, function () {
					    return getWidthOrHeight(elem, name, extra);
					}) : getWidthOrHeight(elem, name, extra);
                }
            },

            set: function set(elem, value, extra) {
                var matches,
				    styles = extra && getStyles(elem),
				    subtract = extra && augmentWidthOrHeight(elem, name, extra, jQuery.css(elem, "boxSizing", false, styles) === "border-box", styles);

                // Convert to pixels if value adjustment is needed
                if (subtract && (matches = rcssNum.exec(value)) && (matches[3] || "px") !== "px") {

                    elem.style[name] = value;
                    value = jQuery.css(elem, name);
                }

                return setPositiveNumber(elem, value, subtract);
            }
        };
    });

    jQuery.cssHooks.marginLeft = addGetHookIf(support.reliableMarginLeft, function (elem, computed) {
        if (computed) {
            return (parseFloat(curCSS(elem, "marginLeft")) || elem.getBoundingClientRect().left - swap(elem, { marginLeft: 0 }, function () {
                return elem.getBoundingClientRect().left;
            })) + "px";
        }
    });

    // These hooks are used by animate to expand properties
    jQuery.each({
        margin: "",
        padding: "",
        border: "Width"
    }, function (prefix, suffix) {
        jQuery.cssHooks[prefix + suffix] = {
            expand: function expand(value) {
                var i = 0,
				    expanded = {},


				// Assumes a single number if not a string
				parts = typeof value === "string" ? value.split(" ") : [value];

                for (; i < 4; i++) {
                    expanded[prefix + cssExpand[i] + suffix] = parts[i] || parts[i - 2] || parts[0];
                }

                return expanded;
            }
        };

        if (!rmargin.test(prefix)) {
            jQuery.cssHooks[prefix + suffix].set = setPositiveNumber;
        }
    });

    jQuery.fn.extend({
        css: function css(name, value) {
            return access(this, function (elem, name, value) {
                var styles,
				    len,
				    map = {},
				    i = 0;

                if (jQuery.isArray(name)) {
                    styles = getStyles(elem);
                    len = name.length;

                    for (; i < len; i++) {
                        map[name[i]] = jQuery.css(elem, name[i], false, styles);
                    }

                    return map;
                }

                return value !== undefined ? jQuery.style(elem, name, value) : jQuery.css(elem, name);
            }, name, value, arguments.length > 1);
        }
    });

    function Tween(elem, options, prop, end, easing) {
        return new Tween.prototype.init(elem, options, prop, end, easing);
    }
    jQuery.Tween = Tween;

    Tween.prototype = {
        constructor: Tween,
        init: function init(elem, options, prop, end, easing, unit) {
            this.elem = elem;
            this.prop = prop;
            this.easing = easing || jQuery.easing._default;
            this.options = options;
            this.start = this.now = this.cur();
            this.end = end;
            this.unit = unit || (jQuery.cssNumber[prop] ? "" : "px");
        },
        cur: function cur() {
            var hooks = Tween.propHooks[this.prop];

            return hooks && hooks.get ? hooks.get(this) : Tween.propHooks._default.get(this);
        },
        run: function run(percent) {
            var eased,
			    hooks = Tween.propHooks[this.prop];

            if (this.options.duration) {
                this.pos = eased = jQuery.easing[this.easing](percent, this.options.duration * percent, 0, 1, this.options.duration);
            } else {
                this.pos = eased = percent;
            }
            this.now = (this.end - this.start) * eased + this.start;

            if (this.options.step) {
                this.options.step.call(this.elem, this.now, this);
            }

            if (hooks && hooks.set) {
                hooks.set(this);
            } else {
                Tween.propHooks._default.set(this);
            }
            return this;
        }
    };

    Tween.prototype.init.prototype = Tween.prototype;

    Tween.propHooks = {
        _default: {
            get: function get(tween) {
                var result;

                // Use a property on the element directly when it is not a DOM element,
                // or when there is no matching style property that exists.
                if (tween.elem.nodeType !== 1 || tween.elem[tween.prop] != null && tween.elem.style[tween.prop] == null) {
                    return tween.elem[tween.prop];
                }

                // Passing an empty string as a 3rd parameter to .css will automatically
                // attempt a parseFloat and fallback to a string if the parse fails.
                // Simple values such as "10px" are parsed to Float;
                // complex values such as "rotate(1rad)" are returned as-is.
                result = jQuery.css(tween.elem, tween.prop, "");

                // Empty strings, null, undefined and "auto" are converted to 0.
                return !result || result === "auto" ? 0 : result;
            },
            set: function set(tween) {

                // Use step hook for back compat.
                // Use cssHook if its there.
                // Use .style if available and use plain properties where available.
                if (jQuery.fx.step[tween.prop]) {
                    jQuery.fx.step[tween.prop](tween);
                } else if (tween.elem.nodeType === 1 && (tween.elem.style[jQuery.cssProps[tween.prop]] != null || jQuery.cssHooks[tween.prop])) {
                    jQuery.style(tween.elem, tween.prop, tween.now + tween.unit);
                } else {
                    tween.elem[tween.prop] = tween.now;
                }
            }
        }
    };

    // Support: IE <=9 only
    // Panic based approach to setting things on disconnected nodes
    Tween.propHooks.scrollTop = Tween.propHooks.scrollLeft = {
        set: function set(tween) {
            if (tween.elem.nodeType && tween.elem.parentNode) {
                tween.elem[tween.prop] = tween.now;
            }
        }
    };

    jQuery.easing = {
        linear: function linear(p) {
            return p;
        },
        swing: function swing(p) {
            return 0.5 - Math.cos(p * Math.PI) / 2;
        },
        _default: "swing"
    };

    jQuery.fx = Tween.prototype.init;

    // Back compat <1.8 extension point
    jQuery.fx.step = {};

    var fxNow,
	    timerId,
	    rfxtypes = /^(?:toggle|show|hide)$/,
	    rrun = /queueHooks$/;

    function raf() {
        if (timerId) {
            window.requestAnimationFrame(raf);
            jQuery.fx.tick();
        }
    }

    // Animations created synchronously will run synchronously
    function createFxNow() {
        window.setTimeout(function () {
            fxNow = undefined;
        });
        return fxNow = jQuery.now();
    }

    // Generate parameters to create a standard animation
    function genFx(type, includeWidth) {
        var which,
		    i = 0,
		    attrs = { height: type };

        // If we include width, step value is 1 to do all cssExpand values,
        // otherwise step value is 2 to skip over Left and Right
        includeWidth = includeWidth ? 1 : 0;
        for (; i < 4; i += 2 - includeWidth) {
            which = cssExpand[i];
            attrs["margin" + which] = attrs["padding" + which] = type;
        }

        if (includeWidth) {
            attrs.opacity = attrs.width = type;
        }

        return attrs;
    }

    function createTween(value, prop, animation) {
        var tween,
		    collection = (Animation.tweeners[prop] || []).concat(Animation.tweeners["*"]),
		    index = 0,
		    length = collection.length;
        for (; index < length; index++) {
            if (tween = collection[index].call(animation, prop, value)) {

                // We're done with this property
                return tween;
            }
        }
    }

    function defaultPrefilter(elem, props, opts) {
        var prop,
		    value,
		    toggle,
		    hooks,
		    oldfire,
		    propTween,
		    restoreDisplay,
		    display,
		    isBox = "width" in props || "height" in props,
		    anim = this,
		    orig = {},
		    style = elem.style,
		    hidden = elem.nodeType && isHiddenWithinTree(elem),
		    dataShow = dataPriv.get(elem, "fxshow");

        // Queue-skipping animations hijack the fx hooks
        if (!opts.queue) {
            hooks = jQuery._queueHooks(elem, "fx");
            if (hooks.unqueued == null) {
                hooks.unqueued = 0;
                oldfire = hooks.empty.fire;
                hooks.empty.fire = function () {
                    if (!hooks.unqueued) {
                        oldfire();
                    }
                };
            }
            hooks.unqueued++;

            anim.always(function () {

                // Ensure the complete handler is called before this completes
                anim.always(function () {
                    hooks.unqueued--;
                    if (!jQuery.queue(elem, "fx").length) {
                        hooks.empty.fire();
                    }
                });
            });
        }

        // Detect show/hide animations
        for (prop in props) {
            value = props[prop];
            if (rfxtypes.test(value)) {
                delete props[prop];
                toggle = toggle || value === "toggle";
                if (value === (hidden ? "hide" : "show")) {

                    // Pretend to be hidden if this is a "show" and
                    // there is still data from a stopped show/hide
                    if (value === "show" && dataShow && dataShow[prop] !== undefined) {
                        hidden = true;

                        // Ignore all other no-op show/hide data
                    } else {
                        continue;
                    }
                }
                orig[prop] = dataShow && dataShow[prop] || jQuery.style(elem, prop);
            }
        }

        // Bail out if this is a no-op like .hide().hide()
        propTween = !jQuery.isEmptyObject(props);
        if (!propTween && jQuery.isEmptyObject(orig)) {
            return;
        }

        // Restrict "overflow" and "display" styles during box animations
        if (isBox && elem.nodeType === 1) {

            // Support: IE <=9 - 11, Edge 12 - 13
            // Record all 3 overflow attributes because IE does not infer the shorthand
            // from identically-valued overflowX and overflowY
            opts.overflow = [style.overflow, style.overflowX, style.overflowY];

            // Identify a display type, preferring old show/hide data over the CSS cascade
            restoreDisplay = dataShow && dataShow.display;
            if (restoreDisplay == null) {
                restoreDisplay = dataPriv.get(elem, "display");
            }
            display = jQuery.css(elem, "display");
            if (display === "none") {
                if (restoreDisplay) {
                    display = restoreDisplay;
                } else {

                    // Get nonempty value(s) by temporarily forcing visibility
                    showHide([elem], true);
                    restoreDisplay = elem.style.display || restoreDisplay;
                    display = jQuery.css(elem, "display");
                    showHide([elem]);
                }
            }

            // Animate inline elements as inline-block
            if (display === "inline" || display === "inline-block" && restoreDisplay != null) {
                if (jQuery.css(elem, "float") === "none") {

                    // Restore the original display value at the end of pure show/hide animations
                    if (!propTween) {
                        anim.done(function () {
                            style.display = restoreDisplay;
                        });
                        if (restoreDisplay == null) {
                            display = style.display;
                            restoreDisplay = display === "none" ? "" : display;
                        }
                    }
                    style.display = "inline-block";
                }
            }
        }

        if (opts.overflow) {
            style.overflow = "hidden";
            anim.always(function () {
                style.overflow = opts.overflow[0];
                style.overflowX = opts.overflow[1];
                style.overflowY = opts.overflow[2];
            });
        }

        // Implement show/hide animations
        propTween = false;
        for (prop in orig) {

            // General show/hide setup for this element animation
            if (!propTween) {
                if (dataShow) {
                    if ("hidden" in dataShow) {
                        hidden = dataShow.hidden;
                    }
                } else {
                    dataShow = dataPriv.access(elem, "fxshow", { display: restoreDisplay });
                }

                // Store hidden/visible for toggle so `.stop().toggle()` "reverses"
                if (toggle) {
                    dataShow.hidden = !hidden;
                }

                // Show elements before animating them
                if (hidden) {
                    showHide([elem], true);
                }

                /* eslint-disable no-loop-func */

                anim.done(function () {

                    /* eslint-enable no-loop-func */

                    // The final step of a "hide" animation is actually hiding the element
                    if (!hidden) {
                        showHide([elem]);
                    }
                    dataPriv.remove(elem, "fxshow");
                    for (prop in orig) {
                        jQuery.style(elem, prop, orig[prop]);
                    }
                });
            }

            // Per-property setup
            propTween = createTween(hidden ? dataShow[prop] : 0, prop, anim);
            if (!(prop in dataShow)) {
                dataShow[prop] = propTween.start;
                if (hidden) {
                    propTween.end = propTween.start;
                    propTween.start = 0;
                }
            }
        }
    }

    function propFilter(props, specialEasing) {
        var index, name, easing, value, hooks;

        // camelCase, specialEasing and expand cssHook pass
        for (index in props) {
            name = jQuery.camelCase(index);
            easing = specialEasing[name];
            value = props[index];
            if (jQuery.isArray(value)) {
                easing = value[1];
                value = props[index] = value[0];
            }

            if (index !== name) {
                props[name] = value;
                delete props[index];
            }

            hooks = jQuery.cssHooks[name];
            if (hooks && "expand" in hooks) {
                value = hooks.expand(value);
                delete props[name];

                // Not quite $.extend, this won't overwrite existing keys.
                // Reusing 'index' because we have the correct "name"
                for (index in value) {
                    if (!(index in props)) {
                        props[index] = value[index];
                        specialEasing[index] = easing;
                    }
                }
            } else {
                specialEasing[name] = easing;
            }
        }
    }

    function Animation(elem, properties, options) {
        var result,
		    stopped,
		    index = 0,
		    length = Animation.prefilters.length,
		    deferred = jQuery.Deferred().always(function () {

		        // Don't match elem in the :animated selector
		        delete tick.elem;
		    }),
		    tick = function tick() {
		        if (stopped) {
		            return false;
		        }
		        var currentTime = fxNow || createFxNow(),
                    remaining = Math.max(0, animation.startTime + animation.duration - currentTime),


                // Support: Android 2.3 only
                // Archaic crash bug won't allow us to use `1 - ( 0.5 || 0 )` (#12497)
                temp = remaining / animation.duration || 0,
                    percent = 1 - temp,
                    index = 0,
                    length = animation.tweens.length;

		        for (; index < length; index++) {
		            animation.tweens[index].run(percent);
		        }

		        deferred.notifyWith(elem, [animation, percent, remaining]);

		        if (percent < 1 && length) {
		            return remaining;
		        } else {
		            deferred.resolveWith(elem, [animation]);
		            return false;
		        }
		    },
		    animation = deferred.promise({
		        elem: elem,
		        props: jQuery.extend({}, properties),
		        opts: jQuery.extend(true, {
		            specialEasing: {},
		            easing: jQuery.easing._default
		        }, options),
		        originalProperties: properties,
		        originalOptions: options,
		        startTime: fxNow || createFxNow(),
		        duration: options.duration,
		        tweens: [],
		        createTween: function createTween(prop, end) {
		            var tween = jQuery.Tween(elem, animation.opts, prop, end, animation.opts.specialEasing[prop] || animation.opts.easing);
		            animation.tweens.push(tween);
		            return tween;
		        },
		        stop: function stop(gotoEnd) {
		            var index = 0,


                    // If we are going to the end, we want to run all the tweens
                    // otherwise we skip this part
                    length = gotoEnd ? animation.tweens.length : 0;
		            if (stopped) {
		                return this;
		            }
		            stopped = true;
		            for (; index < length; index++) {
		                animation.tweens[index].run(1);
		            }

		            // Resolve when we played the last frame; otherwise, reject
		            if (gotoEnd) {
		                deferred.notifyWith(elem, [animation, 1, 0]);
		                deferred.resolveWith(elem, [animation, gotoEnd]);
		            } else {
		                deferred.rejectWith(elem, [animation, gotoEnd]);
		            }
		            return this;
		        }
		    }),
		    props = animation.props;

        propFilter(props, animation.opts.specialEasing);

        for (; index < length; index++) {
            result = Animation.prefilters[index].call(animation, elem, props, animation.opts);
            if (result) {
                if (jQuery.isFunction(result.stop)) {
                    jQuery._queueHooks(animation.elem, animation.opts.queue).stop = jQuery.proxy(result.stop, result);
                }
                return result;
            }
        }

        jQuery.map(props, createTween, animation);

        if (jQuery.isFunction(animation.opts.start)) {
            animation.opts.start.call(elem, animation);
        }

        jQuery.fx.timer(jQuery.extend(tick, {
            elem: elem,
            anim: animation,
            queue: animation.opts.queue
        }));

        // attach callbacks from options
        return animation.progress(animation.opts.progress).done(animation.opts.done, animation.opts.complete).fail(animation.opts.fail).always(animation.opts.always);
    }

    jQuery.Animation = jQuery.extend(Animation, {

        tweeners: {
            "*": [function (prop, value) {
                var tween = this.createTween(prop, value);
                adjustCSS(tween.elem, prop, rcssNum.exec(value), tween);
                return tween;
            }]
        },

        tweener: function tweener(props, callback) {
            if (jQuery.isFunction(props)) {
                callback = props;
                props = ["*"];
            } else {
                props = props.match(rnothtmlwhite);
            }

            var prop,
			    index = 0,
			    length = props.length;

            for (; index < length; index++) {
                prop = props[index];
                Animation.tweeners[prop] = Animation.tweeners[prop] || [];
                Animation.tweeners[prop].unshift(callback);
            }
        },

        prefilters: [defaultPrefilter],

        prefilter: function prefilter(callback, prepend) {
            if (prepend) {
                Animation.prefilters.unshift(callback);
            } else {
                Animation.prefilters.push(callback);
            }
        }
    });

    jQuery.speed = function (speed, easing, fn) {
        var opt = speed && (typeof speed === "undefined" ? "undefined" : _typeof(speed)) === "object" ? jQuery.extend({}, speed) : {
            complete: fn || !fn && easing || jQuery.isFunction(speed) && speed,
            duration: speed,
            easing: fn && easing || easing && !jQuery.isFunction(easing) && easing
        };

        // Go to the end state if fx are off or if document is hidden
        if (jQuery.fx.off || document.hidden) {
            opt.duration = 0;
        } else {
            if (typeof opt.duration !== "number") {
                if (opt.duration in jQuery.fx.speeds) {
                    opt.duration = jQuery.fx.speeds[opt.duration];
                } else {
                    opt.duration = jQuery.fx.speeds._default;
                }
            }
        }

        // Normalize opt.queue - true/undefined/null -> "fx"
        if (opt.queue == null || opt.queue === true) {
            opt.queue = "fx";
        }

        // Queueing
        opt.old = opt.complete;

        opt.complete = function () {
            if (jQuery.isFunction(opt.old)) {
                opt.old.call(this);
            }

            if (opt.queue) {
                jQuery.dequeue(this, opt.queue);
            }
        };

        return opt;
    };

    jQuery.fn.extend({
        fadeTo: function fadeTo(speed, to, easing, callback) {

            // Show any hidden elements after setting opacity to 0
            return this.filter(isHiddenWithinTree).css("opacity", 0).show()

			// Animate to the value specified
			.end().animate({ opacity: to }, speed, easing, callback);
        },
        animate: function animate(prop, speed, easing, callback) {
            var empty = jQuery.isEmptyObject(prop),
			    optall = jQuery.speed(speed, easing, callback),
			    doAnimation = function doAnimation() {

			        // Operate on a copy of prop so per-property easing won't be lost
			        var anim = Animation(this, jQuery.extend({}, prop), optall);

			        // Empty animations, or finishing resolves immediately
			        if (empty || dataPriv.get(this, "finish")) {
			            anim.stop(true);
			        }
			    };
            doAnimation.finish = doAnimation;

            return empty || optall.queue === false ? this.each(doAnimation) : this.queue(optall.queue, doAnimation);
        },
        stop: function stop(type, clearQueue, gotoEnd) {
            var stopQueue = function stopQueue(hooks) {
                var stop = hooks.stop;
                delete hooks.stop;
                stop(gotoEnd);
            };

            if (typeof type !== "string") {
                gotoEnd = clearQueue;
                clearQueue = type;
                type = undefined;
            }
            if (clearQueue && type !== false) {
                this.queue(type || "fx", []);
            }

            return this.each(function () {
                var dequeue = true,
				    index = type != null && type + "queueHooks",
				    timers = jQuery.timers,
				    data = dataPriv.get(this);

                if (index) {
                    if (data[index] && data[index].stop) {
                        stopQueue(data[index]);
                    }
                } else {
                    for (index in data) {
                        if (data[index] && data[index].stop && rrun.test(index)) {
                            stopQueue(data[index]);
                        }
                    }
                }

                for (index = timers.length; index--;) {
                    if (timers[index].elem === this && (type == null || timers[index].queue === type)) {

                        timers[index].anim.stop(gotoEnd);
                        dequeue = false;
                        timers.splice(index, 1);
                    }
                }

                // Start the next in the queue if the last step wasn't forced.
                // Timers currently will call their complete callbacks, which
                // will dequeue but only if they were gotoEnd.
                if (dequeue || !gotoEnd) {
                    jQuery.dequeue(this, type);
                }
            });
        },
        finish: function finish(type) {
            if (type !== false) {
                type = type || "fx";
            }
            return this.each(function () {
                var index,
				    data = dataPriv.get(this),
				    queue = data[type + "queue"],
				    hooks = data[type + "queueHooks"],
				    timers = jQuery.timers,
				    length = queue ? queue.length : 0;

                // Enable finishing flag on private data
                data.finish = true;

                // Empty the queue first
                jQuery.queue(this, type, []);

                if (hooks && hooks.stop) {
                    hooks.stop.call(this, true);
                }

                // Look for any active animations, and finish them
                for (index = timers.length; index--;) {
                    if (timers[index].elem === this && timers[index].queue === type) {
                        timers[index].anim.stop(true);
                        timers.splice(index, 1);
                    }
                }

                // Look for any animations in the old queue and finish them
                for (index = 0; index < length; index++) {
                    if (queue[index] && queue[index].finish) {
                        queue[index].finish.call(this);
                    }
                }

                // Turn off finishing flag
                delete data.finish;
            });
        }
    });

    jQuery.each(["toggle", "show", "hide"], function (i, name) {
        var cssFn = jQuery.fn[name];
        jQuery.fn[name] = function (speed, easing, callback) {
            return speed == null || typeof speed === "boolean" ? cssFn.apply(this, arguments) : this.animate(genFx(name, true), speed, easing, callback);
        };
    });

    // Generate shortcuts for custom animations
    jQuery.each({
        slideDown: genFx("show"),
        slideUp: genFx("hide"),
        slideToggle: genFx("toggle"),
        fadeIn: { opacity: "show" },
        fadeOut: { opacity: "hide" },
        fadeToggle: { opacity: "toggle" }
    }, function (name, props) {
        jQuery.fn[name] = function (speed, easing, callback) {
            return this.animate(props, speed, easing, callback);
        };
    });

    jQuery.timers = [];
    jQuery.fx.tick = function () {
        var timer,
		    i = 0,
		    timers = jQuery.timers;

        fxNow = jQuery.now();

        for (; i < timers.length; i++) {
            timer = timers[i];

            // Checks the timer has not already been removed
            if (!timer() && timers[i] === timer) {
                timers.splice(i--, 1);
            }
        }

        if (!timers.length) {
            jQuery.fx.stop();
        }
        fxNow = undefined;
    };

    jQuery.fx.timer = function (timer) {
        jQuery.timers.push(timer);
        if (timer()) {
            jQuery.fx.start();
        } else {
            jQuery.timers.pop();
        }
    };

    jQuery.fx.interval = 13;
    jQuery.fx.start = function () {
        if (!timerId) {
            timerId = window.requestAnimationFrame ? window.requestAnimationFrame(raf) : window.setInterval(jQuery.fx.tick, jQuery.fx.interval);
        }
    };

    jQuery.fx.stop = function () {
        if (window.cancelAnimationFrame) {
            window.cancelAnimationFrame(timerId);
        } else {
            window.clearInterval(timerId);
        }

        timerId = null;
    };

    jQuery.fx.speeds = {
        slow: 600,
        fast: 200,

        // Default speed
        _default: 400
    };

    // Based off of the plugin by Clint Helfers, with permission.
    // https://web.archive.org/web/20100324014747/http://blindsignals.com/index.php/2009/07/jquery-delay/
    jQuery.fn.delay = function (time, type) {
        time = jQuery.fx ? jQuery.fx.speeds[time] || time : time;
        type = type || "fx";

        return this.queue(type, function (next, hooks) {
            var timeout = window.setTimeout(next, time);
            hooks.stop = function () {
                window.clearTimeout(timeout);
            };
        });
    };

    (function () {
        var input = document.createElement("input"),
		    select = document.createElement("select"),
		    opt = select.appendChild(document.createElement("option"));

        input.type = "checkbox";

        // Support: Android <=4.3 only
        // Default value for a checkbox should be "on"
        support.checkOn = input.value !== "";

        // Support: IE <=11 only
        // Must access selectedIndex to make default options select
        support.optSelected = opt.selected;

        // Support: IE <=11 only
        // An input loses its value after becoming a radio
        input = document.createElement("input");
        input.value = "t";
        input.type = "radio";
        support.radioValue = input.value === "t";
    })();

    var boolHook,
	    attrHandle = jQuery.expr.attrHandle;

    jQuery.fn.extend({
        attr: function attr(name, value) {
            return access(this, jQuery.attr, name, value, arguments.length > 1);
        },

        removeAttr: function removeAttr(name) {
            return this.each(function () {
                jQuery.removeAttr(this, name);
            });
        }
    });

    jQuery.extend({
        attr: function attr(elem, name, value) {
            var ret,
			    hooks,
			    nType = elem.nodeType;

            // Don't get/set attributes on text, comment and attribute nodes
            if (nType === 3 || nType === 8 || nType === 2) {
                return;
            }

            // Fallback to prop when attributes are not supported
            if (typeof elem.getAttribute === "undefined") {
                return jQuery.prop(elem, name, value);
            }

            // Attribute hooks are determined by the lowercase version
            // Grab necessary hook if one is defined
            if (nType !== 1 || !jQuery.isXMLDoc(elem)) {
                hooks = jQuery.attrHooks[name.toLowerCase()] || (jQuery.expr.match.bool.test(name) ? boolHook : undefined);
            }

            if (value !== undefined) {
                if (value === null) {
                    jQuery.removeAttr(elem, name);
                    return;
                }

                if (hooks && "set" in hooks && (ret = hooks.set(elem, value, name)) !== undefined) {
                    return ret;
                }

                elem.setAttribute(name, value + "");
                return value;
            }

            if (hooks && "get" in hooks && (ret = hooks.get(elem, name)) !== null) {
                return ret;
            }

            ret = jQuery.find.attr(elem, name);

            // Non-existent attributes return null, we normalize to undefined
            return ret == null ? undefined : ret;
        },

        attrHooks: {
            type: {
                set: function set(elem, value) {
                    if (!support.radioValue && value === "radio" && jQuery.nodeName(elem, "input")) {
                        var val = elem.value;
                        elem.setAttribute("type", value);
                        if (val) {
                            elem.value = val;
                        }
                        return value;
                    }
                }
            }
        },

        removeAttr: function removeAttr(elem, value) {
            var name,
			    i = 0,


			// Attribute names can contain non-HTML whitespace characters
			// https://html.spec.whatwg.org/multipage/syntax.html#attributes-2
			attrNames = value && value.match(rnothtmlwhite);

            if (attrNames && elem.nodeType === 1) {
                while (name = attrNames[i++]) {
                    elem.removeAttribute(name);
                }
            }
        }
    });

    // Hooks for boolean attributes
    boolHook = {
        set: function set(elem, value, name) {
            if (value === false) {

                // Remove boolean attributes when set to false
                jQuery.removeAttr(elem, name);
            } else {
                elem.setAttribute(name, name);
            }
            return name;
        }
    };

    jQuery.each(jQuery.expr.match.bool.source.match(/\w+/g), function (i, name) {
        var getter = attrHandle[name] || jQuery.find.attr;

        attrHandle[name] = function (elem, name, isXML) {
            var ret,
			    handle,
			    lowercaseName = name.toLowerCase();

            if (!isXML) {

                // Avoid an infinite loop by temporarily removing this function from the getter
                handle = attrHandle[lowercaseName];
                attrHandle[lowercaseName] = ret;
                ret = getter(elem, name, isXML) != null ? lowercaseName : null;
                attrHandle[lowercaseName] = handle;
            }
            return ret;
        };
    });

    var rfocusable = /^(?:input|select|textarea|button)$/i,
	    rclickable = /^(?:a|area)$/i;

    jQuery.fn.extend({
        prop: function prop(name, value) {
            return access(this, jQuery.prop, name, value, arguments.length > 1);
        },

        removeProp: function removeProp(name) {
            return this.each(function () {
                delete this[jQuery.propFix[name] || name];
            });
        }
    });

    jQuery.extend({
        prop: function prop(elem, name, value) {
            var ret,
			    hooks,
			    nType = elem.nodeType;

            // Don't get/set properties on text, comment and attribute nodes
            if (nType === 3 || nType === 8 || nType === 2) {
                return;
            }

            if (nType !== 1 || !jQuery.isXMLDoc(elem)) {

                // Fix name and attach hooks
                name = jQuery.propFix[name] || name;
                hooks = jQuery.propHooks[name];
            }

            if (value !== undefined) {
                if (hooks && "set" in hooks && (ret = hooks.set(elem, value, name)) !== undefined) {
                    return ret;
                }

                return elem[name] = value;
            }

            if (hooks && "get" in hooks && (ret = hooks.get(elem, name)) !== null) {
                return ret;
            }

            return elem[name];
        },

        propHooks: {
            tabIndex: {
                get: function get(elem) {

                    // Support: IE <=9 - 11 only
                    // elem.tabIndex doesn't always return the
                    // correct value when it hasn't been explicitly set
                    // https://web.archive.org/web/20141116233347/http://fluidproject.org/blog/2008/01/09/getting-setting-and-removing-tabindex-values-with-javascript/
                    // Use proper attribute retrieval(#12072)
                    var tabindex = jQuery.find.attr(elem, "tabindex");

                    if (tabindex) {
                        return parseInt(tabindex, 10);
                    }

                    if (rfocusable.test(elem.nodeName) || rclickable.test(elem.nodeName) && elem.href) {
                        return 0;
                    }

                    return -1;
                }
            }
        },

        propFix: {
            "for": "htmlFor",
            "class": "className"
        }
    });

    // Support: IE <=11 only
    // Accessing the selectedIndex property
    // forces the browser to respect setting selected
    // on the option
    // The getter ensures a default option is selected
    // when in an optgroup
    // eslint rule "no-unused-expressions" is disabled for this code
    // since it considers such accessions noop
    if (!support.optSelected) {
        jQuery.propHooks.selected = {
            get: function get(elem) {

                /* eslint no-unused-expressions: "off" */

                var parent = elem.parentNode;
                if (parent && parent.parentNode) {
                    parent.parentNode.selectedIndex;
                }
                return null;
            },
            set: function set(elem) {

                /* eslint no-unused-expressions: "off" */

                var parent = elem.parentNode;
                if (parent) {
                    parent.selectedIndex;

                    if (parent.parentNode) {
                        parent.parentNode.selectedIndex;
                    }
                }
            }
        };
    }

    jQuery.each(["tabIndex", "readOnly", "maxLength", "cellSpacing", "cellPadding", "rowSpan", "colSpan", "useMap", "frameBorder", "contentEditable"], function () {
        jQuery.propFix[this.toLowerCase()] = this;
    });

    // Strip and collapse whitespace according to HTML spec
    // https://html.spec.whatwg.org/multipage/infrastructure.html#strip-and-collapse-whitespace
    function stripAndCollapse(value) {
        var tokens = value.match(rnothtmlwhite) || [];
        return tokens.join(" ");
    }

    function getClass(elem) {
        return elem.getAttribute && elem.getAttribute("class") || "";
    }

    jQuery.fn.extend({
        addClass: function addClass(value) {
            var classes,
			    elem,
			    cur,
			    curValue,
			    clazz,
			    j,
			    finalValue,
			    i = 0;

            if (jQuery.isFunction(value)) {
                return this.each(function (j) {
                    jQuery(this).addClass(value.call(this, j, getClass(this)));
                });
            }

            if (typeof value === "string" && value) {
                classes = value.match(rnothtmlwhite) || [];

                while (elem = this[i++]) {
                    curValue = getClass(elem);
                    cur = elem.nodeType === 1 && " " + stripAndCollapse(curValue) + " ";

                    if (cur) {
                        j = 0;
                        while (clazz = classes[j++]) {
                            if (cur.indexOf(" " + clazz + " ") < 0) {
                                cur += clazz + " ";
                            }
                        }

                        // Only assign if different to avoid unneeded rendering.
                        finalValue = stripAndCollapse(cur);
                        if (curValue !== finalValue) {
                            elem.setAttribute("class", finalValue);
                        }
                    }
                }
            }

            return this;
        },

        removeClass: function removeClass(value) {
            var classes,
			    elem,
			    cur,
			    curValue,
			    clazz,
			    j,
			    finalValue,
			    i = 0;

            if (jQuery.isFunction(value)) {
                return this.each(function (j) {
                    jQuery(this).removeClass(value.call(this, j, getClass(this)));
                });
            }

            if (!arguments.length) {
                return this.attr("class", "");
            }

            if (typeof value === "string" && value) {
                classes = value.match(rnothtmlwhite) || [];

                while (elem = this[i++]) {
                    curValue = getClass(elem);

                    // This expression is here for better compressibility (see addClass)
                    cur = elem.nodeType === 1 && " " + stripAndCollapse(curValue) + " ";

                    if (cur) {
                        j = 0;
                        while (clazz = classes[j++]) {

                            // Remove *all* instances
                            while (cur.indexOf(" " + clazz + " ") > -1) {
                                cur = cur.replace(" " + clazz + " ", " ");
                            }
                        }

                        // Only assign if different to avoid unneeded rendering.
                        finalValue = stripAndCollapse(cur);
                        if (curValue !== finalValue) {
                            elem.setAttribute("class", finalValue);
                        }
                    }
                }
            }

            return this;
        },

        toggleClass: function toggleClass(value, stateVal) {
            var type = typeof value === "undefined" ? "undefined" : _typeof(value);

            if (typeof stateVal === "boolean" && type === "string") {
                return stateVal ? this.addClass(value) : this.removeClass(value);
            }

            if (jQuery.isFunction(value)) {
                return this.each(function (i) {
                    jQuery(this).toggleClass(value.call(this, i, getClass(this), stateVal), stateVal);
                });
            }

            return this.each(function () {
                var className, i, self, classNames;

                if (type === "string") {

                    // Toggle individual class names
                    i = 0;
                    self = jQuery(this);
                    classNames = value.match(rnothtmlwhite) || [];

                    while (className = classNames[i++]) {

                        // Check each className given, space separated list
                        if (self.hasClass(className)) {
                            self.removeClass(className);
                        } else {
                            self.addClass(className);
                        }
                    }

                    // Toggle whole class name
                } else if (value === undefined || type === "boolean") {
                    className = getClass(this);
                    if (className) {

                        // Store className if set
                        dataPriv.set(this, "__className__", className);
                    }

                    // If the element has a class name or if we're passed `false`,
                    // then remove the whole classname (if there was one, the above saved it).
                    // Otherwise bring back whatever was previously saved (if anything),
                    // falling back to the empty string if nothing was stored.
                    if (this.setAttribute) {
                        this.setAttribute("class", className || value === false ? "" : dataPriv.get(this, "__className__") || "");
                    }
                }
            });
        },

        hasClass: function hasClass(selector) {
            var className,
			    elem,
			    i = 0;

            className = " " + selector + " ";
            while (elem = this[i++]) {
                if (elem.nodeType === 1 && (" " + stripAndCollapse(getClass(elem)) + " ").indexOf(className) > -1) {
                    return true;
                }
            }

            return false;
        }
    });

    var rreturn = /\r/g;

    jQuery.fn.extend({
        val: function val(value) {
            var hooks,
			    ret,
			    isFunction,
			    elem = this[0];

            if (!arguments.length) {
                if (elem) {
                    hooks = jQuery.valHooks[elem.type] || jQuery.valHooks[elem.nodeName.toLowerCase()];

                    if (hooks && "get" in hooks && (ret = hooks.get(elem, "value")) !== undefined) {
                        return ret;
                    }

                    ret = elem.value;

                    // Handle most common string cases
                    if (typeof ret === "string") {
                        return ret.replace(rreturn, "");
                    }

                    // Handle cases where value is null/undef or number
                    return ret == null ? "" : ret;
                }

                return;
            }

            isFunction = jQuery.isFunction(value);

            return this.each(function (i) {
                var val;

                if (this.nodeType !== 1) {
                    return;
                }

                if (isFunction) {
                    val = value.call(this, i, jQuery(this).val());
                } else {
                    val = value;
                }

                // Treat null/undefined as ""; convert numbers to string
                if (val == null) {
                    val = "";
                } else if (typeof val === "number") {
                    val += "";
                } else if (jQuery.isArray(val)) {
                    val = jQuery.map(val, function (value) {
                        return value == null ? "" : value + "";
                    });
                }

                hooks = jQuery.valHooks[this.type] || jQuery.valHooks[this.nodeName.toLowerCase()];

                // If set returns undefined, fall back to normal setting
                if (!hooks || !("set" in hooks) || hooks.set(this, val, "value") === undefined) {
                    this.value = val;
                }
            });
        }
    });

    jQuery.extend({
        valHooks: {
            option: {
                get: function get(elem) {

                    var val = jQuery.find.attr(elem, "value");
                    return val != null ? val :

					// Support: IE <=10 - 11 only
					// option.text throws exceptions (#14686, #14858)
					// Strip and collapse whitespace
					// https://html.spec.whatwg.org/#strip-and-collapse-whitespace
					stripAndCollapse(jQuery.text(elem));
                }
            },
            select: {
                get: function get(elem) {
                    var value,
					    option,
					    i,
					    options = elem.options,
					    index = elem.selectedIndex,
					    one = elem.type === "select-one",
					    values = one ? null : [],
					    max = one ? index + 1 : options.length;

                    if (index < 0) {
                        i = max;
                    } else {
                        i = one ? index : 0;
                    }

                    // Loop through all the selected options
                    for (; i < max; i++) {
                        option = options[i];

                        // Support: IE <=9 only
                        // IE8-9 doesn't update selected after form reset (#2551)
                        if ((option.selected || i === index) &&

                            // Don't return options that are disabled or in a disabled optgroup
						!option.disabled && (!option.parentNode.disabled || !jQuery.nodeName(option.parentNode, "optgroup"))) {

                            // Get the specific value for the option
                            value = jQuery(option).val();

                            // We don't need an array for one selects
                            if (one) {
                                return value;
                            }

                            // Multi-Selects return an array
                            values.push(value);
                        }
                    }

                    return values;
                },

                set: function set(elem, value) {
                    var optionSet,
					    option,
					    options = elem.options,
					    values = jQuery.makeArray(value),
					    i = options.length;

                    while (i--) {
                        option = options[i];

                        /* eslint-disable no-cond-assign */

                        if (option.selected = jQuery.inArray(jQuery.valHooks.option.get(option), values) > -1) {
                            optionSet = true;
                        }

                        /* eslint-enable no-cond-assign */
                    }

                    // Force browsers to behave consistently when non-matching value is set
                    if (!optionSet) {
                        elem.selectedIndex = -1;
                    }
                    return values;
                }
            }
        }
    });

    // Radios and checkboxes getter/setter
    jQuery.each(["radio", "checkbox"], function () {
        jQuery.valHooks[this] = {
            set: function set(elem, value) {
                if (jQuery.isArray(value)) {
                    return elem.checked = jQuery.inArray(jQuery(elem).val(), value) > -1;
                }
            }
        };
        if (!support.checkOn) {
            jQuery.valHooks[this].get = function (elem) {
                return elem.getAttribute("value") === null ? "on" : elem.value;
            };
        }
    });

    // Return jQuery for attributes-only inclusion


    var rfocusMorph = /^(?:focusinfocus|focusoutblur)$/;

    jQuery.extend(jQuery.event, {

        trigger: function trigger(event, data, elem, onlyHandlers) {

            var i,
			    cur,
			    tmp,
			    bubbleType,
			    ontype,
			    handle,
			    special,
			    eventPath = [elem || document],
			    type = hasOwn.call(event, "type") ? event.type : event,
			    namespaces = hasOwn.call(event, "namespace") ? event.namespace.split(".") : [];

            cur = tmp = elem = elem || document;

            // Don't do events on text and comment nodes
            if (elem.nodeType === 3 || elem.nodeType === 8) {
                return;
            }

            // focus/blur morphs to focusin/out; ensure we're not firing them right now
            if (rfocusMorph.test(type + jQuery.event.triggered)) {
                return;
            }

            if (type.indexOf(".") > -1) {

                // Namespaced trigger; create a regexp to match event type in handle()
                namespaces = type.split(".");
                type = namespaces.shift();
                namespaces.sort();
            }
            ontype = type.indexOf(":") < 0 && "on" + type;

            // Caller can pass in a jQuery.Event object, Object, or just an event type string
            event = event[jQuery.expando] ? event : new jQuery.Event(type, (typeof event === "undefined" ? "undefined" : _typeof(event)) === "object" && event);

            // Trigger bitmask: & 1 for native handlers; & 2 for jQuery (always true)
            event.isTrigger = onlyHandlers ? 2 : 3;
            event.namespace = namespaces.join(".");
            event.rnamespace = event.namespace ? new RegExp("(^|\\.)" + namespaces.join("\\.(?:.*\\.|)") + "(\\.|$)") : null;

            // Clean up the event in case it is being reused
            event.result = undefined;
            if (!event.target) {
                event.target = elem;
            }

            // Clone any incoming data and prepend the event, creating the handler arg list
            data = data == null ? [event] : jQuery.makeArray(data, [event]);

            // Allow special events to draw outside the lines
            special = jQuery.event.special[type] || {};
            if (!onlyHandlers && special.trigger && special.trigger.apply(elem, data) === false) {
                return;
            }

            // Determine event propagation path in advance, per W3C events spec (#9951)
            // Bubble up to document, then to window; watch for a global ownerDocument var (#9724)
            if (!onlyHandlers && !special.noBubble && !jQuery.isWindow(elem)) {

                bubbleType = special.delegateType || type;
                if (!rfocusMorph.test(bubbleType + type)) {
                    cur = cur.parentNode;
                }
                for (; cur; cur = cur.parentNode) {
                    eventPath.push(cur);
                    tmp = cur;
                }

                // Only add window if we got to document (e.g., not plain obj or detached DOM)
                if (tmp === (elem.ownerDocument || document)) {
                    eventPath.push(tmp.defaultView || tmp.parentWindow || window);
                }
            }

            // Fire handlers on the event path
            i = 0;
            while ((cur = eventPath[i++]) && !event.isPropagationStopped()) {

                event.type = i > 1 ? bubbleType : special.bindType || type;

                // jQuery handler
                handle = (dataPriv.get(cur, "events") || {})[event.type] && dataPriv.get(cur, "handle");
                if (handle) {
                    handle.apply(cur, data);
                }

                // Native handler
                handle = ontype && cur[ontype];
                if (handle && handle.apply && acceptData(cur)) {
                    event.result = handle.apply(cur, data);
                    if (event.result === false) {
                        event.preventDefault();
                    }
                }
            }
            event.type = type;

            // If nobody prevented the default action, do it now
            if (!onlyHandlers && !event.isDefaultPrevented()) {

                if ((!special._default || special._default.apply(eventPath.pop(), data) === false) && acceptData(elem)) {

                    // Call a native DOM method on the target with the same name as the event.
                    // Don't do default actions on window, that's where global variables be (#6170)
                    if (ontype && jQuery.isFunction(elem[type]) && !jQuery.isWindow(elem)) {

                        // Don't re-trigger an onFOO event when we call its FOO() method
                        tmp = elem[ontype];

                        if (tmp) {
                            elem[ontype] = null;
                        }

                        // Prevent re-triggering of the same event, since we already bubbled it above
                        jQuery.event.triggered = type;
                        elem[type]();
                        jQuery.event.triggered = undefined;

                        if (tmp) {
                            elem[ontype] = tmp;
                        }
                    }
                }
            }

            return event.result;
        },

        // Piggyback on a donor event to simulate a different one
        // Used only for `focus(in | out)` events
        simulate: function simulate(type, elem, event) {
            var e = jQuery.extend(new jQuery.Event(), event, {
                type: type,
                isSimulated: true
            });

            jQuery.event.trigger(e, null, elem);
        }

    });

    jQuery.fn.extend({

        trigger: function trigger(type, data) {
            return this.each(function () {
                jQuery.event.trigger(type, data, this);
            });
        },
        triggerHandler: function triggerHandler(type, data) {
            var elem = this[0];
            if (elem) {
                return jQuery.event.trigger(type, data, elem, true);
            }
        }
    });

    jQuery.each(("blur focus focusin focusout resize scroll click dblclick " + "mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave " + "change select submit keydown keypress keyup contextmenu").split(" "), function (i, name) {

        // Handle event binding
        jQuery.fn[name] = function (data, fn) {
            return arguments.length > 0 ? this.on(name, null, data, fn) : this.trigger(name);
        };
    });

    jQuery.fn.extend({
        hover: function hover(fnOver, fnOut) {
            return this.mouseenter(fnOver).mouseleave(fnOut || fnOver);
        }
    });

    support.focusin = "onfocusin" in window;

    // Support: Firefox <=44
    // Firefox doesn't have focus(in | out) events
    // Related ticket - https://bugzilla.mozilla.org/show_bug.cgi?id=687787
    //
    // Support: Chrome <=48 - 49, Safari <=9.0 - 9.1
    // focus(in | out) events fire after focus & blur events,
    // which is spec violation - http://www.w3.org/TR/DOM-Level-3-Events/#events-focusevent-event-order
    // Related ticket - https://bugs.chromium.org/p/chromium/issues/detail?id=449857
    if (!support.focusin) {
        jQuery.each({ focus: "focusin", blur: "focusout" }, function (orig, fix) {

            // Attach a single capturing handler on the document while someone wants focusin/focusout
            var handler = function handler(event) {
                jQuery.event.simulate(fix, event.target, jQuery.event.fix(event));
            };

            jQuery.event.special[fix] = {
                setup: function setup() {
                    var doc = this.ownerDocument || this,
					    attaches = dataPriv.access(doc, fix);

                    if (!attaches) {
                        doc.addEventListener(orig, handler, true);
                    }
                    dataPriv.access(doc, fix, (attaches || 0) + 1);
                },
                teardown: function teardown() {
                    var doc = this.ownerDocument || this,
					    attaches = dataPriv.access(doc, fix) - 1;

                    if (!attaches) {
                        doc.removeEventListener(orig, handler, true);
                        dataPriv.remove(doc, fix);
                    } else {
                        dataPriv.access(doc, fix, attaches);
                    }
                }
            };
        });
    }
    var location = window.location;

    var nonce = jQuery.now();

    var rquery = /\?/;

    // Cross-browser xml parsing
    jQuery.parseXML = function (data) {
        var xml;
        if (!data || typeof data !== "string") {
            return null;
        }

        // Support: IE 9 - 11 only
        // IE throws on parseFromString with invalid input.
        try {
            xml = new window.DOMParser().parseFromString(data, "text/xml");
        } catch (e) {
            xml = undefined;
        }

        if (!xml || xml.getElementsByTagName("parsererror").length) {
            jQuery.error("Invalid XML: " + data);
        }
        return xml;
    };

    var rbracket = /\[\]$/,
	    rCRLF = /\r?\n/g,
	    rsubmitterTypes = /^(?:submit|button|image|reset|file)$/i,
	    rsubmittable = /^(?:input|select|textarea|keygen)/i;

    function buildParams(prefix, obj, traditional, add) {
        var name;

        if (jQuery.isArray(obj)) {

            // Serialize array item.
            jQuery.each(obj, function (i, v) {
                if (traditional || rbracket.test(prefix)) {

                    // Treat each array item as a scalar.
                    add(prefix, v);
                } else {

                    // Item is non-scalar (array or object), encode its numeric index.
                    buildParams(prefix + "[" + ((typeof v === "undefined" ? "undefined" : _typeof(v)) === "object" && v != null ? i : "") + "]", v, traditional, add);
                }
            });
        } else if (!traditional && jQuery.type(obj) === "object") {

            // Serialize object item.
            for (name in obj) {
                buildParams(prefix + "[" + name + "]", obj[name], traditional, add);
            }
        } else {

            // Serialize scalar item.
            add(prefix, obj);
        }
    }

    // Serialize an array of form elements or a set of
    // key/values into a query string
    jQuery.param = function (a, traditional) {
        var prefix,
		    s = [],
		    add = function add(key, valueOrFunction) {

		        // If value is a function, invoke it and use its return value
		        var value = jQuery.isFunction(valueOrFunction) ? valueOrFunction() : valueOrFunction;

		        s[s.length] = encodeURIComponent(key) + "=" + encodeURIComponent(value == null ? "" : value);
		    };

        // If an array was passed in, assume that it is an array of form elements.
        if (jQuery.isArray(a) || a.jquery && !jQuery.isPlainObject(a)) {

            // Serialize the form elements
            jQuery.each(a, function () {
                add(this.name, this.value);
            });
        } else {

            // If traditional, encode the "old" way (the way 1.3.2 or older
            // did it), otherwise encode params recursively.
            for (prefix in a) {
                buildParams(prefix, a[prefix], traditional, add);
            }
        }

        // Return the resulting serialization
        return s.join("&");
    };

    jQuery.fn.extend({
        serialize: function serialize() {
            return jQuery.param(this.serializeArray());
        },
        serializeArray: function serializeArray() {
            return this.map(function () {

                // Can add propHook for "elements" to filter or add form elements
                var elements = jQuery.prop(this, "elements");
                return elements ? jQuery.makeArray(elements) : this;
            }).filter(function () {
                var type = this.type;

                // Use .is( ":disabled" ) so that fieldset[disabled] works
                return this.name && !jQuery(this).is(":disabled") && rsubmittable.test(this.nodeName) && !rsubmitterTypes.test(type) && (this.checked || !rcheckableType.test(type));
            }).map(function (i, elem) {
                var val = jQuery(this).val();

                if (val == null) {
                    return null;
                }

                if (jQuery.isArray(val)) {
                    return jQuery.map(val, function (val) {
                        return { name: elem.name, value: val.replace(rCRLF, "\r\n") };
                    });
                }

                return { name: elem.name, value: val.replace(rCRLF, "\r\n") };
            }).get();
        }
    });

    var r20 = /%20/g,
	    rhash = /#.*$/,
	    rantiCache = /([?&])_=[^&]*/,
	    rheaders = /^(.*?):[ \t]*([^\r\n]*)$/mg,


	// #7653, #8125, #8152: local protocol detection
	rlocalProtocol = /^(?:about|app|app-storage|.+-extension|file|res|widget):$/,
	    rnoContent = /^(?:GET|HEAD)$/,
	    rprotocol = /^\/\//,


	/* Prefilters
  * 1) They are useful to introduce custom dataTypes (see ajax/jsonp.js for an example)
  * 2) These are called:
  *    - BEFORE asking for a transport
  *    - AFTER param serialization (s.data is a string if s.processData is true)
  * 3) key is the dataType
  * 4) the catchall symbol "*" can be used
  * 5) execution will start with transport dataType and THEN continue down to "*" if needed
  */
	prefilters = {},


	/* Transports bindings
  * 1) key is the dataType
  * 2) the catchall symbol "*" can be used
  * 3) selection will start with transport dataType and THEN go to "*" if needed
  */
	transports = {},


	// Avoid comment-prolog char sequence (#10098); must appease lint and evade compression
	allTypes = "*/".concat("*"),


	// Anchor tag for parsing the document origin
	originAnchor = document.createElement("a");
    originAnchor.href = location.href;

    // Base "constructor" for jQuery.ajaxPrefilter and jQuery.ajaxTransport
    function addToPrefiltersOrTransports(structure) {

        // dataTypeExpression is optional and defaults to "*"
        return function (dataTypeExpression, func) {

            if (typeof dataTypeExpression !== "string") {
                func = dataTypeExpression;
                dataTypeExpression = "*";
            }

            var dataType,
			    i = 0,
			    dataTypes = dataTypeExpression.toLowerCase().match(rnothtmlwhite) || [];

            if (jQuery.isFunction(func)) {

                // For each dataType in the dataTypeExpression
                while (dataType = dataTypes[i++]) {

                    // Prepend if requested
                    if (dataType[0] === "+") {
                        dataType = dataType.slice(1) || "*";
                        (structure[dataType] = structure[dataType] || []).unshift(func);

                        // Otherwise append
                    } else {
                        (structure[dataType] = structure[dataType] || []).push(func);
                    }
                }
            }
        };
    }

    // Base inspection function for prefilters and transports
    function inspectPrefiltersOrTransports(structure, options, originalOptions, jqXHR) {

        var inspected = {},
		    seekingTransport = structure === transports;

        function inspect(dataType) {
            var selected;
            inspected[dataType] = true;
            jQuery.each(structure[dataType] || [], function (_, prefilterOrFactory) {
                var dataTypeOrTransport = prefilterOrFactory(options, originalOptions, jqXHR);
                if (typeof dataTypeOrTransport === "string" && !seekingTransport && !inspected[dataTypeOrTransport]) {

                    options.dataTypes.unshift(dataTypeOrTransport);
                    inspect(dataTypeOrTransport);
                    return false;
                } else if (seekingTransport) {
                    return !(selected = dataTypeOrTransport);
                }
            });
            return selected;
        }

        return inspect(options.dataTypes[0]) || !inspected["*"] && inspect("*");
    }

    // A special extend for ajax options
    // that takes "flat" options (not to be deep extended)
    // Fixes #9887
    function ajaxExtend(target, src) {
        var key,
		    deep,
		    flatOptions = jQuery.ajaxSettings.flatOptions || {};

        for (key in src) {
            if (src[key] !== undefined) {
                (flatOptions[key] ? target : deep || (deep = {}))[key] = src[key];
            }
        }
        if (deep) {
            jQuery.extend(true, target, deep);
        }

        return target;
    }

    /* Handles responses to an ajax request:
  * - finds the right dataType (mediates between content-type and expected dataType)
  * - returns the corresponding response
  */
    function ajaxHandleResponses(s, jqXHR, responses) {

        var ct,
		    type,
		    finalDataType,
		    firstDataType,
		    contents = s.contents,
		    dataTypes = s.dataTypes;

        // Remove auto dataType and get content-type in the process
        while (dataTypes[0] === "*") {
            dataTypes.shift();
            if (ct === undefined) {
                ct = s.mimeType || jqXHR.getResponseHeader("Content-Type");
            }
        }

        // Check if we're dealing with a known content-type
        if (ct) {
            for (type in contents) {
                if (contents[type] && contents[type].test(ct)) {
                    dataTypes.unshift(type);
                    break;
                }
            }
        }

        // Check to see if we have a response for the expected dataType
        if (dataTypes[0] in responses) {
            finalDataType = dataTypes[0];
        } else {

            // Try convertible dataTypes
            for (type in responses) {
                if (!dataTypes[0] || s.converters[type + " " + dataTypes[0]]) {
                    finalDataType = type;
                    break;
                }
                if (!firstDataType) {
                    firstDataType = type;
                }
            }

            // Or just use first one
            finalDataType = finalDataType || firstDataType;
        }

        // If we found a dataType
        // We add the dataType to the list if needed
        // and return the corresponding response
        if (finalDataType) {
            if (finalDataType !== dataTypes[0]) {
                dataTypes.unshift(finalDataType);
            }
            return responses[finalDataType];
        }
    }

    /* Chain conversions given the request and the original response
  * Also sets the responseXXX fields on the jqXHR instance
  */
    function ajaxConvert(s, response, jqXHR, isSuccess) {
        var conv2,
		    current,
		    conv,
		    tmp,
		    prev,
		    converters = {},


		// Work with a copy of dataTypes in case we need to modify it for conversion
		dataTypes = s.dataTypes.slice();

        // Create converters map with lowercased keys
        if (dataTypes[1]) {
            for (conv in s.converters) {
                converters[conv.toLowerCase()] = s.converters[conv];
            }
        }

        current = dataTypes.shift();

        // Convert to each sequential dataType
        while (current) {

            if (s.responseFields[current]) {
                jqXHR[s.responseFields[current]] = response;
            }

            // Apply the dataFilter if provided
            if (!prev && isSuccess && s.dataFilter) {
                response = s.dataFilter(response, s.dataType);
            }

            prev = current;
            current = dataTypes.shift();

            if (current) {

                // There's only work to do if current dataType is non-auto
                if (current === "*") {

                    current = prev;

                    // Convert response if prev dataType is non-auto and differs from current
                } else if (prev !== "*" && prev !== current) {

                    // Seek a direct converter
                    conv = converters[prev + " " + current] || converters["* " + current];

                    // If none found, seek a pair
                    if (!conv) {
                        for (conv2 in converters) {

                            // If conv2 outputs current
                            tmp = conv2.split(" ");
                            if (tmp[1] === current) {

                                // If prev can be converted to accepted input
                                conv = converters[prev + " " + tmp[0]] || converters["* " + tmp[0]];
                                if (conv) {

                                    // Condense equivalence converters
                                    if (conv === true) {
                                        conv = converters[conv2];

                                        // Otherwise, insert the intermediate dataType
                                    } else if (converters[conv2] !== true) {
                                        current = tmp[0];
                                        dataTypes.unshift(tmp[1]);
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    // Apply converter (if not an equivalence)
                    if (conv !== true) {

                        // Unless errors are allowed to bubble, catch and return them
                        if (conv && s.throws) {
                            response = conv(response);
                        } else {
                            try {
                                response = conv(response);
                            } catch (e) {
                                return {
                                    state: "parsererror",
                                    error: conv ? e : "No conversion from " + prev + " to " + current
                                };
                            }
                        }
                    }
                }
            }
        }

        return { state: "success", data: response };
    }

    jQuery.extend({

        // Counter for holding the number of active queries
        active: 0,

        // Last-Modified header cache for next request
        lastModified: {},
        etag: {},

        ajaxSettings: {
            url: location.href,
            type: "GET",
            isLocal: rlocalProtocol.test(location.protocol),
            global: true,
            processData: true,
            async: true,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",

            /*
   timeout: 0,
   data: null,
   dataType: null,
   username: null,
   password: null,
   cache: null,
   throws: false,
   traditional: false,
   headers: {},
   */

            accepts: {
                "*": allTypes,
                text: "text/plain",
                html: "text/html",
                xml: "application/xml, text/xml",
                json: "application/json, text/javascript"
            },

            contents: {
                xml: /\bxml\b/,
                html: /\bhtml/,
                json: /\bjson\b/
            },

            responseFields: {
                xml: "responseXML",
                text: "responseText",
                json: "responseJSON"
            },

            // Data converters
            // Keys separate source (or catchall "*") and destination types with a single space
            converters: {

                // Convert anything to text
                "* text": String,

                // Text to html (true = no transformation)
                "text html": true,

                // Evaluate text as a json expression
                "text json": JSON.parse,

                // Parse text as xml
                "text xml": jQuery.parseXML
            },

            // For options that shouldn't be deep extended:
            // you can add your own custom options here if
            // and when you create one that shouldn't be
            // deep extended (see ajaxExtend)
            flatOptions: {
                url: true,
                context: true
            }
        },

        // Creates a full fledged settings object into target
        // with both ajaxSettings and settings fields.
        // If target is omitted, writes into ajaxSettings.
        ajaxSetup: function ajaxSetup(target, settings) {
            return settings ?

			// Building a settings object
			ajaxExtend(ajaxExtend(target, jQuery.ajaxSettings), settings) :

			// Extending ajaxSettings
			ajaxExtend(jQuery.ajaxSettings, target);
        },

        ajaxPrefilter: addToPrefiltersOrTransports(prefilters),
        ajaxTransport: addToPrefiltersOrTransports(transports),

        // Main method
        ajax: function ajax(url, options) {

            // If url is an object, simulate pre-1.5 signature
            if ((typeof url === "undefined" ? "undefined" : _typeof(url)) === "object") {
                options = url;
                url = undefined;
            }

            // Force options to be an object
            options = options || {};

            var transport,


			// URL without anti-cache param
			cacheURL,


			// Response headers
			responseHeadersString,
			    responseHeaders,


			// timeout handle
			timeoutTimer,


			// Url cleanup var
			urlAnchor,


			// Request state (becomes false upon send and true upon completion)
			completed,


			// To know if global events are to be dispatched
			fireGlobals,


			// Loop variable
			i,


			// uncached part of the url
			uncached,


			// Create the final options object
			s = jQuery.ajaxSetup({}, options),


			// Callbacks context
			callbackContext = s.context || s,


			// Context for global events is callbackContext if it is a DOM node or jQuery collection
			globalEventContext = s.context && (callbackContext.nodeType || callbackContext.jquery) ? jQuery(callbackContext) : jQuery.event,


			// Deferreds
			deferred = jQuery.Deferred(),
			    completeDeferred = jQuery.Callbacks("once memory"),


			// Status-dependent callbacks
			_statusCode = s.statusCode || {},


			// Headers (they are sent all at once)
			requestHeaders = {},
			    requestHeadersNames = {},


			// Default abort message
			strAbort = "canceled",


			// Fake xhr
			jqXHR = {
			    readyState: 0,

			    // Builds headers hashtable if needed
			    getResponseHeader: function getResponseHeader(key) {
			        var match;
			        if (completed) {
			            if (!responseHeaders) {
			                responseHeaders = {};
			                while (match = rheaders.exec(responseHeadersString)) {
			                    responseHeaders[match[1].toLowerCase()] = match[2];
			                }
			            }
			            match = responseHeaders[key.toLowerCase()];
			        }
			        return match == null ? null : match;
			    },

			    // Raw string
			    getAllResponseHeaders: function getAllResponseHeaders() {
			        return completed ? responseHeadersString : null;
			    },

			    // Caches the header
			    setRequestHeader: function setRequestHeader(name, value) {
			        if (completed == null) {
			            name = requestHeadersNames[name.toLowerCase()] = requestHeadersNames[name.toLowerCase()] || name;
			            requestHeaders[name] = value;
			        }
			        return this;
			    },

			    // Overrides response content-type header
			    overrideMimeType: function overrideMimeType(type) {
			        if (completed == null) {
			            s.mimeType = type;
			        }
			        return this;
			    },

			    // Status-dependent callbacks
			    statusCode: function statusCode(map) {
			        var code;
			        if (map) {
			            if (completed) {

			                // Execute the appropriate callbacks
			                jqXHR.always(map[jqXHR.status]);
			            } else {

			                // Lazy-add the new callbacks in a way that preserves old ones
			                for (code in map) {
			                    _statusCode[code] = [_statusCode[code], map[code]];
			                }
			            }
			        }
			        return this;
			    },

			    // Cancel the request
			    abort: function abort(statusText) {
			        var finalText = statusText || strAbort;
			        if (transport) {
			            transport.abort(finalText);
			        }
			        done(0, finalText);
			        return this;
			    }
			};

            // Attach deferreds
            deferred.promise(jqXHR);

            // Add protocol if not provided (prefilters might expect it)
            // Handle falsy url in the settings object (#10093: consistency with old signature)
            // We also use the url parameter if available
            s.url = ((url || s.url || location.href) + "").replace(rprotocol, location.protocol + "//");

            // Alias method option to type as per ticket #12004
            s.type = options.method || options.type || s.method || s.type;

            // Extract dataTypes list
            s.dataTypes = (s.dataType || "*").toLowerCase().match(rnothtmlwhite) || [""];

            // A cross-domain request is in order when the origin doesn't match the current origin.
            if (s.crossDomain == null) {
                urlAnchor = document.createElement("a");

                // Support: IE <=8 - 11, Edge 12 - 13
                // IE throws exception on accessing the href property if url is malformed,
                // e.g. http://example.com:80x/
                try {
                    urlAnchor.href = s.url;

                    // Support: IE <=8 - 11 only
                    // Anchor's host property isn't correctly set when s.url is relative
                    urlAnchor.href = urlAnchor.href;
                    s.crossDomain = originAnchor.protocol + "//" + originAnchor.host !== urlAnchor.protocol + "//" + urlAnchor.host;
                } catch (e) {

                    // If there is an error parsing the URL, assume it is crossDomain,
                    // it can be rejected by the transport if it is invalid
                    s.crossDomain = true;
                }
            }

            // Convert data if not already a string
            if (s.data && s.processData && typeof s.data !== "string") {
                s.data = jQuery.param(s.data, s.traditional);
            }

            // Apply prefilters
            inspectPrefiltersOrTransports(prefilters, s, options, jqXHR);

            // If request was aborted inside a prefilter, stop there
            if (completed) {
                return jqXHR;
            }

            // We can fire global events as of now if asked to
            // Don't fire events if jQuery.event is undefined in an AMD-usage scenario (#15118)
            fireGlobals = jQuery.event && s.global;

            // Watch for a new set of requests
            if (fireGlobals && jQuery.active++ === 0) {
                jQuery.event.trigger("ajaxStart");
            }

            // Uppercase the type
            s.type = s.type.toUpperCase();

            // Determine if request has content
            s.hasContent = !rnoContent.test(s.type);

            // Save the URL in case we're toying with the If-Modified-Since
            // and/or If-None-Match header later on
            // Remove hash to simplify url manipulation
            cacheURL = s.url.replace(rhash, "");

            // More options handling for requests with no content
            if (!s.hasContent) {

                // Remember the hash so we can put it back
                uncached = s.url.slice(cacheURL.length);

                // If data is available, append data to url
                if (s.data) {
                    cacheURL += (rquery.test(cacheURL) ? "&" : "?") + s.data;

                    // #9682: remove data so that it's not used in an eventual retry
                    delete s.data;
                }

                // Add or update anti-cache param if needed
                if (s.cache === false) {
                    cacheURL = cacheURL.replace(rantiCache, "$1");
                    uncached = (rquery.test(cacheURL) ? "&" : "?") + "_=" + nonce++ + uncached;
                }

                // Put hash and anti-cache on the URL that will be requested (gh-1732)
                s.url = cacheURL + uncached;

                // Change '%20' to '+' if this is encoded form body content (gh-2658)
            } else if (s.data && s.processData && (s.contentType || "").indexOf("application/x-www-form-urlencoded") === 0) {
                s.data = s.data.replace(r20, "+");
            }

            // Set the If-Modified-Since and/or If-None-Match header, if in ifModified mode.
            if (s.ifModified) {
                if (jQuery.lastModified[cacheURL]) {
                    jqXHR.setRequestHeader("If-Modified-Since", jQuery.lastModified[cacheURL]);
                }
                if (jQuery.etag[cacheURL]) {
                    jqXHR.setRequestHeader("If-None-Match", jQuery.etag[cacheURL]);
                }
            }

            // Set the correct header, if data is being sent
            if (s.data && s.hasContent && s.contentType !== false || options.contentType) {
                jqXHR.setRequestHeader("Content-Type", s.contentType);
            }

            // Set the Accepts header for the server, depending on the dataType
            jqXHR.setRequestHeader("Accept", s.dataTypes[0] && s.accepts[s.dataTypes[0]] ? s.accepts[s.dataTypes[0]] + (s.dataTypes[0] !== "*" ? ", " + allTypes + "; q=0.01" : "") : s.accepts["*"]);

            // Check for headers option
            for (i in s.headers) {
                jqXHR.setRequestHeader(i, s.headers[i]);
            }

            // Allow custom headers/mimetypes and early abort
            if (s.beforeSend && (s.beforeSend.call(callbackContext, jqXHR, s) === false || completed)) {

                // Abort if not done already and return
                return jqXHR.abort();
            }

            // Aborting is no longer a cancellation
            strAbort = "abort";

            // Install callbacks on deferreds
            completeDeferred.add(s.complete);
            jqXHR.done(s.success);
            jqXHR.fail(s.error);

            // Get transport
            transport = inspectPrefiltersOrTransports(transports, s, options, jqXHR);

            // If no transport, we auto-abort
            if (!transport) {
                done(-1, "No Transport");
            } else {
                jqXHR.readyState = 1;

                // Send global event
                if (fireGlobals) {
                    globalEventContext.trigger("ajaxSend", [jqXHR, s]);
                }

                // If request was aborted inside ajaxSend, stop there
                if (completed) {
                    return jqXHR;
                }

                // Timeout
                if (s.async && s.timeout > 0) {
                    timeoutTimer = window.setTimeout(function () {
                        jqXHR.abort("timeout");
                    }, s.timeout);
                }

                try {
                    completed = false;
                    transport.send(requestHeaders, done);
                } catch (e) {

                    // Rethrow post-completion exceptions
                    if (completed) {
                        throw e;
                    }

                    // Propagate others as results
                    done(-1, e);
                }
            }

            // Callback for when everything is done
            function done(status, nativeStatusText, responses, headers) {
                var isSuccess,
				    success,
				    error,
				    response,
				    modified,
				    statusText = nativeStatusText;

                // Ignore repeat invocations
                if (completed) {
                    return;
                }

                completed = true;

                // Clear timeout if it exists
                if (timeoutTimer) {
                    window.clearTimeout(timeoutTimer);
                }

                // Dereference transport for early garbage collection
                // (no matter how long the jqXHR object will be used)
                transport = undefined;

                // Cache response headers
                responseHeadersString = headers || "";

                // Set readyState
                jqXHR.readyState = status > 0 ? 4 : 0;

                // Determine if successful
                isSuccess = status >= 200 && status < 300 || status === 304;

                // Get response data
                if (responses) {
                    response = ajaxHandleResponses(s, jqXHR, responses);
                }

                // Convert no matter what (that way responseXXX fields are always set)
                response = ajaxConvert(s, response, jqXHR, isSuccess);

                // If successful, handle type chaining
                if (isSuccess) {

                    // Set the If-Modified-Since and/or If-None-Match header, if in ifModified mode.
                    if (s.ifModified) {
                        modified = jqXHR.getResponseHeader("Last-Modified");
                        if (modified) {
                            jQuery.lastModified[cacheURL] = modified;
                        }
                        modified = jqXHR.getResponseHeader("etag");
                        if (modified) {
                            jQuery.etag[cacheURL] = modified;
                        }
                    }

                    // if no content
                    if (status === 204 || s.type === "HEAD") {
                        statusText = "nocontent";

                        // if not modified
                    } else if (status === 304) {
                        statusText = "notmodified";

                        // If we have data, let's convert it
                    } else {
                        statusText = response.state;
                        success = response.data;
                        error = response.error;
                        isSuccess = !error;
                    }
                } else {

                    // Extract error from statusText and normalize for non-aborts
                    error = statusText;
                    if (status || !statusText) {
                        statusText = "error";
                        if (status < 0) {
                            status = 0;
                        }
                    }
                }

                // Set data for the fake xhr object
                jqXHR.status = status;
                jqXHR.statusText = (nativeStatusText || statusText) + "";

                // Success/Error
                if (isSuccess) {
                    deferred.resolveWith(callbackContext, [success, statusText, jqXHR]);
                } else {
                    deferred.rejectWith(callbackContext, [jqXHR, statusText, error]);
                }

                // Status-dependent callbacks
                jqXHR.statusCode(_statusCode);
                _statusCode = undefined;

                if (fireGlobals) {
                    globalEventContext.trigger(isSuccess ? "ajaxSuccess" : "ajaxError", [jqXHR, s, isSuccess ? success : error]);
                }

                // Complete
                completeDeferred.fireWith(callbackContext, [jqXHR, statusText]);

                if (fireGlobals) {
                    globalEventContext.trigger("ajaxComplete", [jqXHR, s]);

                    // Handle the global AJAX counter
                    if (! --jQuery.active) {
                        jQuery.event.trigger("ajaxStop");
                    }
                }
            }

            return jqXHR;
        },

        getJSON: function getJSON(url, data, callback) {
            return jQuery.get(url, data, callback, "json");
        },

        getScript: function getScript(url, callback) {
            return jQuery.get(url, undefined, callback, "script");
        }
    });

    jQuery.each(["get", "post"], function (i, method) {
        jQuery[method] = function (url, data, callback, type) {

            // Shift arguments if data argument was omitted
            if (jQuery.isFunction(data)) {
                type = type || callback;
                callback = data;
                data = undefined;
            }

            // The url can be an options object (which then must have .url)
            return jQuery.ajax(jQuery.extend({
                url: url,
                type: method,
                dataType: type,
                data: data,
                success: callback
            }, jQuery.isPlainObject(url) && url));
        };
    });

    jQuery._evalUrl = function (url) {
        return jQuery.ajax({
            url: url,

            // Make this explicit, since user can override this through ajaxSetup (#11264)
            type: "GET",
            dataType: "script",
            cache: true,
            async: false,
            global: false,
            "throws": true
        });
    };

    jQuery.fn.extend({
        wrapAll: function wrapAll(html) {
            var wrap;

            if (this[0]) {
                if (jQuery.isFunction(html)) {
                    html = html.call(this[0]);
                }

                // The elements to wrap the target around
                wrap = jQuery(html, this[0].ownerDocument).eq(0).clone(true);

                if (this[0].parentNode) {
                    wrap.insertBefore(this[0]);
                }

                wrap.map(function () {
                    var elem = this;

                    while (elem.firstElementChild) {
                        elem = elem.firstElementChild;
                    }

                    return elem;
                }).append(this);
            }

            return this;
        },

        wrapInner: function wrapInner(html) {
            if (jQuery.isFunction(html)) {
                return this.each(function (i) {
                    jQuery(this).wrapInner(html.call(this, i));
                });
            }

            return this.each(function () {
                var self = jQuery(this),
				    contents = self.contents();

                if (contents.length) {
                    contents.wrapAll(html);
                } else {
                    self.append(html);
                }
            });
        },

        wrap: function wrap(html) {
            var isFunction = jQuery.isFunction(html);

            return this.each(function (i) {
                jQuery(this).wrapAll(isFunction ? html.call(this, i) : html);
            });
        },

        unwrap: function unwrap(selector) {
            this.parent(selector).not("body").each(function () {
                jQuery(this).replaceWith(this.childNodes);
            });
            return this;
        }
    });

    jQuery.expr.pseudos.hidden = function (elem) {
        return !jQuery.expr.pseudos.visible(elem);
    };
    jQuery.expr.pseudos.visible = function (elem) {
        return !!(elem.offsetWidth || elem.offsetHeight || elem.getClientRects().length);
    };

    jQuery.ajaxSettings.xhr = function () {
        try {
            return new window.XMLHttpRequest();
        } catch (e) { }
    };

    var xhrSuccessStatus = {

        // File protocol always yields status code 0, assume 200
        0: 200,

        // Support: IE <=9 only
        // #1450: sometimes IE returns 1223 when it should be 204
        1223: 204
    },
	    xhrSupported = jQuery.ajaxSettings.xhr();

    support.cors = !!xhrSupported && "withCredentials" in xhrSupported;
    support.ajax = xhrSupported = !!xhrSupported;

    jQuery.ajaxTransport(function (options) {
        var _callback, errorCallback;

        // Cross domain only allowed if supported through XMLHttpRequest
        if (support.cors || xhrSupported && !options.crossDomain) {
            return {
                send: function send(headers, complete) {
                    var i,
					    xhr = options.xhr();

                    xhr.open(options.type, options.url, options.async, options.username, options.password);

                    // Apply custom fields if provided
                    if (options.xhrFields) {
                        for (i in options.xhrFields) {
                            xhr[i] = options.xhrFields[i];
                        }
                    }

                    // Override mime type if needed
                    if (options.mimeType && xhr.overrideMimeType) {
                        xhr.overrideMimeType(options.mimeType);
                    }

                    // X-Requested-With header
                    // For cross-domain requests, seeing as conditions for a preflight are
                    // akin to a jigsaw puzzle, we simply never set it to be sure.
                    // (it can always be set on a per-request basis or even using ajaxSetup)
                    // For same-domain requests, won't change header if already provided.
                    if (!options.crossDomain && !headers["X-Requested-With"]) {
                        headers["X-Requested-With"] = "XMLHttpRequest";
                    }

                    // Set headers
                    for (i in headers) {
                        xhr.setRequestHeader(i, headers[i]);
                    }

                    // Callback
                    _callback = function callback(type) {
                        return function () {
                            if (_callback) {
                                _callback = errorCallback = xhr.onload = xhr.onerror = xhr.onabort = xhr.onreadystatechange = null;

                                if (type === "abort") {
                                    xhr.abort();
                                } else if (type === "error") {

                                    // Support: IE <=9 only
                                    // On a manual native abort, IE9 throws
                                    // errors on any property access that is not readyState
                                    if (typeof xhr.status !== "number") {
                                        complete(0, "error");
                                    } else {
                                        complete(

										// File: protocol always yields status 0; see #8605, #14207
										xhr.status, xhr.statusText);
                                    }
                                } else {
                                    complete(xhrSuccessStatus[xhr.status] || xhr.status, xhr.statusText,

									// Support: IE <=9 only
									// IE9 has no XHR2 but throws on binary (trac-11426)
									// For XHR2 non-text, let the caller handle it (gh-2498)
									(xhr.responseType || "text") !== "text" || typeof xhr.responseText !== "string" ? { binary: xhr.response } : { text: xhr.responseText }, xhr.getAllResponseHeaders());
                                }
                            }
                        };
                    };

                    // Listen to events
                    xhr.onload = _callback();
                    errorCallback = xhr.onerror = _callback("error");

                    // Support: IE 9 only
                    // Use onreadystatechange to replace onabort
                    // to handle uncaught aborts
                    if (xhr.onabort !== undefined) {
                        xhr.onabort = errorCallback;
                    } else {
                        xhr.onreadystatechange = function () {

                            // Check readyState before timeout as it changes
                            if (xhr.readyState === 4) {

                                // Allow onerror to be called first,
                                // but that will not handle a native abort
                                // Also, save errorCallback to a variable
                                // as xhr.onerror cannot be accessed
                                window.setTimeout(function () {
                                    if (_callback) {
                                        errorCallback();
                                    }
                                });
                            }
                        };
                    }

                    // Create the abort callback
                    _callback = _callback("abort");

                    try {

                        // Do send the request (this may raise an exception)
                        xhr.send(options.hasContent && options.data || null);
                    } catch (e) {

                        // #14683: Only rethrow if this hasn't been notified as an error yet
                        if (_callback) {
                            throw e;
                        }
                    }
                },

                abort: function abort() {
                    if (_callback) {
                        _callback();
                    }
                }
            };
        }
    });

    // Prevent auto-execution of scripts when no explicit dataType was provided (See gh-2432)
    jQuery.ajaxPrefilter(function (s) {
        if (s.crossDomain) {
            s.contents.script = false;
        }
    });

    // Install script dataType
    jQuery.ajaxSetup({
        accepts: {
            script: "text/javascript, application/javascript, " + "application/ecmascript, application/x-ecmascript"
        },
        contents: {
            script: /\b(?:java|ecma)script\b/
        },
        converters: {
            "text script": function textScript(text) {
                jQuery.globalEval(text);
                return text;
            }
        }
    });

    // Handle cache's special case and crossDomain
    jQuery.ajaxPrefilter("script", function (s) {
        if (s.cache === undefined) {
            s.cache = false;
        }
        if (s.crossDomain) {
            s.type = "GET";
        }
    });

    // Bind script tag hack transport
    jQuery.ajaxTransport("script", function (s) {

        // This transport only deals with cross domain requests
        if (s.crossDomain) {
            var script, _callback2;
            return {
                send: function send(_, complete) {
                    script = jQuery("<script>").prop({
                        charset: s.scriptCharset,
                        src: s.url
                    }).on("load error", _callback2 = function callback(evt) {
                        script.remove();
                        _callback2 = null;
                        if (evt) {
                            complete(evt.type === "error" ? 404 : 200, evt.type);
                        }
                    });

                    // Use native DOM manipulation to avoid our domManip AJAX trickery
                    document.head.appendChild(script[0]);
                },
                abort: function abort() {
                    if (_callback2) {
                        _callback2();
                    }
                }
            };
        }
    });

    var oldCallbacks = [],
	    rjsonp = /(=)\?(?=&|$)|\?\?/;

    // Default jsonp settings
    jQuery.ajaxSetup({
        jsonp: "callback",
        jsonpCallback: function jsonpCallback() {
            var callback = oldCallbacks.pop() || jQuery.expando + "_" + nonce++;
            this[callback] = true;
            return callback;
        }
    });

    // Detect, normalize options and install callbacks for jsonp requests
    jQuery.ajaxPrefilter("json jsonp", function (s, originalSettings, jqXHR) {

        var callbackName,
		    overwritten,
		    responseContainer,
		    jsonProp = s.jsonp !== false && (rjsonp.test(s.url) ? "url" : typeof s.data === "string" && (s.contentType || "").indexOf("application/x-www-form-urlencoded") === 0 && rjsonp.test(s.data) && "data");

        // Handle iff the expected data type is "jsonp" or we have a parameter to set
        if (jsonProp || s.dataTypes[0] === "jsonp") {

            // Get callback name, remembering preexisting value associated with it
            callbackName = s.jsonpCallback = jQuery.isFunction(s.jsonpCallback) ? s.jsonpCallback() : s.jsonpCallback;

            // Insert callback into url or form data
            if (jsonProp) {
                s[jsonProp] = s[jsonProp].replace(rjsonp, "$1" + callbackName);
            } else if (s.jsonp !== false) {
                s.url += (rquery.test(s.url) ? "&" : "?") + s.jsonp + "=" + callbackName;
            }

            // Use data converter to retrieve json after script execution
            s.converters["script json"] = function () {
                if (!responseContainer) {
                    jQuery.error(callbackName + " was not called");
                }
                return responseContainer[0];
            };

            // Force json dataType
            s.dataTypes[0] = "json";

            // Install callback
            overwritten = window[callbackName];
            window[callbackName] = function () {
                responseContainer = arguments;
            };

            // Clean-up function (fires after converters)
            jqXHR.always(function () {

                // If previous value didn't exist - remove it
                if (overwritten === undefined) {
                    jQuery(window).removeProp(callbackName);

                    // Otherwise restore preexisting value
                } else {
                    window[callbackName] = overwritten;
                }

                // Save back as free
                if (s[callbackName]) {

                    // Make sure that re-using the options doesn't screw things around
                    s.jsonpCallback = originalSettings.jsonpCallback;

                    // Save the callback name for future use
                    oldCallbacks.push(callbackName);
                }

                // Call if it was a function and we have a response
                if (responseContainer && jQuery.isFunction(overwritten)) {
                    overwritten(responseContainer[0]);
                }

                responseContainer = overwritten = undefined;
            });

            // Delegate to script
            return "script";
        }
    });

    // Support: Safari 8 only
    // In Safari 8 documents created via document.implementation.createHTMLDocument
    // collapse sibling forms: the second one becomes a child of the first one.
    // Because of that, this security measure has to be disabled in Safari 8.
    // https://bugs.webkit.org/show_bug.cgi?id=137337
    support.createHTMLDocument = function () {
        var body = document.implementation.createHTMLDocument("").body;
        body.innerHTML = "<form></form><form></form>";
        return body.childNodes.length === 2;
    }();

    // Argument "data" should be string of html
    // context (optional): If specified, the fragment will be created in this context,
    // defaults to document
    // keepScripts (optional): If true, will include scripts passed in the html string
    jQuery.parseHTML = function (data, context, keepScripts) {
        if (typeof data !== "string") {
            return [];
        }
        if (typeof context === "boolean") {
            keepScripts = context;
            context = false;
        }

        var base, parsed, scripts;

        if (!context) {

            // Stop scripts or inline event handlers from being executed immediately
            // by using document.implementation
            if (support.createHTMLDocument) {
                context = document.implementation.createHTMLDocument("");

                // Set the base href for the created document
                // so any parsed elements with URLs
                // are based on the document's URL (gh-2965)
                base = context.createElement("base");
                base.href = document.location.href;
                context.head.appendChild(base);
            } else {
                context = document;
            }
        }

        parsed = rsingleTag.exec(data);
        scripts = !keepScripts && [];

        // Single tag
        if (parsed) {
            return [context.createElement(parsed[1])];
        }

        parsed = buildFragment([data], context, scripts);

        if (scripts && scripts.length) {
            jQuery(scripts).remove();
        }

        return jQuery.merge([], parsed.childNodes);
    };

    /**
  * Load a url into a page
  */
    jQuery.fn.load = function (url, params, callback) {
        var selector,
		    type,
		    response,
		    self = this,
		    off = url.indexOf(" ");

        if (off > -1) {
            selector = stripAndCollapse(url.slice(off));
            url = url.slice(0, off);
        }

        // If it's a function
        if (jQuery.isFunction(params)) {

            // We assume that it's the callback
            callback = params;
            params = undefined;

            // Otherwise, build a param string
        } else if (params && (typeof params === "undefined" ? "undefined" : _typeof(params)) === "object") {
            type = "POST";
        }

        // If we have elements to modify, make the request
        if (self.length > 0) {
            jQuery.ajax({
                url: url,

                // If "type" variable is undefined, then "GET" method will be used.
                // Make value of this field explicit since
                // user can override it through ajaxSetup method
                type: type || "GET",
                dataType: "html",
                data: params
            }).done(function (responseText) {

                // Save response for use in complete callback
                response = arguments;

                self.html(selector ?

				// If a selector was specified, locate the right elements in a dummy div
				// Exclude scripts to avoid IE 'Permission Denied' errors
				jQuery("<div>").append(jQuery.parseHTML(responseText)).find(selector) :

				// Otherwise use the full result
				responseText);

                // If the request succeeds, this function gets "data", "status", "jqXHR"
                // but they are ignored because response was set above.
                // If it fails, this function gets "jqXHR", "status", "error"
            }).always(callback && function (jqXHR, status) {
                self.each(function () {
                    callback.apply(this, response || [jqXHR.responseText, status, jqXHR]);
                });
            });
        }

        return this;
    };

    // Attach a bunch of functions for handling common AJAX events
    jQuery.each(["ajaxStart", "ajaxStop", "ajaxComplete", "ajaxError", "ajaxSuccess", "ajaxSend"], function (i, type) {
        jQuery.fn[type] = function (fn) {
            return this.on(type, fn);
        };
    });

    jQuery.expr.pseudos.animated = function (elem) {
        return jQuery.grep(jQuery.timers, function (fn) {
            return elem === fn.elem;
        }).length;
    };

    /**
  * Gets a window from an element
  */
    function getWindow(elem) {
        return jQuery.isWindow(elem) ? elem : elem.nodeType === 9 && elem.defaultView;
    }

    jQuery.offset = {
        setOffset: function setOffset(elem, options, i) {
            var curPosition,
			    curLeft,
			    curCSSTop,
			    curTop,
			    curOffset,
			    curCSSLeft,
			    calculatePosition,
			    position = jQuery.css(elem, "position"),
			    curElem = jQuery(elem),
			    props = {};

            // Set position first, in-case top/left are set even on static elem
            if (position === "static") {
                elem.style.position = "relative";
            }

            curOffset = curElem.offset();
            curCSSTop = jQuery.css(elem, "top");
            curCSSLeft = jQuery.css(elem, "left");
            calculatePosition = (position === "absolute" || position === "fixed") && (curCSSTop + curCSSLeft).indexOf("auto") > -1;

            // Need to be able to calculate position if either
            // top or left is auto and position is either absolute or fixed
            if (calculatePosition) {
                curPosition = curElem.position();
                curTop = curPosition.top;
                curLeft = curPosition.left;
            } else {
                curTop = parseFloat(curCSSTop) || 0;
                curLeft = parseFloat(curCSSLeft) || 0;
            }

            if (jQuery.isFunction(options)) {

                // Use jQuery.extend here to allow modification of coordinates argument (gh-1848)
                options = options.call(elem, i, jQuery.extend({}, curOffset));
            }

            if (options.top != null) {
                props.top = options.top - curOffset.top + curTop;
            }
            if (options.left != null) {
                props.left = options.left - curOffset.left + curLeft;
            }

            if ("using" in options) {
                options.using.call(elem, props);
            } else {
                curElem.css(props);
            }
        }
    };

    jQuery.fn.extend({
        offset: function offset(options) {

            // Preserve chaining for setter
            if (arguments.length) {
                return options === undefined ? this : this.each(function (i) {
                    jQuery.offset.setOffset(this, options, i);
                });
            }

            var docElem,
			    win,
			    rect,
			    doc,
			    elem = this[0];

            if (!elem) {
                return;
            }

            // Support: IE <=11 only
            // Running getBoundingClientRect on a
            // disconnected node in IE throws an error
            if (!elem.getClientRects().length) {
                return { top: 0, left: 0 };
            }

            rect = elem.getBoundingClientRect();

            // Make sure element is not hidden (display: none)
            if (rect.width || rect.height) {
                doc = elem.ownerDocument;
                win = getWindow(doc);
                docElem = doc.documentElement;

                return {
                    top: rect.top + win.pageYOffset - docElem.clientTop,
                    left: rect.left + win.pageXOffset - docElem.clientLeft
                };
            }

            // Return zeros for disconnected and hidden elements (gh-2310)
            return rect;
        },

        position: function position() {
            if (!this[0]) {
                return;
            }

            var offsetParent,
			    offset,
			    elem = this[0],
			    parentOffset = { top: 0, left: 0 };

            // Fixed elements are offset from window (parentOffset = {top:0, left: 0},
            // because it is its only offset parent
            if (jQuery.css(elem, "position") === "fixed") {

                // Assume getBoundingClientRect is there when computed position is fixed
                offset = elem.getBoundingClientRect();
            } else {

                // Get *real* offsetParent
                offsetParent = this.offsetParent();

                // Get correct offsets
                offset = this.offset();
                if (!jQuery.nodeName(offsetParent[0], "html")) {
                    parentOffset = offsetParent.offset();
                }

                // Add offsetParent borders
                parentOffset = {
                    top: parentOffset.top + jQuery.css(offsetParent[0], "borderTopWidth", true),
                    left: parentOffset.left + jQuery.css(offsetParent[0], "borderLeftWidth", true)
                };
            }

            // Subtract parent offsets and element margins
            return {
                top: offset.top - parentOffset.top - jQuery.css(elem, "marginTop", true),
                left: offset.left - parentOffset.left - jQuery.css(elem, "marginLeft", true)
            };
        },

        // This method will return documentElement in the following cases:
        // 1) For the element inside the iframe without offsetParent, this method will return
        //    documentElement of the parent window
        // 2) For the hidden or detached element
        // 3) For body or html element, i.e. in case of the html node - it will return itself
        //
        // but those exceptions were never presented as a real life use-cases
        // and might be considered as more preferable results.
        //
        // This logic, however, is not guaranteed and can change at any point in the future
        offsetParent: function offsetParent() {
            return this.map(function () {
                var offsetParent = this.offsetParent;

                while (offsetParent && jQuery.css(offsetParent, "position") === "static") {
                    offsetParent = offsetParent.offsetParent;
                }

                return offsetParent || documentElement;
            });
        }
    });

    // Create scrollLeft and scrollTop methods
    jQuery.each({ scrollLeft: "pageXOffset", scrollTop: "pageYOffset" }, function (method, prop) {
        var top = "pageYOffset" === prop;

        jQuery.fn[method] = function (val) {
            return access(this, function (elem, method, val) {
                var win = getWindow(elem);

                if (val === undefined) {
                    return win ? win[prop] : elem[method];
                }

                if (win) {
                    win.scrollTo(!top ? val : win.pageXOffset, top ? val : win.pageYOffset);
                } else {
                    elem[method] = val;
                }
            }, method, val, arguments.length);
        };
    });

    // Support: Safari <=7 - 9.1, Chrome <=37 - 49
    // Add the top/left cssHooks using jQuery.fn.position
    // Webkit bug: https://bugs.webkit.org/show_bug.cgi?id=29084
    // Blink bug: https://bugs.chromium.org/p/chromium/issues/detail?id=589347
    // getComputedStyle returns percent when specified for top/left/bottom/right;
    // rather than make the css module depend on the offset module, just check for it here
    jQuery.each(["top", "left"], function (i, prop) {
        jQuery.cssHooks[prop] = addGetHookIf(support.pixelPosition, function (elem, computed) {
            if (computed) {
                computed = curCSS(elem, prop);

                // If curCSS returns percentage, fallback to offset
                return rnumnonpx.test(computed) ? jQuery(elem).position()[prop] + "px" : computed;
            }
        });
    });

    // Create innerHeight, innerWidth, height, width, outerHeight and outerWidth methods
    jQuery.each({ Height: "height", Width: "width" }, function (name, type) {
        jQuery.each({ padding: "inner" + name, content: type, "": "outer" + name }, function (defaultExtra, funcName) {

            // Margin is only for outerHeight, outerWidth
            jQuery.fn[funcName] = function (margin, value) {
                var chainable = arguments.length && (defaultExtra || typeof margin !== "boolean"),
				    extra = defaultExtra || (margin === true || value === true ? "margin" : "border");

                return access(this, function (elem, type, value) {
                    var doc;

                    if (jQuery.isWindow(elem)) {

                        // $( window ).outerWidth/Height return w/h including scrollbars (gh-1729)
                        return funcName.indexOf("outer") === 0 ? elem["inner" + name] : elem.document.documentElement["client" + name];
                    }

                    // Get document width or height
                    if (elem.nodeType === 9) {
                        doc = elem.documentElement;

                        // Either scroll[Width/Height] or offset[Width/Height] or client[Width/Height],
                        // whichever is greatest
                        return Math.max(elem.body["scroll" + name], doc["scroll" + name], elem.body["offset" + name], doc["offset" + name], doc["client" + name]);
                    }

                    return value === undefined ?

					// Get width or height on the element, requesting but not forcing parseFloat
					jQuery.css(elem, type, extra) :

					// Set width or height on the element
					jQuery.style(elem, type, value, extra);
                }, type, chainable ? margin : undefined, chainable);
            };
        });
    });

    jQuery.fn.extend({

        bind: function bind(types, data, fn) {
            return this.on(types, null, data, fn);
        },
        unbind: function unbind(types, fn) {
            return this.off(types, null, fn);
        },

        delegate: function delegate(selector, types, data, fn) {
            return this.on(types, selector, data, fn);
        },
        undelegate: function undelegate(selector, types, fn) {

            // ( namespace ) or ( selector, types [, fn] )
            return arguments.length === 1 ? this.off(selector, "**") : this.off(types, selector || "**", fn);
        }
    });

    jQuery.parseJSON = JSON.parse;

    // Register as a named AMD module, since jQuery can be concatenated with other
    // files that may use define, but not via a proper concatenation script that
    // understands anonymous AMD modules. A named AMD is safest and most robust
    // way to register. Lowercase jquery is used because AMD module names are
    // derived from file names, and jQuery is normally delivered in a lowercase
    // file name. Do this after creating the global so that if an AMD module wants
    // to call noConflict to hide this version of jQuery, it will work.

    // Note that for maximum portability, libraries that are not jQuery should
    // declare themselves as anonymous modules, and avoid setting a global if an
    // AMD loader is present. jQuery is a special case. For more information, see
    // https://github.com/jrburke/requirejs/wiki/Updating-existing-libraries#wiki-anon

    if (typeof define === "function" && define.amd) {
        define("jquery", [], function () {
            return jQuery;
        });
    }

    var

	// Map over jQuery in case of overwrite
	_jQuery = window.jQuery,


	// Map over the $ in case of overwrite
	_$ = window.$;

    jQuery.noConflict = function (deep) {
        if (window.$ === jQuery) {
            window.$ = _$;
        }

        if (deep && window.jQuery === jQuery) {
            window.jQuery = _jQuery;
        }

        return jQuery;
    };

    // Expose jQuery and $ identifiers, even in AMD
    // (#7102#comment:10, https://github.com/jquery/jquery/pull/557)
    // and CommonJS for browser emulators (#13566)
    if (!noGlobal) {
        window.jQuery = window.$ = jQuery;
    }

    return jQuery;
});
"use strict";

var _typeof = typeof Symbol === "function" && typeof Symbol.iterator === "symbol" ? function (obj) { return typeof obj; } : function (obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; };

!function (t, e) {
    "function" == typeof define && define.amd ? define(e) : "object" == (typeof module === "undefined" ? "undefined" : _typeof(module)) && module.exports ? module.exports = e() : t.EvEmitter = e();
}("undefined" != typeof window ? window : undefined, function () {
    "use strict";
    function t() { } var e = t.prototype; return e.on = function (t, e) {
        if (t && e) {
            var n = this._events = this._events || {},
                i = n[t] = n[t] || []; return i.indexOf(e) == -1 && i.push(e), this;
        }
    }, e.once = function (t, e) {
        if (t && e) {
            this.on(t, e); var n = this._onceEvents = this._onceEvents || {},
                i = n[t] = n[t] || {}; return i[e] = !0, this;
        }
    }, e.off = function (t, e) {
        var n = this._events && this._events[t]; if (n && n.length) {
            var i = n.indexOf(e); return i != -1 && n.splice(i, 1), this;
        }
    }, e.emitEvent = function (t, e) {
        var n = this._events && this._events[t]; if (n && n.length) {
            var i = 0,
                s = n[i]; e = e || []; for (var o = this._onceEvents && this._onceEvents[t]; s;) {
                    var f = o && o[s]; f && (this.off(t, s), delete o[s]), s.apply(this, e), i += f ? 0 : 1, s = n[i];
                } return this;
        }
    }, t;
});
!function (t, e) {
    "use strict";
    "function" == typeof define && define.amd ? define(function () {
        return e();
    }) : "object" == (typeof module === "undefined" ? "undefined" : _typeof(module)) && module.exports ? module.exports = e() : t.getSize = e();
}(window, function () {
    "use strict";
    function t(t) {
        var e = parseFloat(t),
            i = t.indexOf("%") == -1 && !isNaN(e); return i && e;
    } function e() { } function i() {
        for (var t = { width: 0, height: 0, innerWidth: 0, innerHeight: 0, outerWidth: 0, outerHeight: 0 }, e = 0; e < g; e++) {
            var i = a[e]; t[i] = 0;
        } return t;
    } function r(t) {
        var e = getComputedStyle(t); return e || h("Style returned " + e + ". Are you running this code in a hidden iframe on Firefox? See http://bit.ly/getsizebug1"), e;
    } function o() {
        if (!u) {
            u = !0; var e = document.createElement("div"); e.style.width = "200px", e.style.padding = "1px 2px 3px 4px", e.style.borderStyle = "solid", e.style.borderWidth = "1px 2px 3px 4px", e.style.boxSizing = "border-box"; var i = document.body || document.documentElement; i.appendChild(e); var o = r(e); n.isBoxSizeOuter = d = 200 == t(o.width), i.removeChild(e);
        }
    } function n(e) {
        if (o(), "string" == typeof e && (e = document.querySelector(e)), e && "object" == (typeof e === "undefined" ? "undefined" : _typeof(e)) && e.nodeType) {
            var n = r(e); if ("none" == n.display) return i(); var h = {}; h.width = e.offsetWidth, h.height = e.offsetHeight; for (var u = h.isBorderBox = "border-box" == n.boxSizing, p = 0; p < g; p++) {
                var f = a[p],
                    m = n[f],
                    s = parseFloat(m); h[f] = isNaN(s) ? 0 : s;
            } var c = h.paddingLeft + h.paddingRight,
                l = h.paddingTop + h.paddingBottom,
                b = h.marginLeft + h.marginRight,
                x = h.marginTop + h.marginBottom,
                y = h.borderLeftWidth + h.borderRightWidth,
                v = h.borderTopWidth + h.borderBottomWidth,
                W = u && d,
                w = t(n.width); w !== !1 && (h.width = w + (W ? 0 : c + y)); var B = t(n.height); return B !== !1 && (h.height = B + (W ? 0 : l + v)), h.innerWidth = h.width - (c + y), h.innerHeight = h.height - (l + v), h.outerWidth = h.width + b, h.outerHeight = h.height + x, h;
        }
    } var d,
        h = "undefined" == typeof console ? e : function (t) {
            console.error(t);
        },
        a = ["paddingLeft", "paddingRight", "paddingTop", "paddingBottom", "marginLeft", "marginRight", "marginTop", "marginBottom", "borderLeftWidth", "borderRightWidth", "borderTopWidth", "borderBottomWidth"],
        g = a.length,
        u = !1; return n;
});
!function (e, t) {
    "use strict";
    "function" == typeof define && define.amd ? define(t) : "object" == (typeof module === "undefined" ? "undefined" : _typeof(module)) && module.exports ? module.exports = t() : e.matchesSelector = t();
}(window, function () {
    "use strict";
    var e = function () {
        var e = window.Element.prototype; if (e.matches) return "matches"; if (e.matchesSelector) return "matchesSelector"; for (var t = ["webkit", "moz", "ms", "o"], o = 0; o < t.length; o++) {
            var r = t[o],
                n = r + "MatchesSelector"; if (e[n]) return n;
        }
    }(); return function (t, o) {
        return t[e](o);
    };
});
!function (e, t) {
    "function" == typeof define && define.amd ? define(["desandro-matches-selector/matches-selector"], function (r) {
        return t(e, r);
    }) : "object" == (typeof module === "undefined" ? "undefined" : _typeof(module)) && module.exports ? module.exports = t(e, require("desandro-matches-selector")) : e.fizzyUIUtils = t(e, e.matchesSelector);
}(window, function (e, t) {
    "use strict";
    var r = {}; r.extend = function (e, t) {
        for (var r in t) {
            e[r] = t[r];
        } return e;
    }, r.modulo = function (e, t) {
        return (e % t + t) % t;
    }, r.makeArray = function (e) {
        var t = []; if (Array.isArray(e)) t = e; else if (e && "object" == (typeof e === "undefined" ? "undefined" : _typeof(e)) && "number" == typeof e.length) for (var r = 0; r < e.length; r++) {
            t.push(e[r]);
        } else t.push(e); return t;
    }, r.removeFrom = function (e, t) {
        var r = e.indexOf(t); r != -1 && e.splice(r, 1);
    }, r.getParent = function (e, r) {
        for (; e != document.body;) {
            if (e = e.parentNode, t(e, r)) return e;
        }
    }, r.getQueryElement = function (e) {
        return "string" == typeof e ? document.querySelector(e) : e;
    }, r.handleEvent = function (e) {
        var t = "on" + e.type; this[t] && this[t](e);
    }, r.filterFindElements = function (e, n) {
        e = r.makeArray(e); var o = []; return e.forEach(function (e) {
            if (e instanceof HTMLElement) {
                if (!n) return void o.push(e); t(e, n) && o.push(e); for (var r = e.querySelectorAll(n), u = 0; u < r.length; u++) {
                    o.push(r[u]);
                }
            }
        }), o;
    }, r.debounceMethod = function (e, t, r) {
        var n = e.prototype[t],
            o = t + "Timeout"; e.prototype[t] = function () {
                var e = this[o]; e && clearTimeout(e); var t = arguments,
                    u = this; this[o] = setTimeout(function () {
                        n.apply(u, t), delete u[o];
                    }, r || 100);
            };
    }, r.docReady = function (e) {
        var t = document.readyState; "complete" == t || "interactive" == t ? setTimeout(e) : document.addEventListener("DOMContentLoaded", e);
    }, r.toDashed = function (e) {
        return e.replace(/(.)([A-Z])/g, function (e, t, r) {
            return t + "-" + r;
        }).toLowerCase();
    }; var n = e.console; return r.htmlInit = function (t, o) {
        r.docReady(function () {
            var u = r.toDashed(o),
                a = "data-" + u,
                i = document.querySelectorAll("[" + a + "]"),
                c = document.querySelectorAll(".js-" + u),
                f = r.makeArray(i).concat(r.makeArray(c)),
                s = a + "-options",
                d = e.jQuery; f.forEach(function (e) {
                    var r,
                        u = e.getAttribute(a) || e.getAttribute(s); try {
                            r = u && JSON.parse(u);
                        } catch (i) {
                            return void (n && n.error("Error parsing " + a + " on " + e.className + ": " + i));
                        } var c = new t(e, r); d && d.data(e, o, c);
                });
        });
    }, r;
});
!function (t, i) {
    "function" == typeof define && define.amd ? define(["ev-emitter/ev-emitter", "get-size/get-size"], i) : "object" == (typeof module === "undefined" ? "undefined" : _typeof(module)) && module.exports ? module.exports = i(require("ev-emitter"), require("get-size")) : (t.Outlayer = {}, t.Outlayer.Item = i(t.EvEmitter, t.getSize));
}(window, function (t, i) {
    "use strict";
    function n(t) {
        for (var i in t) {
            return !1;
        } return i = null, !0;
    } function o(t, i) {
        t && (this.element = t, this.layout = i, this.position = { x: 0, y: 0 }, this._create());
    } function e(t) {
        return t.replace(/([A-Z])/g, function (t) {
            return "-" + t.toLowerCase();
        });
    } var s = document.documentElement.style,
        r = "string" == typeof s.transition ? "transition" : "WebkitTransition",
        a = "string" == typeof s.transform ? "transform" : "WebkitTransform",
        h = { WebkitTransition: "webkitTransitionEnd", transition: "transitionend" }[r],
        l = { transform: a, transition: r, transitionDuration: r + "Duration", transitionProperty: r + "Property", transitionDelay: r + "Delay" },
        u = o.prototype = Object.create(t.prototype); u.constructor = o, u._create = function () {
            this._transn = { ingProperties: {}, clean: {}, onEnd: {} }, this.css({ position: "absolute" });
        }, u.handleEvent = function (t) {
            var i = "on" + t.type; this[i] && this[i](t);
        }, u.getSize = function () {
            this.size = i(this.element);
        }, u.css = function (t) {
            var i = this.element.style; for (var n in t) {
                var o = l[n] || n; i[o] = t[n];
            }
        }, u.getPosition = function () {
            var t = getComputedStyle(this.element),
                i = this.layout._getOption("originLeft"),
                n = this.layout._getOption("originTop"),
                o = t[i ? "left" : "right"],
                e = t[n ? "top" : "bottom"],
                s = this.layout.size,
                r = o.indexOf("%") != -1 ? parseFloat(o) / 100 * s.width : parseInt(o, 10),
                a = e.indexOf("%") != -1 ? parseFloat(e) / 100 * s.height : parseInt(e, 10); r = isNaN(r) ? 0 : r, a = isNaN(a) ? 0 : a, r -= i ? s.paddingLeft : s.paddingRight, a -= n ? s.paddingTop : s.paddingBottom, this.position.x = r, this.position.y = a;
        }, u.layoutPosition = function () {
            var t = this.layout.size,
                i = {},
                n = this.layout._getOption("originLeft"),
                o = this.layout._getOption("originTop"),
                e = n ? "paddingLeft" : "paddingRight",
                s = n ? "left" : "right",
                r = n ? "right" : "left",
                a = this.position.x + t[e]; i[s] = this.getXValue(a), i[r] = ""; var h = o ? "paddingTop" : "paddingBottom",
                l = o ? "top" : "bottom",
                u = o ? "bottom" : "top",
                d = this.position.y + t[h]; i[l] = this.getYValue(d), i[u] = "", this.css(i), this.emitEvent("layout", [this]);
        }, u.getXValue = function (t) {
            var i = this.layout._getOption("horizontal"); return this.layout.options.percentPosition && !i ? t / this.layout.size.width * 100 + "%" : t + "px";
        }, u.getYValue = function (t) {
            var i = this.layout._getOption("horizontal"); return this.layout.options.percentPosition && i ? t / this.layout.size.height * 100 + "%" : t + "px";
        }, u._transitionTo = function (t, i) {
            this.getPosition(); var n = this.position.x,
                o = this.position.y,
                e = parseInt(t, 10),
                s = parseInt(i, 10),
                r = e === this.position.x && s === this.position.y; if (this.setPosition(t, i), r && !this.isTransitioning) return void this.layoutPosition(); var a = t - n,
                h = i - o,
                l = {}; l.transform = this.getTranslate(a, h), this.transition({ to: l, onTransitionEnd: { transform: this.layoutPosition }, isCleaning: !0 });
        }, u.getTranslate = function (t, i) {
            var n = this.layout._getOption("originLeft"),
                o = this.layout._getOption("originTop"); return t = n ? t : -t, i = o ? i : -i, "translate3d(" + t + "px, " + i + "px, 0)";
        }, u.goTo = function (t, i) {
            this.setPosition(t, i), this.layoutPosition();
        }, u.moveTo = u._transitionTo, u.setPosition = function (t, i) {
            this.position.x = parseInt(t, 10), this.position.y = parseInt(i, 10);
        }, u._nonTransition = function (t) {
            this.css(t.to), t.isCleaning && this._removeStyles(t.to); for (var i in t.onTransitionEnd) {
                t.onTransitionEnd[i].call(this);
            }
        }, u.transition = function (t) {
            if (!parseFloat(this.layout.options.transitionDuration)) return void this._nonTransition(t); var i = this._transn; for (var n in t.onTransitionEnd) {
                i.onEnd[n] = t.onTransitionEnd[n];
            } for (n in t.to) {
                i.ingProperties[n] = !0, t.isCleaning && (i.clean[n] = !0);
            } if (t.from) {
                this.css(t.from); var o = this.element.offsetHeight; o = null;
            } this.enableTransition(t.to), this.css(t.to), this.isTransitioning = !0;
        }; var d = "opacity," + e(a); u.enableTransition = function () {
            if (!this.isTransitioning) {
                var t = this.layout.options.transitionDuration; t = "number" == typeof t ? t + "ms" : t, this.css({ transitionProperty: d, transitionDuration: t, transitionDelay: this.staggerDelay || 0 }), this.element.addEventListener(h, this, !1);
            }
        }, u.onwebkitTransitionEnd = function (t) {
            this.ontransitionend(t);
        }, u.onotransitionend = function (t) {
            this.ontransitionend(t);
        }; var p = { "-webkit-transform": "transform" }; u.ontransitionend = function (t) {
            if (t.target === this.element) {
                var i = this._transn,
                    o = p[t.propertyName] || t.propertyName; if (delete i.ingProperties[o], n(i.ingProperties) && this.disableTransition(), o in i.clean && (this.element.style[t.propertyName] = "", delete i.clean[o]), o in i.onEnd) {
                        var e = i.onEnd[o]; e.call(this), delete i.onEnd[o];
                    } this.emitEvent("transitionEnd", [this]);
            }
        }, u.disableTransition = function () {
            this.removeTransitionStyles(), this.element.removeEventListener(h, this, !1), this.isTransitioning = !1;
        }, u._removeStyles = function (t) {
            var i = {}; for (var n in t) {
                i[n] = "";
            } this.css(i);
        }; var f = { transitionProperty: "", transitionDuration: "", transitionDelay: "" }; return u.removeTransitionStyles = function () {
            this.css(f);
        }, u.stagger = function (t) {
            t = isNaN(t) ? 0 : t, this.staggerDelay = t + "ms";
        }, u.removeElem = function () {
            this.element.parentNode.removeChild(this.element), this.css({ display: "" }), this.emitEvent("remove", [this]);
        }, u.remove = function () {
            return r && parseFloat(this.layout.options.transitionDuration) ? (this.once("transitionEnd", function () {
                this.removeElem();
            }), void this.hide()) : void this.removeElem();
        }, u.reveal = function () {
            delete this.isHidden, this.css({ display: "" }); var t = this.layout.options,
                i = {},
                n = this.getHideRevealTransitionEndProperty("visibleStyle"); i[n] = this.onRevealTransitionEnd, this.transition({ from: t.hiddenStyle, to: t.visibleStyle, isCleaning: !0, onTransitionEnd: i });
        }, u.onRevealTransitionEnd = function () {
            this.isHidden || this.emitEvent("reveal");
        }, u.getHideRevealTransitionEndProperty = function (t) {
            var i = this.layout.options[t]; if (i.opacity) return "opacity"; for (var n in i) {
                return n;
            }
        }, u.hide = function () {
            this.isHidden = !0, this.css({ display: "" }); var t = this.layout.options,
                i = {},
                n = this.getHideRevealTransitionEndProperty("hiddenStyle"); i[n] = this.onHideTransitionEnd, this.transition({ from: t.visibleStyle, to: t.hiddenStyle, isCleaning: !0, onTransitionEnd: i });
        }, u.onHideTransitionEnd = function () {
            this.isHidden && (this.css({ display: "none" }), this.emitEvent("hide"));
        }, u.destroy = function () {
            this.css({ position: "", left: "", right: "", top: "", bottom: "", transition: "", transform: "" });
        }, o;
});
!function (t, e) {
    "use strict";
    "function" == typeof define && define.amd ? define(["ev-emitter/ev-emitter", "get-size/get-size", "fizzy-ui-utils/utils", "./item"], function (i, n, s, o) {
        return e(t, i, n, s, o);
    }) : "object" == (typeof module === "undefined" ? "undefined" : _typeof(module)) && module.exports ? module.exports = e(t, require("ev-emitter"), require("get-size"), require("fizzy-ui-utils"), require("./item")) : t.Outlayer = e(t, t.EvEmitter, t.getSize, t.fizzyUIUtils, t.Outlayer.Item);
}(window, function (t, e, i, n, s) {
    "use strict";
    function o(t, e) {
        var i = n.getQueryElement(t); if (!i) return void (h && h.error("Bad element for " + this.constructor.namespace + ": " + (i || t))); this.element = i, u && (this.$element = u(this.element)), this.options = n.extend({}, this.constructor.defaults), this.option(e); var s = ++c; this.element.outlayerGUID = s, f[s] = this, this._create(); var o = this._getOption("initLayout"); o && this.layout();
    } function r(t) {
        function e() {
            t.apply(this, arguments);
        } return e.prototype = Object.create(t.prototype), e.prototype.constructor = e, e;
    } function a(t) {
        if ("number" == typeof t) return t; var e = t.match(/(^\d*\.?\d*)(\w*)/),
            i = e && e[1],
            n = e && e[2]; if (!i.length) return 0; i = parseFloat(i); var s = d[n] || 1; return i * s;
    } var h = t.console,
        u = t.jQuery,
        m = function m() { },
        c = 0,
        f = {}; o.namespace = "outlayer", o.Item = s, o.defaults = { containerStyle: { position: "relative" }, initLayout: !0, originLeft: !0, originTop: !0, resize: !0, resizeContainer: !0, transitionDuration: "0.4s", hiddenStyle: { opacity: 0, transform: "scale(0.001)" }, visibleStyle: { opacity: 1, transform: "scale(1)" } }; var l = o.prototype; n.extend(l, e.prototype), l.option = function (t) {
            n.extend(this.options, t);
        }, l._getOption = function (t) {
            var e = this.constructor.compatOptions[t]; return e && void 0 !== this.options[e] ? this.options[e] : this.options[t];
        }, o.compatOptions = { initLayout: "isInitLayout", horizontal: "isHorizontal", layoutInstant: "isLayoutInstant", originLeft: "isOriginLeft", originTop: "isOriginTop", resize: "isResizeBound", resizeContainer: "isResizingContainer" }, l._create = function () {
            this.reloadItems(), this.stamps = [], this.stamp(this.options.stamp), n.extend(this.element.style, this.options.containerStyle); var t = this._getOption("resize"); t && this.bindResize();
        }, l.reloadItems = function () {
            this.items = this._itemize(this.element.children);
        }, l._itemize = function (t) {
            for (var e = this._filterFindItemElements(t), i = this.constructor.Item, n = [], s = 0; s < e.length; s++) {
                var o = e[s],
                    r = new i(o, this); n.push(r);
            } return n;
        }, l._filterFindItemElements = function (t) {
            return n.filterFindElements(t, this.options.itemSelector);
        }, l.getItemElements = function () {
            return this.items.map(function (t) {
                return t.element;
            });
        }, l.layout = function () {
            this._resetLayout(), this._manageStamps(); var t = this._getOption("layoutInstant"),
                e = void 0 !== t ? t : !this._isLayoutInited; this.layoutItems(this.items, e), this._isLayoutInited = !0;
        }, l._init = l.layout, l._resetLayout = function () {
            this.getSize();
        }, l.getSize = function () {
            this.size = i(this.element);
        }, l._getMeasurement = function (t, e) {
            var n,
                s = this.options[t]; s ? ("string" == typeof s ? n = this.element.querySelector(s) : s instanceof HTMLElement && (n = s), this[t] = n ? i(n)[e] : s) : this[t] = 0;
        }, l.layoutItems = function (t, e) {
            t = this._getItemsForLayout(t), this._layoutItems(t, e), this._postLayout();
        }, l._getItemsForLayout = function (t) {
            return t.filter(function (t) {
                return !t.isIgnored;
            });
        }, l._layoutItems = function (t, e) {
            if (this._emitCompleteOnItems("layout", t), t && t.length) {
                var i = []; t.forEach(function (t) {
                    var n = this._getItemLayoutPosition(t); n.item = t, n.isInstant = e || t.isLayoutInstant, i.push(n);
                }, this), this._processLayoutQueue(i);
            }
        }, l._getItemLayoutPosition = function () {
            return { x: 0, y: 0 };
        }, l._processLayoutQueue = function (t) {
            this.updateStagger(), t.forEach(function (t, e) {
                this._positionItem(t.item, t.x, t.y, t.isInstant, e);
            }, this);
        }, l.updateStagger = function () {
            var t = this.options.stagger; return null === t || void 0 === t ? void (this.stagger = 0) : (this.stagger = a(t), this.stagger);
        }, l._positionItem = function (t, e, i, n, s) {
            n ? t.goTo(e, i) : (t.stagger(s * this.stagger), t.moveTo(e, i));
        }, l._postLayout = function () {
            this.resizeContainer();
        }, l.resizeContainer = function () {
            var t = this._getOption("resizeContainer"); if (t) {
                var e = this._getContainerSize(); e && (this._setContainerMeasure(e.width, !0), this._setContainerMeasure(e.height, !1));
            }
        }, l._getContainerSize = m, l._setContainerMeasure = function (t, e) {
            if (void 0 !== t) {
                var i = this.size; i.isBorderBox && (t += e ? i.paddingLeft + i.paddingRight + i.borderLeftWidth + i.borderRightWidth : i.paddingBottom + i.paddingTop + i.borderTopWidth + i.borderBottomWidth), t = Math.max(t, 0), this.element.style[e ? "width" : "height"] = t + "px";
            }
        }, l._emitCompleteOnItems = function (t, e) {
            function i() {
                s.dispatchEvent(t + "Complete", null, [e]);
            } function n() {
                r++, r == o && i();
            } var s = this,
                o = e.length; if (!e || !o) return void i(); var r = 0; e.forEach(function (e) {
                    e.once(t, n);
                });
        }, l.dispatchEvent = function (t, e, i) {
            var n = e ? [e].concat(i) : i; if (this.emitEvent(t, n), u) if (this.$element = this.$element || u(this.element), e) {
                var s = u.Event(e); s.type = t, this.$element.trigger(s, i);
            } else this.$element.trigger(t, i);
        }, l.ignore = function (t) {
            var e = this.getItem(t); e && (e.isIgnored = !0);
        }, l.unignore = function (t) {
            var e = this.getItem(t); e && delete e.isIgnored;
        }, l.stamp = function (t) {
            t = this._find(t), t && (this.stamps = this.stamps.concat(t), t.forEach(this.ignore, this));
        }, l.unstamp = function (t) {
            t = this._find(t), t && t.forEach(function (t) {
                n.removeFrom(this.stamps, t), this.unignore(t);
            }, this);
        }, l._find = function (t) {
            if (t) return "string" == typeof t && (t = this.element.querySelectorAll(t)), t = n.makeArray(t);
        }, l._manageStamps = function () {
            this.stamps && this.stamps.length && (this._getBoundingRect(), this.stamps.forEach(this._manageStamp, this));
        }, l._getBoundingRect = function () {
            var t = this.element.getBoundingClientRect(),
                e = this.size; this._boundingRect = { left: t.left + e.paddingLeft + e.borderLeftWidth, top: t.top + e.paddingTop + e.borderTopWidth, right: t.right - (e.paddingRight + e.borderRightWidth), bottom: t.bottom - (e.paddingBottom + e.borderBottomWidth) };
        }, l._manageStamp = m, l._getElementOffset = function (t) {
            var e = t.getBoundingClientRect(),
                n = this._boundingRect,
                s = i(t),
                o = { left: e.left - n.left - s.marginLeft, top: e.top - n.top - s.marginTop, right: n.right - e.right - s.marginRight, bottom: n.bottom - e.bottom - s.marginBottom }; return o;
        }, l.handleEvent = n.handleEvent, l.bindResize = function () {
            t.addEventListener("resize", this), this.isResizeBound = !0;
        }, l.unbindResize = function () {
            t.removeEventListener("resize", this), this.isResizeBound = !1;
        }, l.onresize = function () {
            this.resize();
        }, n.debounceMethod(o, "onresize", 100), l.resize = function () {
            this.isResizeBound && this.needsResizeLayout() && this.layout();
        }, l.needsResizeLayout = function () {
            var t = i(this.element),
                e = this.size && t; return e && t.innerWidth !== this.size.innerWidth;
        }, l.addItems = function (t) {
            var e = this._itemize(t); return e.length && (this.items = this.items.concat(e)), e;
        }, l.appended = function (t) {
            var e = this.addItems(t); e.length && (this.layoutItems(e, !0), this.reveal(e));
        }, l.prepended = function (t) {
            var e = this._itemize(t); if (e.length) {
                var i = this.items.slice(0); this.items = e.concat(i), this._resetLayout(), this._manageStamps(), this.layoutItems(e, !0), this.reveal(e), this.layoutItems(i);
            }
        }, l.reveal = function (t) {
            if (this._emitCompleteOnItems("reveal", t), t && t.length) {
                var e = this.updateStagger(); t.forEach(function (t, i) {
                    t.stagger(i * e), t.reveal();
                });
            }
        }, l.hide = function (t) {
            if (this._emitCompleteOnItems("hide", t), t && t.length) {
                var e = this.updateStagger(); t.forEach(function (t, i) {
                    t.stagger(i * e), t.hide();
                });
            }
        }, l.revealItemElements = function (t) {
            var e = this.getItems(t); this.reveal(e);
        }, l.hideItemElements = function (t) {
            var e = this.getItems(t); this.hide(e);
        }, l.getItem = function (t) {
            for (var e = 0; e < this.items.length; e++) {
                var i = this.items[e]; if (i.element == t) return i;
            }
        }, l.getItems = function (t) {
            t = n.makeArray(t); var e = []; return t.forEach(function (t) {
                var i = this.getItem(t); i && e.push(i);
            }, this), e;
        }, l.remove = function (t) {
            var e = this.getItems(t); this._emitCompleteOnItems("remove", e), e && e.length && e.forEach(function (t) {
                t.remove(), n.removeFrom(this.items, t);
            }, this);
        }, l.destroy = function () {
            var t = this.element.style; t.height = "", t.position = "", t.width = "", this.items.forEach(function (t) {
                t.destroy();
            }), this.unbindResize(); var e = this.element.outlayerGUID; delete f[e], delete this.element.outlayerGUID, u && u.removeData(this.element, this.constructor.namespace);
        }, o.data = function (t) {
            t = n.getQueryElement(t); var e = t && t.outlayerGUID; return e && f[e];
        }, o.create = function (t, e) {
            var i = r(o); return i.defaults = n.extend({}, o.defaults), n.extend(i.defaults, e), i.compatOptions = n.extend({}, o.compatOptions), i.namespace = t, i.data = o.data, i.Item = r(s), n.htmlInit(i, t), u && u.bridget && u.bridget(t, i), i;
        }; var d = { ms: 1, s: 1e3 }; return o.Item = s, o;
});
!function (t, i) {
    "function" == typeof define && define.amd ? define(["outlayer/outlayer", "get-size/get-size"], i) : "object" == (typeof module === "undefined" ? "undefined" : _typeof(module)) && module.exports ? module.exports = i(require("outlayer"), require("get-size")) : t.Masonry = i(t.Outlayer, t.getSize);
}(window, function (t, i) {
    "use strict";
    var e = t.create("masonry"); return e.compatOptions.fitWidth = "isFitWidth", e.prototype._resetLayout = function () {
        this.getSize(), this._getMeasurement("columnWidth", "outerWidth"), this._getMeasurement("gutter", "outerWidth"), this.measureColumns(), this.colYs = []; for (var t = 0; t < this.cols; t++) {
            this.colYs.push(0);
        } this.maxY = 0;
    }, e.prototype.measureColumns = function () {
        if (this.getContainerWidth(), !this.columnWidth) {
            var t = this.items[0],
                e = t && t.element; this.columnWidth = e && i(e).outerWidth || this.containerWidth;
        } var o = this.columnWidth += this.gutter,
            h = this.containerWidth + this.gutter,
            s = h / o,
            n = o - h % o,
            r = n && n < 1 ? "round" : "floor"; s = Math[r](s), this.cols = Math.max(s, 1);
    }, e.prototype.getContainerWidth = function () {
        var t = this._getOption("fitWidth"),
            e = t ? this.element.parentNode : this.element,
            o = i(e); this.containerWidth = o && o.innerWidth;
    }, e.prototype._getItemLayoutPosition = function (t) {
        t.getSize(); var i = t.size.outerWidth % this.columnWidth,
            e = i && i < 1 ? "round" : "ceil",
            o = Math[e](t.size.outerWidth / this.columnWidth); o = Math.min(o, this.cols); for (var h = this._getColGroup(o), s = Math.min.apply(Math, h), n = h.indexOf(s), r = { x: this.columnWidth * n, y: s }, a = s + t.size.outerHeight, u = this.cols + 1 - h.length, l = 0; l < u; l++) {
                this.colYs[n + l] = a;
            } return r;
    }, e.prototype._getColGroup = function (t) {
        if (t < 2) return this.colYs; for (var i = [], e = this.cols + 1 - t, o = 0; o < e; o++) {
            var h = this.colYs.slice(o, o + t); i[o] = Math.max.apply(Math, h);
        } return i;
    }, e.prototype._manageStamp = function (t) {
        var e = i(t),
            o = this._getElementOffset(t),
            h = this._getOption("originLeft"),
            s = h ? o.left : o.right,
            n = s + e.outerWidth,
            r = Math.floor(s / this.columnWidth); r = Math.max(0, r); var a = Math.floor(n / this.columnWidth); a -= n % this.columnWidth ? 0 : 1, a = Math.min(this.cols - 1, a); for (var u = this._getOption("originTop"), l = (u ? o.top : o.bottom) + e.outerHeight, c = r; c <= a; c++) {
                this.colYs[c] = Math.max(l, this.colYs[c]);
            }
    }, e.prototype._getContainerSize = function () {
        this.maxY = Math.max.apply(Math, this.colYs); var t = { height: this.maxY }; return this._getOption("fitWidth") && (t.width = this._getContainerFitWidth()), t;
    }, e.prototype._getContainerFitWidth = function () {
        for (var t = 0, i = this.cols; --i && 0 === this.colYs[i];) {
            t++;
        } return (this.cols - t) * this.columnWidth - this.gutter;
    }, e.prototype.needsResizeLayout = function () {
        var t = this.containerWidth; return this.getContainerWidth(), t != this.containerWidth;
    }, e;
});
!function (t, e) {
    "use strict";
    "function" == typeof define && define.amd ? define(["ev-emitter/ev-emitter"], function (i) {
        return e(t, i);
    }) : "object" == (typeof module === "undefined" ? "undefined" : _typeof(module)) && module.exports ? module.exports = e(t, require("ev-emitter")) : t.imagesLoaded = e(t, t.EvEmitter);
}(window, function (t, e) {
    "use strict";
    function i(t, e) {
        for (var i in e) {
            t[i] = e[i];
        } return t;
    } function o(t) {
        var e = []; if (Array.isArray(t)) e = t; else if ("number" == typeof t.length) for (var i = 0; i < t.length; i++) {
            e.push(t[i]);
        } else e.push(t); return e;
    } function r(t, e, n) {
        return this instanceof r ? ("string" == typeof t && (t = document.querySelectorAll(t)), this.elements = o(t), this.options = i({}, this.options), "function" == typeof e ? n = e : i(this.options, e), n && this.on("always", n), this.getImages(), h && (this.jqDeferred = new h.Deferred()), void setTimeout(function () {
            this.check();
        }.bind(this))) : new r(t, e, n);
    } function n(t) {
        this.img = t;
    } function s(t, e) {
        this.url = t, this.element = e, this.img = new Image();
    } var h = t.jQuery,
        a = t.console; r.prototype = Object.create(e.prototype), r.prototype.options = {}, r.prototype.getImages = function () {
            this.images = [], this.elements.forEach(this.addElementImages, this);
        }, r.prototype.addElementImages = function (t) {
            "IMG" == t.nodeName && this.addImage(t), this.options.background === !0 && this.addElementBackgroundImages(t); var e = t.nodeType; if (e && d[e]) {
                for (var i = t.querySelectorAll("img"), o = 0; o < i.length; o++) {
                    var r = i[o]; this.addImage(r);
                } if ("string" == typeof this.options.background) {
                    var n = t.querySelectorAll(this.options.background); for (o = 0; o < n.length; o++) {
                        var s = n[o]; this.addElementBackgroundImages(s);
                    }
                }
            }
        }; var d = { 1: !0, 9: !0, 11: !0 }; return r.prototype.addElementBackgroundImages = function (t) {
            var e = getComputedStyle(t); if (e) for (var i = /url\((['"])?(.*?)\1\)/gi, o = i.exec(e.backgroundImage) ; null !== o;) {
                var r = o && o[2]; r && this.addBackground(r, t), o = i.exec(e.backgroundImage);
            }
        }, r.prototype.addImage = function (t) {
            var e = new n(t); this.images.push(e);
        }, r.prototype.addBackground = function (t, e) {
            var i = new s(t, e); this.images.push(i);
        }, r.prototype.check = function () {
            function t(t, i, o) {
                setTimeout(function () {
                    e.progress(t, i, o);
                });
            } var e = this; return this.progressedCount = 0, this.hasAnyBroken = !1, this.images.length ? void this.images.forEach(function (e) {
                e.once("progress", t), e.check();
            }) : void this.complete();
        }, r.prototype.progress = function (t, e, i) {
            this.progressedCount++, this.hasAnyBroken = this.hasAnyBroken || !t.isLoaded, this.emitEvent("progress", [this, t, e]), this.jqDeferred && this.jqDeferred.notify && this.jqDeferred.notify(this, t), this.progressedCount == this.images.length && this.complete(), this.options.debug && a && a.log("progress: " + i, t, e);
        }, r.prototype.complete = function () {
            var t = this.hasAnyBroken ? "fail" : "done"; if (this.isComplete = !0, this.emitEvent(t, [this]), this.emitEvent("always", [this]), this.jqDeferred) {
                var e = this.hasAnyBroken ? "reject" : "resolve"; this.jqDeferred[e](this);
            }
        }, n.prototype = Object.create(e.prototype), n.prototype.check = function () {
            var t = this.getIsImageComplete(); return t ? void this.confirm(0 !== this.img.naturalWidth, "naturalWidth") : (this.proxyImage = new Image(), this.proxyImage.addEventListener("load", this), this.proxyImage.addEventListener("error", this), this.img.addEventListener("load", this), this.img.addEventListener("error", this), void (this.proxyImage.src = this.img.src));
        }, n.prototype.getIsImageComplete = function () {
            return this.img.complete && void 0 !== this.img.naturalWidth;
        }, n.prototype.confirm = function (t, e) {
            this.isLoaded = t, this.emitEvent("progress", [this, this.img, e]);
        }, n.prototype.handleEvent = function (t) {
            var e = "on" + t.type; this[e] && this[e](t);
        }, n.prototype.onload = function () {
            this.confirm(!0, "onload"), this.unbindEvents();
        }, n.prototype.onerror = function () {
            this.confirm(!1, "onerror"), this.unbindEvents();
        }, n.prototype.unbindEvents = function () {
            this.proxyImage.removeEventListener("load", this), this.proxyImage.removeEventListener("error", this), this.img.removeEventListener("load", this), this.img.removeEventListener("error", this);
        }, s.prototype = Object.create(n.prototype), s.prototype.check = function () {
            this.img.addEventListener("load", this), this.img.addEventListener("error", this), this.img.src = this.url; var t = this.getIsImageComplete(); t && (this.confirm(0 !== this.img.naturalWidth, "naturalWidth"), this.unbindEvents());
        }, s.prototype.unbindEvents = function () {
            this.img.removeEventListener("load", this), this.img.removeEventListener("error", this);
        }, s.prototype.confirm = function (t, e) {
            this.isLoaded = t, this.emitEvent("progress", [this, this.element, e]);
        }, r.makeJQueryPlugin = function (e) {
            e = e || t.jQuery, e && (h = e, h.fn.imagesLoaded = function (t, e) {
                var i = new r(this, t, e); return i.jqDeferred.promise(h(this));
            });
        }, r.makeJQueryPlugin(), r;
});
!function () {
    window.FizzyDocs = {}, window.filterBind = function (n, t, i, e) {
        n.addEventListener(t, function (n) {
            matchesSelector(n.target, i) && e(n);
        });
    };
}();
FizzyDocs["commercial-license-agreement"] = function (e) {
    "use strict";
    function t(e) {
        var t = o.querySelector(".is-selected"); t && t.classList.remove("is-selected"), e.classList.add("is-selected"); var i = e.getAttribute("data-license-option"),
            n = r[i]; l.forEach(function (e) {
                e.element.textContent = n[e.property];
            });
    } var r = { developer: { title: "Developer", "for-official": "one (1) Licensed Developer", "for-plain": "one individual Developer" }, team: { title: "Team", "for-official": "up to eight (8) Licensed Developer(s)", "for-plain": "up to 8 Developers" }, organization: { title: "Organization", "for-official": "an unlimited number of Licensed Developer(s)", "for-plain": "an unlimited number of Developers" } },
        o = e.querySelector(".button-group"),
        i = e.querySelector("h2"),
        n = i.cloneNode(!0); n.style.borderTop = "none", n.style.marginTop = 0, n.id = "", n.innerHTML = n.innerHTML.replace("Commercial License", 'Commercial <span data-license-property="title"></span> License'), i.textContent = "", o.parentNode.insertBefore(n, o.nextSibling); for (var l = [], a = e.querySelectorAll("[data-license-property]"), c = 0, s = a.length; c < s; c++) {
            var p = a[c],
                u = { property: p.getAttribute("data-license-property"), element: p }; l.push(u);
        } t(o.querySelector(".button--developer")), filterBind(o, "click", ".button", function (e) {
            t(e.target);
        });
};
FizzyDocs["fizzy-bear-shirt"] = function (e) {
    var t = new Date(2016, 10, 30),
        r = Math.round((t - new Date()) / 864e5); e.querySelector(".fizzy-bear-shirt__title").textContent = "Rainbow bear shirts. Only on sale for " + r + " more days.";
};
!function (t) {
    "use strict";
    function e() {
        var t = new Date(),
            e = t.getMinutes(); e = e < 10 ? "0" + e : e; var n = t.getSeconds(); return n = n < 10 ? "0" + n : n, [t.getHours(), e, n].join(":");
    } function n() {
        c ? (o.style[c] = "opacity 1.0s", o.style.opacity = "0") : o.style.display = "none";
    } var i = t.MasonryDocs = {}; t.filterBindEvent = function (t, e, n, i) {
        t.addEventListener(e, function (t) {
            matchesSelector(t.target, n) && i.call(t.target, t);
        });
    }; var o; document.addEventListener("DOMContentLoaded", function () {
        o = document.querySelector("#notification"); for (var t = document.querySelectorAll("[data-js]"), e = 0; e < t.length; e++) {
            var n = t[e],
                r = n.getAttribute("data-js"),
                a = i[r] || FizzyDocs[r]; a && a(n);
        }
    }), i.getItemElement = function () {
        var t = document.createElement("div"),
            e = Math.random(),
            n = Math.random(),
            i = e > .8 ? "grid-item--width3" : e > .6 ? "grid-item--width2" : "",
            o = n > .8 ? "grid-item--height3" : n > .5 ? "grid-item--height2" : ""; return t.className = "grid-item " + i + " " + o, t;
    }; var r,
        a = document.documentElement,
        c = "string" == typeof a.style.transition ? "transition" : "WebkitTransition",
        s = c ? 1e3 : 1500; i.notify = function (t) {
            o.textContent = t + " at " + e(), o.style[c] = "none", o.style.display = "block", o.style.opacity = "1", clearTimeout(r), r = setTimeout(n, s);
        };
}(window);
MasonryDocs["animate-item-size"] = function (e) {
    "use strict";
    var t = e.querySelector(".grid"),
        i = new Masonry(t, { columnWidth: 60 }); filterBindEvent(t, "click", ".animate-item-size-item__content", function (e) {
            e.target.parentNode.classList.toggle("is-expanded"), i.layout();
        });
};
MasonryDocs["animate-item-size-responsive"] = function (t) {
    "use strict";
    function e(t) {
        var e = getSize(t); t.style[o] = "none", t.style.width = e.width + "px", t.style.height = e.height + "px";
    } function i(t) {
        var e = function e() {
            t.style.width = "", t.style.height = "", t.removeEventListener(r, e, !1);
        }; t.addEventListener(r, e, !1);
    } function n(t, e) {
        var i = getSize(e); t.style.width = i.width + "px", t.style.height = i.height + "px";
    } var s = document.documentElement,
        o = "string" == typeof s.style.transition ? "transition" : "WebkitTransition",
        r = { WebkitTransition: "webkitTransitionEnd", transition: "transitionend" }[o],
        a = t.querySelector(".grid"),
        d = new Masonry(a, { itemSelector: ".animate-item-size-item", columnWidth: ".grid-sizer", percentPosition: !0 }); filterBindEvent(a, "click", ".animate-item-size-item__content", function () {
            var t = this; e(t); var s = t.parentNode; s.classList.toggle("is-expanded"); t.offsetWidth; t.style[o] = "", i(t), n(t, s), d.layout();
        });
};
MasonryDocs["appended-demo"] = function (e) {
    "use strict";
    var n = e.querySelector(".grid"),
        t = new Masonry(n, { columnWidth: 80 }),
        d = e.querySelector(".append-button"); d.addEventListener("click", function () {
            var e = [MasonryDocs.getItemElement(), MasonryDocs.getItemElement(), MasonryDocs.getItemElement()],
                d = document.createDocumentFragment(); d.appendChild(e[0]), d.appendChild(e[1]), d.appendChild(e[2]), n.appendChild(d), t.appended(e);
        });
};
MasonryDocs["destroy-demo"] = function (e) {
    "use strict";
    var o = e.querySelector(".grid"),
        t = { columnWidth: 80 },
        n = new Masonry(o, t),
        r = !0,
        c = e.querySelector(".toggle-button"); c.addEventListener("click", function () {
            r ? n.destroy() : n = new Masonry(o, t), r = !r;
        });
};
MasonryDocs["imagesloaded-callback"] = function (e) {
    "use strict";
    imagesLoaded(e, function () {
        new Masonry(e, { itemSelector: ".grid-image-item", columnWidth: ".grid-sizer", percentPosition: !0 });
    });
};
MasonryDocs["imagesloaded-progress"] = function (e) {
    "use strict";
    var o = new Masonry(e, { itemSelector: ".grid-image-item", columnWidth: ".grid-sizer", percentPosition: !0 }),
        i = imagesLoaded(e); i.on("progress", function () {
            o.layout();
        });
};
MasonryDocs["layout-complete-demo"] = function (t) {
    "use strict";
    var o = t.querySelector(".grid"),
        e = new Masonry(o, { columnWidth: 80 }); e.on("layoutComplete", function (t) {
            MasonryDocs.notify("Masonry layout completed on " + t.length + " items");
        }), filterBindEvent(o, "click", ".grid-item", function (t) {
            t.target.classList.toggle("grid-item--gigante"), e.layout();
        });
};
MasonryDocs["layout-demo"] = function (t) {
    "use strict";
    var i = t.querySelector(".grid"),
        e = new Masonry(i, { columnWidth: 80 }); filterBindEvent(i, "click", ".grid-item", function (t) {
            t.target.classList.toggle("grid-item--gigante"), e.layout();
        });
};
MasonryDocs["prepended-demo"] = function (e) {
    "use strict";
    var n = e.querySelector(".grid"),
        t = new Masonry(n, { columnWidth: 80 }),
        r = e.querySelector(".prepend-button"); r.addEventListener("click", function () {
            var e = [MasonryDocs.getItemElement(), MasonryDocs.getItemElement(), MasonryDocs.getItemElement()],
                r = document.createDocumentFragment(); r.appendChild(e[0]), r.appendChild(e[1]), r.appendChild(e[2]), n.insertBefore(r, n.firstChild), t.prepended(e);
        });
};
MasonryDocs["remove-complete-demo"] = function (e) {
    "use strict";
    var o = e.querySelector(".grid"),
        t = new Masonry(o, { columnWidth: 80 }); t.on("removeComplete", function (e) {
            MasonryDocs.notify("Removed " + e.length + " items");
        }), filterBindEvent(o, "click", ".grid-item", function (e) {
            t.remove(e.target), t.layout();
        });
};
MasonryDocs["remove-demo"] = function (e) {
    "use strict";
    var r = e.querySelector(".grid"),
        t = new Masonry(r, { columnWidth: 80 }); filterBindEvent(r, "click", ".grid-item", function (e) {
            t.remove(e.target), t.layout();
        });
};
MasonryDocs.stagger = function (t) {
    "use strict";
    var e = t.querySelector(".grid"),
        i = new Masonry(e, { columnWidth: 80, stagger: 30 }); filterBindEvent(e, "click", ".grid-item", function (t) {
            t.target.classList.toggle("grid-item--gigante"), i.layout();
        });
};
MasonryDocs["stamp-methods-demo"] = function (t) {
    "use strict";
    var e = t.querySelector(".grid"),
        o = new Masonry(e, { itemSelector: ".grid-item", columnWidth: 80 }),
        r = e.querySelector(".stamp"),
        n = !1,
        s = t.querySelector(".stamp-button"); s.addEventListener("click", function () {
            n ? o.unstamp(r) : o.stamp(r), o.layout(), n = !n;
        });
};
MasonryDocs.hero = function (e) {
    "use strict";
    function t(e) {
        var t = document.createElement("div"); t.className = "hero-grid__item"; var i = document.createElement("a"); i.className = "hero__example-link", i.href = e.url; var r = document.createElement("img"); r.className = "hero__example-link__img", r.src = e.image; var m = document.createElement("p"); return m.className = "hero__example-link__title", m.textContent = e.title, i.appendChild(r), i.appendChild(m), t.appendChild(i), t;
    } for (var i = e.querySelector(".hero-grid"), r = new Masonry(i, { itemSelector: ".hero-grid__item", columnWidth: ".hero-grid__grid-sizer", percentPosition: !0 }), m = [{ title: "Erik Johansson", url: "http://erikjohanssonphoto.com/work/imagecats/personal/", image: "http://i.imgur.com/6Lo8oun.jpg" }, { title: "Beyoncé | I Am", url: "http://iam.beyonce.com/", image: "http://i.imgur.com/HDSAMFl.jpg" }, { title: "Webster Hall Timeline", url: "http://www.websterhall.com/timeline/", image: "http://i.imgur.com/VWfCPYx.jpg" }, { title: "Halcyon theme", url: "http://halcyon-theme.tumblr.com/", image: "http://i.imgur.com/A1RSOhg.jpg" }, { title: "RESIZE.THATSH.IT", url: "http://resize.thatsh.it/", image: "http://i.imgur.com/00xWxLG.png" }, { title: "Tumblr Staff: Archive", url: "http://staff.tumblr.com/archive", image: "http://i.imgur.com/igjvRa3.jpg" }, { title: "Kristian Hammerstad", url: "http://www.kristianhammerstad.com/", image: "http://i.imgur.com/Zwd7Sch.jpg" }, { title: "Loading Effects for Grid Items | Demo 2", url: "http://tympanus.net/Development/GridLoadingEffects/index2.html", image: "http://i.imgur.com/iFBSB1t.jpg" }], a = [], o = document.createDocumentFragment(), n = 0, l = m.length; n < l; n++) {
        var c = t(m[n]); a.push(c), o.appendChild(c);
    } imagesLoaded(o).on("progress", function (e, t) {
        var m = t.img.parentNode.parentNode; i.appendChild(m), r.appended(m);
    });
};
MasonryDocs["refactor-shirt"] = function (t) {
    "use strict";
    var e = new Date(2016, 1, 10),
        r = Math.round((e - new Date()) / 864e5); t.querySelector(".refactor-shirt__title").textContent = "Refactor shirts. Only on sale for " + r + " more days.";
};
"use strict";

$(document).ready(function () {
    var container = document.querySelector('.container-menu');
    // var msnry = new Masonry( container, {
    //   // options
    //   columnWidth: 19.5,
    //   itemSelector: '.row-menu',
    //   alignLTR: true
    // });

    var maxLevel = 2;

    $(document).scroll(function () {
        if ($(document).scrollTop() > 30) {
            $("section.navbar .nav").addClass('fixed');
            $("section.navbar").addClass('fixed');
        } else {
            $("section.navbar .nav").removeClass('fixed');
            $("section.navbar").removeClass('fixed');
        }
    });

    $("ul.navbar > li > a").click(function (e) {
        e.preventDefault();
        $("ul.navbar > li > a").removeClass('active');
        $(this).addClass('active');
        $("section.menu").removeClass('active');
        //menu list special
        $("section.menu-list").removeClass('active');
        $("section.menu[data-id=" + $(this).attr('data-id') + "]").addClass('active');
        //menu list special
        $("section.menu-list[data-id=" + $(this).attr('data-id') + "]").addClass('active');
    });

    // $("ul.navbar > li > a").hover(function(e){
    //     e.preventDefault();

    //     $("ul.navbar > li > a").removeClass('active');
    //     $(this).addClass('active');
    //     $("section.menu").removeClass('active');
    //     $(`section.menu[data-id=${$(this).attr('data-id')}]`).addClass('active');
    // });

    /////////////test animation when click form///////////////////
    $("input[type=\"radio\"]").change(function () {
        console.log($(this).val());
        if ($(this).val() == '1') {
            console.log('fafafaf');
            $('.wrap-form.tab.hide').slideToggle();
        } else {
            $('.wrap-form.tab.hide').slideToggle();
        }
        // if($(this).prop("checked") == true){

        //     //alert("Checkbox is checked.");
        // }
        // else if($(this).prop("checked") == false){
        //     //alert("Checkbox is unchecked.");
        // }
    });

    $('.form-info').click(function (e) {
        e.preventDefault();
        var targetID = $(this).data("target");
        $('.wrap-form #' + targetID).slideToggle();
    });

    $('.close').click(function (e) {
        e.preventDefault();
        var targetID = $(this).data("target");
        $('.wrap-form .info#' + targetID).slideToggle();
    });
    ///////////test animation when click form///////////////////

    

    $("section.menu").mouseleave(function () {
        var data = $(this);
        setTimeout(function () {
            if (!data.is(":hover")) {
                data.removeClass('active');
                $("ul.navbar > li > a[data-id=" + data.attr('data-id') + "]").removeClass('active');
            }
        } /*,700*/);
    });

    //menu list special
    $("section.menu-list").mouseleave(function () {
        var data = $(this);
        setTimeout(function () {
            if (!data.is(":hover")) {
                data.removeClass('active');
                $("ul.navbar > li > a[data-id=" + data.attr('data-id') + "]").removeClass('active');
            }
        } /*,700*/);
        resetMenulist();
    });

    $("body").mouseup(function (evt) {
        $("section.menu").removeClass('active');
        if (!isMenuListItem(evt)) {
            $("ul.navbar > li > a").removeClass('active');
        }
    });

    $("body").on('touchend', function (evt) {
        $("ul.navbar > li > a").removeClass('active');
        $("section.menu").removeClass('active');
    });

    ////search mobile
    $(".search-mobile-button").click(function (e) {
        ///onclick for display searchbar
        e.preventDefault();
        $("section.mask").addClass("active");
        $("section.search-mobile").addClass("active");
        $("body").addClass("disable-scroll");
    });
    $(".wrap-search a").click(function (e) {
        ///onclick search button
        e.preventDefault();
        $("section.mask").removeClass("active");
        $("body").removeClass("disable-scroll");
        $("section.search-mobile").removeClass("active");
    });
    ////burger
    $("section.navbar .nav > .burger > a").click(function (e) {
        e.preventDefault();
        $("section.mask").addClass("active");
        $("section.menu-mobile").addClass("active");
        $("body").addClass("disable-scroll");
    });
    ///close mobile menu
    $("section.mask").click(function (e) {
        e.preventDefault();
        $("section.mask").removeClass("active");
        $("body").removeClass("disable-scroll");
        $("section.menu-mobile").removeClass("active");
        $("section.search-mobile").removeClass("active");
        $("section.menu-mobile > .content > .main").removeClass('disactive');
        $("section.menu-mobile > .content > .menu").removeClass('active');
    });

    ///mobile menu click main menu
    $("section.menu-mobile > .content ul.main > li > a").click(function (e) {
        e.preventDefault();
        console.log($(this).attr('data-id'));
        $("section.menu-mobile > .content > .menu[data-id=" + $(this).attr('data-id') + "]").addClass('active');
        $("section.menu-mobile > .content > .main").addClass('disactive');
    });

    ///sub menu click back
    $("section.menu-mobile > .content > .menu >.back > a").click(function (e) {
        e.preventDefault();
        $("section.menu-mobile > .content > .main").removeClass('disactive');
        $("section.menu-mobile > .content > .menu").removeClass('active');
    });

    ///progress menu type script
    $("section.menu-list .menu ul > li > a").click(function (e) {
        e.preventDefault();
        var currentLevel = $(this).attr('level');
        var targetId = $(this).attr('menu-id');
        $("section.menu-list .menu[level=" + currentLevel + "] ul > li > a").removeClass('active');
        removePreviousLevel(Number(currentLevel), maxLevel);
        $(this).addClass('active');
        if (targetId !== '') {
            $("section.menu-list .menu[menu-id=" + targetId + "]").addClass('active');
        }
    });

    var resetMenulist = function resetMenulist() {
        $("section.menu-list .menu[level=0] ul > li > a").removeClass('active');
        removePreviousLevel(0, maxLevel);
    };

    var removePreviousLevel = function removePreviousLevel(level, max) {
        for (var i = level + 1; i <= max; i++) {
            $("section.menu-list .menu[level=" + i + "]").removeClass('active');
            $("section.menu-list .menu[level=" + i + "] ul > li > a").removeClass('active');
        }
    };

    var isMenuListItem = function isMenuListItem(evt) {
        var className = $(evt.target).attr('class');
        if (className === 'menu-list-item' || className === 'menu-list active' || className === 'container-list') {
            return true;
        }
        return false;
    };

    function GScrolling(selector) {
        console.log('G', selector);
        if (Array.isArray(selector)) {
            for (var i = 0; i < selector.length; i++) {
                ScrollAnimate(selector[i]);
            }
        } else {
            ScrollAnimate(selector);
        }
    }

    var ScrollAnimate = function ScrollAnimate(obj) {
        // Assign custom properties with base properties
        var initialState = {
            selector: '',
            offset: 0,
            delay: 0,
            action: function action() { }
        };
        initialState = Object.assign(initialState, obj); //{ ...initialState, ...obj };
        var _selector = document.querySelectorAll(initialState.selector);

        // Init Animation
        initAnimate();
        window.addEventListener('scroll', function () {
            return initAnimate();
        });

        function initAnimate() {
            Array.prototype.forEach.call(_selector, function (element, i) {
                isVisible(element, initialState.offset, initialState.delay, initialState.action);
            });
        };

        // Check DOM is on screen
        function isVisible(selector) {
            var offset = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : 0;
            var delay = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : 0;
            var funcAnimate = arguments[3];

            var THIS = selector;
            if (!THIS.classList.contains('visibled')) {
                // Hidden DOM frist
                THIS.style.visibility = 'hidden';

                var winHeight = window.innerHeight;
                var windowTop = document.body.scrollTop;
                var rect = THIS.getBoundingClientRect();
                var prosTop = rect.top + windowTop;
                var realTop = prosTop - windowTop + offset;
                // If Current DOM is visible on screen
                if (winHeight >= realTop) {
                    // Add class visibled
                    THIS.classList.add('visibled');
                    // Call custom function
                    setTimeout(function () {
                        THIS.style.visibility = 'visible';
                        funcAnimate(THIS);
                    }, delay * 1000);
                };
            }
            return false;
        }
    };

    (function () {
        var dataScroll = [{
            selector: '.magic',
            offset: 10,
            delay: 0.3,
            action: function action(selector) {
                TweenMax.from(selector, 0.5, { y: 100, opacity: 0 });
            }
        }];

        GScrolling(dataScroll);
    })();
});