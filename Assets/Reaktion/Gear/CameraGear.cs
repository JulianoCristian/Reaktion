﻿//
// Reaktion - An audio reactive animation toolkit for Unity.
//
// Copyright (C) 2013, 2014 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Camera Gear")]
public class CameraGear : MonoBehaviour
{
    public bool autoBind = true;
    public Reaktor reaktor;

    public Modifier fieldOfView = Modifier.Linear(60, 45);
    public Modifier viewportWidth = Modifier.Linear(1, 0.2f);
    public Modifier viewportHeight = Modifier.Linear(1, 0.2f);

    void Awake()
    {
        if (autoBind || reaktor == null)
            reaktor = Reaktor.SearchAvailableFrom(gameObject);
    }

    void Update()
    {
        if (fieldOfView.enabled)
            camera.fieldOfView = fieldOfView.Evaluate(reaktor.Output);

        if (viewportWidth.enabled || viewportHeight.enabled)
        {
            var rect = camera.rect;
            if (viewportWidth.enabled)
            {
                rect.width = viewportWidth.Evaluate(reaktor.Output);
                rect.x = (1.0f - rect.width) * 0.5f;
            }
            if (viewportHeight.enabled)
            {
                rect.height = viewportHeight.Evaluate(reaktor.Output);
                rect.y = (1.0f - rect.height) * 0.5f;
            }
            camera.rect = rect;
        }
    }
}

} // namespace Reaktion
