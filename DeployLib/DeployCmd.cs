using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployLib
{
    public class DeployCmd
    {
        public DeployCmd(bool forceDeploy)
        {
            this.ForceDeploy = forceDeploy;
        }
        private bool cancelDeploy = false;

        /// <summary>
        /// Indicates whether the whole deployment process should be cancelled or not.
        /// <para>If true, the <code>ForceDeploy</code> property is invalid.</para>
        /// </summary>
        public bool CancelDeploy
        {
            get { return cancelDeploy; }
            set { cancelDeploy = value; }
        }
        private bool forceDeploy = false;

        /// <summary>
        /// Indicates that whether files are forced to cover original files.
        /// <para>If the <code>CancelDeploy</code> property is true, <code>ForceDeploy</code> is invalid.</para>
        /// </summary>
        public bool ForceDeploy
        {
            get { return forceDeploy; }
            set { forceDeploy = value; }
        }

        public override string ToString()
        {
            var result = string.Format("cancelled: {0}, force deploy: {1}", cancelDeploy, forceDeploy);
            return result;
        }
    }
}
