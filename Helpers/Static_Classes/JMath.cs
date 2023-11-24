using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
///<summary>
///Helper class dedicated to specialty math functions
///</summary>
public static class JMath{
    
    ///<summary>
    ///Convert boolean to integer. True returns 1, and false returns 0.
    ///</summary>
    public static int BoolToInt(bool boolean){
        if(boolean)
            return 1;
        return 0;
    }


    ///<summary>
    ///Get direction to follow for the shortest path between two angles
    ///</summary>
    public static float GetShorterAngleDistance(float startAngle, float endAngle){
        float diff = endAngle - startAngle;
        if(Mathf.Abs(diff) > 180)
            diff -= 360 * Mathf.Sign(diff);

        return diff;
    }


    ///<summary>
    ///Returns the starting point of a series of the given number of points separated by the given spacing, centered on zero
    ///</summary>
    public static float GetStartPoint(float totalPoints, float spacing){
        return ((totalPoints/2f)-.5f) * -spacing;
    }
    ///<summary>
    ///Returns the starting point of a series of the given number of points separated by the given spacing, centered on zero
    ///</summary>
    public static Vector2 GetStartPoint(Vector2 totalPoints, Vector2 spacing){
        return (((totalPoints/2f)- new Vector2(.5f, .5f)) * -spacing);
    }


    ///<summary>
    ///Returns a directional vector that has the given angle, in degrees
    ///</summary>
    public static Vector2 AngleToVector2(float angle){
        angle*=Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }


    ///<summary>
    ///Returns the angle, in degrees of the given Vector2
    ///</summary>
    public static float Vector2ToAngle(Vector2 direction){
        float angle = Mathf.Atan2(direction.y, direction.x)  *  Mathf.Rad2Deg;
        return angle;
    }


    ///<summary>
    ///Returns the average color of the sprite. Does not include pixels whose alpha values are 0
    ///NOTE: not 100% sure if this works correctly with sprites split from a spritesheet
    ///</summary>
    public static Color AverageColor(Sprite sprite){
        
        if(!sprite.texture.isReadable){
            Debug.LogError("The texture does not have read/write enabled");
            return Color.white;
        }

        Color sum = new Color(0, 0, 0);
        int totalPixels = 0;
        Color[] pixels = sprite.texture.GetPixels();
        foreach(Color color in pixels){
            if(color.a > 0){
                sum += color;
                totalPixels++;
            }
        }

        return sum / totalPixels;
    }

    ///<summary>
    ///Returns the total area, in world space, of pixels above the given alpha value
    ///NOTE: not 100% sure if this works correctly with sprites split from a spritesheet
    ///</summary>
    public static float GetSpriteArea(Sprite sprite, float minAlpha){
        
        if(!sprite.texture.isReadable){
            Debug.LogError("The texture does not have read/write enabled");
            return 0;
        }

        int totalPixels = 0;
        Color[] pixels = sprite.texture.GetPixels();
        foreach(Color color in pixels){
            if(color.a > minAlpha){
                totalPixels++;
            }
        }

        return totalPixels / sprite.pixelsPerUnit;
    }


    ///<summary>
    ///Returns a color between green and red. Different from Color.Lerp, because it maintains a high brightness throughout
    ///</summary>
    public static Color GreenToRedScale(float t){
        
        float r = t*2;

        float g = (1-t)*2;


        Color color = new Color(r, g, 0);

        return color;
    }


    //--------------------------------------------------------
    ///<summary>
    ///Static class dedicated to functions that have a starting point of (0, 0) and final point of (1, 1)
    ///</summary>
    public static class ZeroToOneFunctions{

        ///<summary>
        ///A function that has a starting point of (0, 0) and final point of (1, 1)
        ///</summary>
        [System.Serializable]
        public delegate float ZeroToOneFunction(float x);
        
        //---------------------------------------------------------------------------
        //Constant functions

        ///<summary>
        ///Straight line
        ///</summary>
        public static readonly ZeroToOneFunction LinearFunction =
        (float x) => {
            return x;
        };

        ///<summary>
        ///Sin wave. Returns values from 0 to the highest point of a sin curve, thus a quarter
        ///</summary>
        public static readonly ZeroToOneFunction QuarterSinFunction =
        (float x) => {
            return Mathf.Sin(((Mathf.PI/2)*x));
        };

        ///<summary>
        ///Sin wave. Returns values from the lowest to highest point of a sin curve, thus a half
        ///</summary>
        public static readonly ZeroToOneFunction HalfSinFunction = 
        (float x) => {
            return (Mathf.Cos(Mathf.PI + (Mathf.PI*x)) + 1)/2;
        };




        //---------------------------------------------------------------------------
        //Curry functions

        ///<summary>
        ///Power function. Returns y = x^p.
        ///</summary>
        public static ZeroToOneFunction PowerFunction(float p){
            return (float x) => {
                return Mathf.Pow(x, p);  
            };
        }
        ///<summary>
        ///Power function. Returns y = +/-((x - offset.x)^p + offset.y). 
        ///NOTE: Not guaranteed to return a function from zero to one. You must figure that out yourself
        ///</summary>
        public static ZeroToOneFunction PowerFunction(float p, Vector2 offset, bool flip){
            if(!flip)
                return (float x) => {
                    return Mathf.Pow(x - offset.x, p)  +  offset.y;  
                };
            else
                return (float x) => {
                    return -(Mathf.Pow(x - offset.x, p)  +  offset.y);  
                };
        }
    }



    //--------------------------------------------------------
    ///<summary>
    ///Static class dedicated to randomizer functions
    ///</summary>
    public static class Random{
        ///<summary>
        ///Returns a random bool; either true or false
        ///</summary>
        public static bool RandomBool(){
            float r = UnityEngine.Random.value;
            return (r < .5f);
        } 


        ///<summary>
        ///Returns a random sign; either 1 or -1
        ///</summary>
        public static int RandomSign(){
            float r = UnityEngine.Random.value;
            return (r < .5f) ? 1 : -1;
        }

        /// <summary>
        /// Returns a float between -1 and 1;
        /// </summary>
        public static float RandomSigned(){
            float r = UnityEngine.Random.value;
            return (1- 2*r);
        }

        ///<summary>
        ///Get a random value in the range, except for the given integer
        ///</summary>
        public static int DifferentRandomValue(int minInclusive, int maxExclusive, int exception){
            int r = UnityEngine.Random.Range(minInclusive, maxExclusive);

            if(r == exception){
                r++;

                if(r >= maxExclusive)
                    r = minInclusive;
            }



            return r;
        }
        ///<summary>
        ///Get a random value in the range, except for the values in the given integer array. 
        ///NOTE: Can throw an error if there are no possible values to return
        ///</summary>
        public static int DifferentRandomValue(int minInclusive, int maxExclusive, int[] exceptions){
            int r = UnityEngine.Random.Range(minInclusive, maxExclusive);


            bool correct = false;
            int iterations = 0;//counting the interations
            while(!correct){
                if(iterations >= maxExclusive-minInclusive){
                    throw new Exception("No possible values to return");
                }

                correct = true;
                iterations++;
                //check if any of the exceptions equals the value, r
                for(int i = 0; i < exceptions.Length; i++){

                    //if it does, increment r, and re-check all the values again
                    if(r == exceptions[i]){
                        r++;
                        if(r >= maxExclusive)
                            r = minInclusive;

                        correct = false;
                        break;
                    }
                }
            }



            return r;
        }


        ///<summary>
        ///Returns an integer within the size of the weightArray
        ///</summary>
        public static int WeightedRandomizer(int[] weightArray){
            int total = 0;
            foreach(int num in weightArray)
                total += num;

            int r = UnityEngine.Random.Range(0, total);
            for(int i = 0; i < weightArray.Length; i++){
                r -= weightArray[i];
                if(r < 0){
                    return i;
                }
            }

            //Shouldn't ever reach this
            return -1;
        }
    
    
    
        ///<summary>
        ///The average color of the sprite. Does not include pixels whose alpha values are 0
        ///NOTE: not 100% sure if this works correctly with sprites split from a spritesheet
        ///NOTE: This uses a brute force randomizer to ensure the chosen pixel doesn't have an alpha value of 0
        ///</summary>
        public static Color RandomColorOnSprite(Sprite sprite, float minGrayTolerance){
            
            if(!sprite.texture.isReadable){
                Debug.LogError("The texture does not have read/write enabled");
                return Color.white;
            }

            Color[] pixels = sprite.texture.GetPixels();
            Color randomColor;

            bool WithinMinGrayTolerance(Color c){
                float rbDiff = Mathf.Abs(c.r - c.b);
                float rgDiff = Mathf.Abs(c.r - c.g);
                float gbDiff = Mathf.Abs(c.g - c.b);

                return rbDiff < minGrayTolerance  &&  rgDiff < minGrayTolerance  &&  gbDiff < minGrayTolerance;
            }
            do{
                int randomIndex = UnityEngine.Random.Range(0, pixels.Length);
                randomColor = pixels[randomIndex];

                //check min gray tolerance
            }
            while(randomColor.a == 0 || WithinMinGrayTolerance(randomColor));

            return randomColor;
        }
    }
}
